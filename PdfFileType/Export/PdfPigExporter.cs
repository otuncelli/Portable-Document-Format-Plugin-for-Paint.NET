// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using PaintDotNet;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Graphics.Operations.MarkedContent;
using UglyToad.PdfPig.Tokens;
using UglyToad.PdfPig.Writer;

namespace PdfFileTypePlugin.Export
{
    internal sealed class PdfPigExporter : PdfExporter
    {
        public PdfPigExporter(Document input, Stream output, PropertyBasedSaveConfigToken token, Surface scratchSurface, ProgressEventHandler progressCallback)
            : base(input, output, token, scratchSurface, progressCallback)
        {
        }

        private static void MarkedContentBegin(PdfPageBuilder pb, string mark, IDictionary<string, byte[]> dictionary)
        {
            DictionaryToken dictToken = DictionaryToken.With(dictionary.ToDictionary(n => n.Key, n => (IToken)new HexToken(BitConverter.ToString(n.Value).Replace("-", "").ToCharArray())));
            BeginMarkedContentWithProperties bmcwp = new BeginMarkedContentWithProperties(NameToken.Create(mark), dictToken);
            pb.CurrentStream.Operations.Add(bmcwp);
        }

        private static void MarkedContentEnd(PdfPageBuilder pb) => pb.CurrentStream.Operations.Add(EndMarkedContent.Value);

        private PdfDocumentBuilder CreatePdfDocBuilder()
        {
            PdfDocumentBuilder builder = new PdfDocumentBuilder(Output, false);
            builder.ArchiveStandard = (PdfAStandard)PdfStd;
            builder.IncludeDocumentInformation = true;
            builder.DocumentInformation.Producer = MyPluginSupportInfo.Instance.DisplayName;
            builder.DocumentInformation.Author = null;
            builder.DocumentInformation.Creator = null;
            builder.DocumentInformation.Keywords = null;
            builder.DocumentInformation.Subject = null;
            builder.DocumentInformation.Title = null;
            return builder;
        }

        public override void Export(CancellationToken cancellationToken)
        {
            IReadOnlyList<BitmapLayer> layers = GetFilteredLayers();

            using (PdfDocumentBuilder pdf = CreatePdfDocBuilder())
            {
                for (int i = 0; i < layers.Count; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    BitmapLayer layer = layers[i];
                    Surface surface = GetPreparedSurface(layer, i == 0, out Rectangle bounds);

                    SizeF sizef = Util.PixelsToPoints(bounds.Size);
                    double widthPt = sizef.Width;
                    double heightPt = sizef.Height;
                    PdfPageBuilder page = pdf.AddPage(widthPt, heightPt);
                    PdfRectangle size = page.PageSize;

                    if (EmbedProperties)
                    {
                        Dictionary<string, byte[]> objectMarks = new Dictionary<string, byte[]>();
                        if (i == 0)
                        {
                            // Only the first page contains document properties.
                            objectMarks.Add(DocumentProperties.ParamName, DocInfoPayload);
                        }
                        byte[] buf = LayerProperties.CreatePayload(layer);
                        Debug.Assert(buf.Length <= short.MaxValue);
                        objectMarks.Add(LayerProperties.ParamName, buf);
                        MarkedContentBegin(page, DocumentProperties.MarkName, objectMarks);
                    }

                    using (_ = Util.CreateMemoryFailPoint(surface.Width, surface.Height))
                    using (Bitmap bitmap = surface.CreateAliasedBitmap(bounds, alpha: true))
                    {
                        byte[] payload;
                        if (Quality < 100)
                        {
                            using (MemoryStream ms = Encode(bitmap))
                            {
                                payload = ms.ToArray();
                            }
                            page.AddJpeg(payload, size);
                        }
                        else
                        {
                            using (MemoryStream ms = EncodeLossless(bitmap))
                            {
                                payload = ms.ToArray();
                            }
                            page.AddPng(payload, size);
                        }
                    }

                    if (EmbedProperties)
                    {
                        MarkedContentEnd(page);
                    }

                    OnProgress((i + 1) * 100 / (double)layers.Count);
                }
            }
        }
    }
}
