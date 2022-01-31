// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime;
using System.Threading;
using PaintDotNet;
using PDFiumSharp;
using PDFiumSharp.Enums;
using PDFiumSharp.Types;

namespace PdfFileTypePlugin.Export
{
    internal sealed class PdfiumExporter : PdfExporter
    {
        public PdfiumExporter(Document input, Stream output, PropertyBasedSaveConfigToken token, Surface scratchSurface, ProgressEventHandler progressCallback)
            : base(input, output, token, scratchSurface, progressCallback)
        {
        }

        public override void Export(CancellationToken cancellationToken)
        {
            IReadOnlyList<BitmapLayer> layers = GetFilteredLayers();

            PDFiumBitmap pdfBmp = null;
            MemoryFailPoint mfp1 = null;

            if (Mode != ExportMode.Cropped)
            {
                mfp1 = Util.CreateMemoryFailPoint(Input.Width, Input.Height);
                pdfBmp = new PDFiumBitmap(Input.Width, Input.Height, hasAlpha: true);
            }

            using (mfp1)
            using (pdfBmp)
            using (PdfDocument pdf = new PdfDocument())
            {
                bool ret;
                for (int i = 0; i < layers.Count; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    BitmapLayer layer = layers[i];
                    Surface surface = GetPreparedSurface(layer, i == 0, out Rectangle bounds);

                    SizeF sizef = Util.PixelsToPoints(bounds.Size);
                    double widthPt = sizef.Width;
                    double heightPt = sizef.Height;

                    PdfPage page = null;
                    FPDF_PAGEOBJECT img = default;
                    try
                    {
                        page = pdf.Pages.Add(widthPt, heightPt);
                        img = PDFium.FPDFPageObj_NewImageObj(pdf.Handle);
                        Ensure.IsTrue(!img.IsNull, ThrowPDFiumException);
                        FPDF_PAGE[] loadedPages = { page.Handle };
                        if (EmbedProperties)
                        {
                            FPDF_PAGEOBJECTMARK mark = PDFium.FPDFPageObj_AddMark(img, DocumentProperties.MarkName);
                            Ensure.IsTrue(!mark.IsNull, ThrowPDFiumException);
                            if (i == 0)
                            {
                                // Only the first page contains document properties.
                                ret = PDFium.FPDFPageObjMark_SetBlobParam(pdf.Handle, img, mark, DocumentProperties.ParamName, ref DocInfoPayload[0], (uint)DocInfoPayload.Length);
                                Ensure.IsTrue(ret, ThrowPDFiumException);
                            }

                            byte[] buf = LayerProperties.CreatePayload(layer);
                            Debug.Assert(buf.Length <= short.MaxValue);
                            ret = PDFium.FPDFPageObjMark_SetBlobParam(pdf.Handle, img, mark, LayerProperties.ParamName, ref buf[0], (uint)buf.Length);
                            Ensure.IsTrue(ret, ThrowPDFiumException);
                        }

                        using (_ = Util.CreateMemoryFailPoint(surface.Width, surface.Height))
                        using (Bitmap bitmap = surface.CreateAliasedBitmap(bounds, alpha: true))
                        {
                            if (Quality < 100)
                            {
                                using (MemoryStream ms = Encode(bitmap))
                                {
                                    ret = PDFium.FPDFImageObj_LoadJpegFile(loadedPages, img, ms, count: 0, inline: true);
                                    Ensure.IsTrue(ret, ThrowPDFiumException);
                                }
                            }
                            else
                            {
                                bounds = Rectangle.Round(bitmap.GetBounds());
                                BitmapData bitmapData = bitmap.LockBits(bounds, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                                int width = bitmapData.Width;
                                int height = bitmapData.Height;
                                int stride = bitmapData.Stride;
                                int length = stride * bitmapData.Height;
                                IntPtr src = bitmapData.Scan0;
                                if (Mode == ExportMode.Cropped)
                                {
                                    using (_ = Util.CreateMemoryFailPoint(width, height))
                                    using (var pdfBmp1 = new PDFiumBitmap(width, height, BitmapFormats.BGRA, src, stride))
                                    {
                                        ret = PDFium.FPDFImageObj_SetBitmap(loadedPages, img, pdfBmp1.Handle);
                                    }
                                }
                                else
                                {
                                    Util.BufferCopy(src, pdfBmp.Scan0, length);
                                    ret = PDFium.FPDFImageObj_SetBitmap(loadedPages, img, pdfBmp.Handle);
                                }
                                Ensure.IsTrue(ret, ThrowPDFiumException);
                                bitmap.UnlockBits(bitmapData);
                            }
                            ret = PDFium.FPDFImageObj_SetMatrix(img, widthPt, 0, 0, heightPt, 0, 0);
                            Ensure.IsTrue(ret, ThrowPDFiumException);
                            PDFium.FPDFPage_InsertObject(page.Handle, ref img);
                            ret = PDFium.FPDFPage_GenerateContent(page.Handle);
                            Ensure.IsTrue(ret, ThrowPDFiumException);
                        }
                        OnProgress((i + 1) * 90 / (double)layers.Count);
                    }
                    finally
                    {
                        if (!img.IsNull)
                        {
                            PDFium.FPDFPageObj_Destroy(img);
                        }
                        page?.Dispose();
                    }
                }
                ret = pdf.Save(Output, flags: SaveFlags.None);
                Ensure.IsTrue(ret, ThrowPDFiumException);
                OnProgress(100);
            }
        }

        private static void ThrowPDFiumException() => throw new PDFiumException();
    }
}
