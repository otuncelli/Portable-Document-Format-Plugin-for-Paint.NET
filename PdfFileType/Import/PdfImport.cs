// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PaintDotNet;
using PaintDotNet.Rendering;
using PdfFileTypePlugin.Localization;
using PDFiumSharp;
using PDFiumSharp.Enums;
using PDFiumSharp.Types;

namespace PdfFileTypePlugin.Import
{
    internal sealed class PdfImport
    {
        #region Properties

        public IReadOnlyList<int> PageIndices { get; set; } = new[] { 0 };

        public RenderingFlags RenderingFlags { get; set; }

        public ContentAlignment Alignment { get; set; }

        public Color BlendColor { get; set; }

        public bool Descending { get; set; }

        public bool RestoreProperties { get; set; }

        public LayerBlendMode BlendMode { get; set; }

        public bool Invisible { get; set; }

        public byte Opacity { get; set; } = 255;

        #endregion

        #region Fields 

        private readonly int DegreeOfParallelism = Environment.ProcessorCount;
        private readonly int width;
        private readonly int height;
        private readonly int maxPageWidth;
        private readonly int maxPageHeight;

        #endregion

        #region Constructors

        public PdfImport(int width, int height, int maxPageWidth, int maxPageHeight)
        {
            Ensure.IsGreaterThan(width, 0, nameof(width));
            Ensure.IsGreaterThan(height, 0, nameof(height));
            Ensure.IsGreaterThan(maxPageWidth, 0, nameof(maxPageWidth));
            Ensure.IsGreaterThan(maxPageHeight, 0, nameof(maxPageHeight));

            this.width = width;
            this.height = height;
            this.maxPageWidth = maxPageWidth;
            this.maxPageHeight = maxPageHeight;
        }

        public PdfImport(int width, int height) : this(width, height, width, height)
        {
        }

        #endregion

        #region Convert

        public Document Convert(PdfDocument pdf, Action progress = null, CancellationToken cancellationToken = default)
        {
            Ensure.IsNotNull(pdf, nameof(pdf));
            Ensure.IsTrue(pdf.Pages.Count > 0, () => throw new ArgumentException("PDF does not contain any page.", nameof(pdf)));

            DocumentProperties docProps = null;
            int doNotCheckMark = 0;
            ConcurrentDictionary<int, Lazy<BitmapLayer>> map = new ConcurrentDictionary<int, Lazy<BitmapLayer>>(DegreeOfParallelism, PageIndices.Count);
            ParallelQuery<BitmapLayer> layers = PageIndices
                .AsParallel()
                .AsOrdered()
                .WithCancellation(cancellationToken)
                .WithDegreeOfParallelism(DegreeOfParallelism)
                .Select(index =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    BitmapLayer layer = null;
                    Lazy<BitmapLayer> lazy = map.GetOrAdd(index, i => new Lazy<BitmapLayer>(() =>
                    {
                        using (PdfPage page = pdf.Pages[i])
                        {
                            LayerProperties layerProps = null;

                            if (RestoreProperties && Interlocked.CompareExchange(ref doNotCheckMark, 0, 0) == 0)
                            {
                                if (TryGetObjMark(page, DocumentProperties.MarkName, out FPDF_PAGEOBJECTMARK mark))
                                {
                                    byte[] buf = new byte[short.MaxValue];
                                    if (i == 0)
                                    {
                                        _ = DocumentProperties.TryLoadFromPageObjectMark(mark, buf, out docProps);
                                    }

                                    if (!LayerProperties.TryLoadFromPageObjectMark(mark, buf, out layerProps))
                                    {
                                        Interlocked.CompareExchange(ref doNotCheckMark, 1, 0);
                                    }
                                }
                                else
                                {
                                    Interlocked.CompareExchange(ref doNotCheckMark, 1, 0);
                                }
                            }

                            Surface surface = RenderPage(page);
                            BitmapLayer layer1 = null;
                            try
                            {
                                layer1 = new BitmapLayer(surface, takeOwnership: true);
                                if (layerProps == null || !layerProps.TryCopyTo(layer1))
                                {
                                    layer1.Name = page.GetLabel() ?? String.Format(StringResources.PageNumberFormat, i + 1);
                                    layer1.Visible = !Invisible;
                                    layer1.Opacity = Opacity;
                                    layer1.BlendMode = BlendMode;
                                }
                                layer = layer1;
                                return layer;
                            }
                            catch
                            {
                                surface.Dispose();
                                layer1?.Dispose();
                                throw;
                            }
                        }
                    }, LazyThreadSafetyMode.ExecutionAndPublication));

                    if (layer == null)
                    {
                        layer = lazy.Value;
                        // create copy of surface
                        layer = new BitmapLayer(layer.Surface, takeOwnership: false)
                        {
                            Name = layer.Name,
                            Visible = layer.Visible,
                            BlendMode = layer.BlendMode,
                            Opacity = layer.Opacity
                        };
                    }
                    try
                    {
                        progress?.Invoke();
                    }
                    catch
                    {
                        layer.Dispose();
                        throw;
                    }
                    return layer;
                });

