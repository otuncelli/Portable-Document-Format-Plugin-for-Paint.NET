// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using PdfFileTypePlugin.Export;
using PDFiumSharp.Enums;

namespace PdfFileTypePlugin.Localization
{

    internal static class Localize
    {
        public static string GetDisplayName(PdfStandard value)
        {
            switch (value)
            {
                case PdfStandard.A1B: return "PDF/A-1b";
                case PdfStandard.A2B: return "PDF/A-2b";
                case PdfStandard.None: return StringResources.PdfStandardNone;
                default: throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        public static string GetDisplayName(ExportMode value)
        {
            switch (value)
            {
                case ExportMode.Cropped: return StringResources.ExportModeCropped;
                case ExportMode.Cumulative: return StringResources.ExportModeCumulative;
                case ExportMode.Flattened: return StringResources.ExportModeFlattened;
                case ExportMode.Normal: return StringResources.ExportModeNormal;
                default: throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        public static string GetDisplayName(RenderingFlags flag)
        {
            string name = Enum.GetName(typeof(RenderingFlags), flag);
            string resourceKey = $"{nameof(RenderingFlags)}{name}";
            return StringResources.ResourceManager.GetString(resourceKey);
        }

        public static string GetDescription(RenderingFlags flag)
        {
            string name = Enum.GetName(typeof(RenderingFlags), flag);
            string resourceKey = $"{nameof(RenderingFlags)}{name}Desc";
            return StringResources.ResourceManager.GetString(resourceKey);
        }
    }
}
