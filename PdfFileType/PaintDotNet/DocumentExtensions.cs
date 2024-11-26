// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System.Collections.Generic;
using System.Linq;

namespace PaintDotNet;

internal static class DocumentExtensions
{
    public static IList<BitmapLayer> GetBitmapLayers(this Document document)
        => document.Layers.OfType<BitmapLayer>().ToList();
}
