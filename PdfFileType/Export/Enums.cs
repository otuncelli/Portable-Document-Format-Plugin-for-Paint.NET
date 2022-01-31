// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using UglyToad.PdfPig.Writer;

namespace PdfFileTypePlugin.Export
{
    internal enum ExportMode
    {
        Normal,
        Cropped,
        Cumulative,
        Flattened
    }

    internal enum PdfStandard
    {
        None = PdfAStandard.None,
        A1B = PdfAStandard.A1B,
        A2B = PdfAStandard.A2B
    }

    internal enum PropertyNames
    {
        Quality,
        SkipInvisibleLayers,
        SkipDuplicateLayers,
        ExportMode,
        EmbedProperties,
        PdfStandard,
        GitHubLink,
        ForumLink
    }
}