            if (Descending)
            {
                layers = layers.Reverse();
            }

            Document document = new Document(width, height);
            try
            {
                var layerList = layers.ToList();
                layerList[0].Visible = true;
                document.Layers.AddRange(layerList);

                if (RestoreProperties)
                {
                    docProps?.CopyTo(document);
                }
            }
            catch (OperationCanceledException ex)
            {
                document.Dispose();
                throw new WarningException(StringResources.CanceledUponYourRequest, ex);
            }
            catch
            {
                document.Dispose();
                throw;
            }
            return document;
        }

        #endregion

        #region Private Methods

        private Rectangle GetPlacementRectangle(PdfPage page)
        {
            float scx = width / (float)maxPageWidth;
            float scy = height / (float)maxPageHeight;
            SizeF scaledSizeF = page.Size;
            scaledSizeF = new SizeF(scaledSizeF.Width * scx, scaledSizeF.Height * scy);
            scaledSizeF = Util.PointsToPixels(scaledSizeF);
            Size scaledSize = Size.Round(scaledSizeF);
            int scaledWidth = scaledSize.Width;
            int scaledHeight = scaledSize.Height;
            int x, y;
            if (scaledWidth < width)
            {
                switch (Alignment)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.BottomLeft:
                        x = 0;
                        break;
                    case ContentAlignment.TopRight:
                    case ContentAlignment.MiddleRight:
                    case ContentAlignment.BottomRight:
                        x = width - scaledWidth;
                        break;
                    default:
                        x = (int)Math.Round((width - scaledWidth) / 2.0);
                        break;
                }
            }
            else
            {
                x = 0;
                scaledWidth = width;
            }

