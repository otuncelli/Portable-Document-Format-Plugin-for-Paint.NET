// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using PaintDotNet;
using PaintDotNet.Rendering;

namespace PdfFileTypePlugin.Export
{
    internal abstract class PdfExporter
    {
        #region Fields

        protected readonly int Quality;
        protected readonly bool SkipHiddenLayers;
        protected readonly bool SkipDuplicateLayers;
        protected readonly bool EmbedProperties;
        protected readonly PdfStandard PdfStd;
        protected readonly ExportMode Mode;
        protected readonly Document Input;
        protected readonly Stream Output;
        protected readonly Surface ScratchSurface;
        protected readonly byte[] DocInfoPayload;
        private readonly ProgressEventHandler progress;
        private readonly ImageCodecInfo jpegCodec;

        #endregion

        #region Constructor

        protected PdfExporter(Document input, Stream output, PropertyBasedSaveConfigToken token, Surface scratchSurface, ProgressEventHandler progressCallback)
        {
            Ensure.IsNotNull(input, nameof(input));
            Ensure.IsNotNull(output, nameof(output));
            Ensure.IsNotNull(token, nameof(token));
            Ensure.IsNotNull(scratchSurface, nameof(scratchSurface));
            Ensure.IsNotNull(progressCallback, nameof(progressCallback));
            Ensure.Test(() => input.Width * 4 * input.Height, "Canvas is too big.");
            Ensure.IsTrue(input.Layers.Count > 0, () => throw new ArgumentException("Document does not contain layers."));

            scratchSurface.Clear();

            Input = input;
            Output = output;
            ScratchSurface = scratchSurface;
            progress = progressCallback;

            Quality = token.GetInt32PropertyValue(PropertyNames.Quality);
            SkipHiddenLayers = token.GetBooleanPropertyValue(PropertyNames.SkipInvisibleLayers);
            SkipDuplicateLayers = token.GetBooleanPropertyValue(PropertyNames.SkipDuplicateLayers);
            PdfStd = (PdfStandard)token.GetStaticListChoicePropertyValue(PropertyNames.PdfStandard);
            Mode = (ExportMode)token.GetStaticListChoicePropertyValue(PropertyNames.ExportMode);
            EmbedProperties = token.GetBooleanPropertyValue(PropertyNames.EmbedProperties) && (Mode == ExportMode.Cropped || Mode == ExportMode.Normal);

            if (EmbedProperties)
            {
                DocInfoPayload = DocumentProperties.CreatePayload(input);
                Debug.Assert(DocInfoPayload.Length <= short.MaxValue);
            }

            if (Quality < 100)
            {
                jpegCodec = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
            }
        }

        #endregion

        #region Methods

        public abstract void Export(CancellationToken cancellationToken);

        protected Surface GetPreparedSurface(BitmapLayer layer, bool first, out Rectangle bounds)
        {
            Surface surface = layer.Surface;
            bounds = surface.Bounds;
            if (Mode == ExportMode.Cropped && surface.TryGetRealBounds(out Rectangle realBounds))
            {
                bounds = realBounds;
            }

            if (Mode == ExportMode.Cumulative)
            {
                if (first && (Quality < 100 || PdfStd == PdfStandard.A1B))
                {
                    ScratchSurface.Fill(ColorBgra.White);
                }

                CompositionOp compOp = LayerBlendModeUtil.CreateCompositionOp(layer.BlendMode);
                compOp.Apply(ScratchSurface, surface);
                surface = ScratchSurface;
            }
            else if (Quality < 100 || PdfStd == PdfStandard.A1B)
            {
                ScratchSurface.Fill(ColorBgra.White);
                CompositionOps.Normal.Static.Apply(ScratchSurface, surface);
                surface = ScratchSurface;
            }

            return surface;
        }

        protected IReadOnlyList<BitmapLayer> GetFilteredLayers()
        {
            if (Mode == ExportMode.Flattened)
            {
                Input.Flatten(ScratchSurface);
                return new[] { new BitmapLayer(ScratchSurface, takeOwnership: false) };
            }

            IEnumerable<BitmapLayer> list = from BitmapLayer layer in Input.GetBitmapLayers()
                                            where layer.Visible || !SkipHiddenLayers
                                            select layer;
#if DEBUG
            list = list.Take(1);
#endif
            if (SkipDuplicateLayers)
            {
                list = list.Distinct(BitmapLayerEqualityComparer.Instance);
            }
            return list.ToList();
        }

        protected EncoderParameters CreateEncoderParameters()
        {
            var prms = new EncoderParameters(1);
            prms.Param[0] = new EncoderParameter(Encoder.Quality, Quality);
            return prms;
        }

        protected void OnProgress(double progress) => this.progress?.Invoke(this, new ProgressEventArgs(progress));

        protected MemoryStream Encode(Bitmap bmp)
        {
            MemoryStream ms = new MemoryStream();
            using (var prms = CreateEncoderParameters())
            {
                bmp.Save(ms, jpegCodec, prms);
            }
            ms.Position = 0;
            return ms;
        }

#pragma warning disable CA1822 // Mark members as static
        protected MemoryStream EncodeLossless(Bitmap bmp)
#pragma warning restore CA1822 // Mark members as static
        {
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);
            ms.Position = 0;
            return ms;
        }

        #endregion

        #region Static 

        public static PdfExporter Create(Document input, Stream output, PropertyBasedSaveConfigToken token, Surface scratchSurface, ProgressEventHandler progressCallback)
        {
            PdfStandard std = (PdfStandard)token.GetStaticListChoicePropertyValue(PropertyNames.PdfStandard);
            return std == PdfStandard.None
#pragma warning disable IDE0004
                ? (PdfExporter)new PdfiumExporter(input, output, token, scratchSurface, progressCallback)
#pragma warning restore IDE0004
                : new PdfPigExporter(input, output, token, scratchSurface, progressCallback);
        }

        #endregion

        #region BitmapLayerEqualityComparer

        private sealed class BitmapLayerEqualityComparer : IEqualityComparer<BitmapLayer>
        {
            public static readonly BitmapLayerEqualityComparer Instance = new BitmapLayerEqualityComparer();

            private BitmapLayerEqualityComparer()
            {
            }

            public bool Equals(BitmapLayer x, BitmapLayer y)
            {
                if (x == null && y == null) { return true; }
                if (x == null || y == null) { return false; }
                if (ReferenceEquals(x, y)) { return true; }
                return GetPixelDataHash(x.Surface) == GetPixelDataHash(y.Surface);
            }

            public int GetHashCode(BitmapLayer obj) => GetPixelDataHash(obj.Surface);

            private static unsafe int GetPixelDataHash(Surface surface)
            {
                const int prime = 16777619;
                int length = surface.Stride * surface.Height;
                byte* ptr = (byte*)surface.Scan0.VoidStar;
                unchecked
                {
                    int hash = (int)2166136261;
                    for (int i = 0; i < length; i++)
                    {
                        hash = (hash ^ *(ptr + i)) * prime;
                    }
                    hash += hash << 13;
                    hash ^= hash >> 7;
                    hash += hash << 3;
                    hash ^= hash >> 17;
                    hash += hash << 5;
                    return hash;
                }
            }
        }

        #endregion
    }
}
