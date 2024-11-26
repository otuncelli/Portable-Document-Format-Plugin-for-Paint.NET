// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime;
using PdfFileTypePlugin.Import;

namespace PdfFileTypePlugin;

internal static class Utils
{
    #region Pixel <-> Point Conversions

    public static SizeF PixelsToPoints(Size size, float dpi = 72)
    {
        SizeF device = UI.DPI.Value;
        float width = size.Width * dpi / device.Width;
        float height = size.Height * dpi / device.Height;
        return new SizeF(width, height);
    }

    public static Size PointsToPixels(SizeF sizef, float dpi = 72)
    {
        SizeF device = UI.DPI.Value;
        int width = (int)Math.Round(sizef.Width / (dpi / device.Width));
        int height = (int)Math.Round(sizef.Height / (dpi / device.Height));
        return new Size(width, height);
    }

    #endregion

    #region Other

    public static bool FeatureFileExists(string filename)
    {
        try
        {
            string? dir = Path.GetDirectoryName(typeof(PdfFileType).Assembly.Location);
            if (string.IsNullOrWhiteSpace(dir))
                return false;
            string path = Path.Combine(dir, filename);
            return File.Exists(path);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public static MemoryFailPoint CreateMemoryFailPoint(int width, int height, int multiply = 1)
    {
        int bytesPerLayer = checked(width * 4 * height);
        int sizeInMegabytes = Math.Max(bytesPerLayer / (1024 * 1024), 1) * multiply;
        return new MemoryFailPoint(sizeInMegabytes);
    }

    public static unsafe void BufferCopy(IntPtr src, IntPtr dst, long length)
    {
        Buffer.MemoryCopy((void*)src, (void*)dst, length, length);
    }

    #endregion
}
