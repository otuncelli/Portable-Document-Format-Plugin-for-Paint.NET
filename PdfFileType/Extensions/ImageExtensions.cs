// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System.Drawing;

namespace PdfFileTypePlugin
{
    internal static class ImageExtensions
    {
        public static RectangleF GetBounds(this Image image, GraphicsUnit unit = GraphicsUnit.Pixel)
            => image.GetBounds(ref unit);
    }
}
