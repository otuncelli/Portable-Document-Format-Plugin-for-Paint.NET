// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System.Text;
using PDFiumSharp;

namespace PdfFileTypePlugin
{
    internal static class PdfPageExtensions
    {
        public static string GetLabel(this PdfPage page, int buflen = 255)
        {
            byte[] buf = new byte[buflen];
            uint len = PDFium.FPDF_GetPageLabel(page.Document.Handle, page.Index, ref buf[0], (uint)buf.Length);
            return len > 0 ? Encoding.Unicode.GetString(buf, 0, (int)len).TrimEnd('\0') : null;
        }
    }
}
