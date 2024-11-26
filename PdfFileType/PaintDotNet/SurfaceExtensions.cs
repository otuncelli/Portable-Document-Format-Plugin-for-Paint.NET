// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System.Drawing;

namespace PaintDotNet;

internal static partial class SurfaceExtensions
{
    #region Real Bounds 

    public static bool TryGetRealBounds(this Surface surface, out Rectangle bounds)
    {
        int left = FindLeft(surface);
        if (left == int.MaxValue)
        {
            bounds = default;
            return false;
        }
        int top = FindTop(surface, left);
        int right = FindRight(surface, left, top);
        int bottom = FindBottom(surface, left, top, right);
        bounds = Rectangle.FromLTRB(left, top, right, bottom);
        return true;
    }

    private static int FindLeft(Surface surface)
    {
        // Find xMin
        for (int x = 0; x < surface.Width; x++)
        {
            for (int y = 0; y < surface.Height; y++)
            {
                byte alpha = surface.GetPointUnchecked(x, y).A;
                if (alpha != 0)
                {
                    return x;
                }
            }
        }
        return int.MaxValue;
    }

    private static int FindTop(Surface surface, int left)
    {
        // Find yMin
        for (int y = 0; y < surface.Height; y++)
        {
            for (int x = left; x < surface.Width; x++)
            {
                byte alpha = surface.GetPointUnchecked(x, y).A;
                if (alpha != 0)
                {
                    return y;
                }
            }
        }
        return int.MaxValue;
    }

    private static int FindRight(Surface surface, int left, int top)
    {
        // Find xMax
        for (int x = surface.Width - 1; x >= left; x--)
        {
            for (int y = top; y < surface.Height; y++)
            {
                byte alpha = surface.GetPointUnchecked(x, y).A;
                if (alpha != 0)
                {
                    return x;
                }
            }
        }
        return int.MinValue;
    }

    private static int FindBottom(Surface surface, int left, int top, int right)
    {
        // Find yMax
        for (int y = surface.Height - 1; y >= top; y--)
        {
            for (int x = left; x <= right; x++)
            {
                byte alpha = surface.GetPointUnchecked(x, y).A;
                if (alpha != 0)
                {
                    return y;
                }
            }
        }
        return int.MinValue;
    }

    #endregion
}