            if (scaledHeight < height)
            {
                switch (Alignment)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.TopCenter:
                    case ContentAlignment.TopRight:
                        y = 0;
                        break;
                    case ContentAlignment.BottomLeft:
                    case ContentAlignment.BottomCenter:
                    case ContentAlignment.BottomRight:
                        y = height - scaledHeight;
                        break;
                    default:
                        y = (int)Math.Round((height - scaledHeight) / 2.0);
                        break;
                }
            }
            else
            {
                y = 0;
                scaledHeight = height;
            }
            return new Rectangle(x, y, scaledWidth, scaledHeight);
        }

        private Surface RenderPage(PdfPage page)
        {
            Rectangle rect = GetPlacementRectangle(page);

            using (_ = Util.CreateMemoryFailPoint(width, height, 2))
            using (PDFiumBitmap rhs = new PDFiumBitmap(width, height, hasAlpha: true))
            {
                IntPtr src = rhs.Scan0;
                int stride = rhs.Stride;
                page.Render(rhs, rect, PageOrientations.Normal, RenderingFlags);
                Surface surface = new Surface(width, height, SurfaceCreationFlags.DoNotZeroFillHint);
                IntPtr dst = surface.Scan0.Pointer;
                try
                {
                    if (BlendColor != Color.Empty && BlendColor != Color.Transparent)
                    {
                        surface.Fill(BlendColor);
                        CompositionOps.Normal.Static.UnsafeApply(width, height, dst, stride, src, stride);
                    }
                    else
                    {
                        Util.BufferCopy(src, dst, stride * height);
                    }
                }
                catch
                {
                    surface.Dispose();
                    throw;
                }
                return surface;
            }
        }

        #endregion

        #region Static

        private static bool TryGetObjMark(PdfPage page, string markName, out FPDF_PAGEOBJECTMARK mark)
        {
            mark = default;
            FPDF_PAGEOBJECT obj = PDFium.FPDFPage_GetObject(page.Handle, 0);
            if (!obj.IsNull)
            {
                mark = PDFium.FPDFPageObj_GetMark(obj, 0);
                if (!mark.IsNull)
                {
                    byte[] buf = new byte[sbyte.MaxValue];
                    if (PDFium.FPDFPageObjMark_GetName(mark, ref buf[0], (uint)buf.Length, out uint out_buflen))
                    {
                        string markName1 = Encoding.Unicode.GetString(buf, 0, (int)out_buflen).TrimEnd('\0');
                        if (markName1.Equals(markName, StringComparison.Ordinal))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        #region Load

        public static Document Load(Stream input)
        {
            Ensure.IsNotNull(input, nameof(input));
            PdfDocument pdf = null;

            if (UI.SaveConfigDialog != null)
            {
#if DEBUG
                pdf = OpenDocument(input, null);
                _ = pdf.GetMaxPageSize(out int maxPageWidth, out int maxPageHeight);
                PdfImport import = new PdfImport(maxPageWidth, maxPageHeight);
                return import.Convert(pdf, null, default);
#else
                return GetPreviewNotSupportedDocument();
#endif
            }

            string password = null;
            do
            {
                try
                {
                    pdf = OpenDocument(input, password);
                }
                catch (PDFiumException) when (PDFium.FPDF_GetLastError() == FPDF_ERR.PASSWORD)
                {
                    password = UI.Invoke(() =>
                    {
                        using (PasswordInputDialog dialog = new PasswordInputDialog())
                        {
                            DialogResult dr = dialog.ShowDialog();
                            if (dr != DialogResult.OK)
                            {
                                throw new WarningException(StringResources.CanceledUponYourRequest, new OperationCanceledException());
                            }
                            return dialog.Password;
                        }
                    });
                }
            } while (pdf == null);

            using (pdf)
            {
                return UI.Invoke(() =>
                {
                    using (PdfImportDialog dialog = new PdfImportDialog(pdf, !String.IsNullOrEmpty(password)))
                    {
                        dialog.ShowDialog();
                        return dialog.Result;
                    }
                });
            }
        }

        private static PdfDocument OpenDocument(Stream stream, string password = null)
        {
            byte[] managedBuffer = null;
            bool FileRead(IntPtr _, int position, IntPtr buffer, int size)
            {
                managedBuffer = managedBuffer == null || managedBuffer.Length < size ? new byte[size] : managedBuffer;
                stream.Position = position;
                int len = stream.Read(managedBuffer, 0, size);
                if (len > 0)
                {
                    Marshal.Copy(managedBuffer, 0, buffer, len);
                    return len == size;
                }
                return false;
            }

            FPDF_FILEREAD fileRead = new FPDF_FILEREAD(checked((int)stream.Length), FileRead);
            stream.Position = 0;
            return new PdfDocument(stream, fileRead, count: 0, password: password);
        }

        private static Document GetPreviewNotSupportedDocument()
        {
            string header = StringResources.PreviewTextHeader + Environment.NewLine + Environment.NewLine;
            string text = StringResources.PreviewText;

            using (StringFormat sf_header = new StringFormat() { Alignment = StringAlignment.Center })
            using (StringFormat sf_body = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near })
            using (Font font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold))
            using (Graphics screen = Graphics.FromHwnd(IntPtr.Zero))
            {
                Size header_size = Size.Round(screen.MeasureString(header, font));
                Size text_size = Size.Round(screen.MeasureString(text, font));

                int width = Math.Max(header_size.Width, text_size.Width);
                int height = header_size.Height + text_size.Height;

                using (var bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb))
                using (var g = Graphics.FromImage(bmp))
                {
                    Color foreColor;
                    Color backColor;
                    if (UI.IsDarkMode)
                    {
                        foreColor = Color.White;
                        backColor = Color.FromArgb(48, 48, 48);
                    }
                    else
                    {
                        foreColor = Color.Black;
                        backColor = Color.LightGray;
                    }

                    g.Clear(backColor);
                    g.DrawString(header, font, Brushes.Red, bmp.GetBounds(), sf_header);
                    using (var textBrush = new SolidBrush(foreColor))
                    {
                        g.DrawString(text, font, textBrush, new PointF(0, header_size.Height), sf_body);
                    }
                    return Document.FromImage(bmp);
                }
            }
        }

        #endregion

        #endregion
    }
}
