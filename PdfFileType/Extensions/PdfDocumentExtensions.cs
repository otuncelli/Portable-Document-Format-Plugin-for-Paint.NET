// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.Drawing;
using PDFiumSharp;

namespace PdfFileTypePlugin
{
    internal static class PdfDocumentExtensions
    {
        public static bool GetMaxPageSize(this PdfDocument pdf, out int maxPageWidth, out int maxPageHeight)
        {
            bool varies = false;
            SizeF maxPageSizeF = pdf.Pages[0].Size;
            for (int i = 1; i < pdf.Pages.Count; i++)
            {
                PdfPage page = pdf.Pages[i];
                SizeF pageSize = page.Size;
                if (pageSize != maxPageSizeF)
                {
                    varies = true;
                    maxPageSizeF = new SizeF(
                        width: Math.Max(maxPageSizeF.Width, pageSize.Width),
                        height: Math.Max(maxPageSizeF.Height, pageSize.Height));
                }
            }
            maxPageSizeF = Util.PointsToPixels(maxPageSizeF);
            Size maxPageSize = Size.Round(maxPageSizeF);
            maxPageWidth = maxPageSize.Width;
            maxPageHeight = maxPageSize.Height;
            return varies;
        }
    }
}
