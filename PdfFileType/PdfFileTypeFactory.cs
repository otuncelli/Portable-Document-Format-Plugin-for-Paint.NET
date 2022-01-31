// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.Collections.Generic;
using PaintDotNet;

namespace PdfFileTypePlugin
{
    public sealed class PdfFileTypeFactory : IFileTypeFactory
    {
        public FileType[] GetFileTypeInstances()
        {
            List<FileType> filetypes = new List<FileType>();
            if (!Util.FileExists("PdfFileType.DisableAi.txt", def: false))
            {
                filetypes.Add(new AiFileType());
            }

            FileTypeOptions options = new FileTypeOptions
            {
                LoadExtensions = new[] { ".pdf" },
                SupportsCancellation = true,
                SupportsLayers = true
            };

            options.SaveExtensions = Util.FileExists("PdfFileType.DisableSave.txt", def: false)
                ? Array.Empty<string>()
                : options.LoadExtensions;
            filetypes.Add(new PdfFileType(options));
            return filetypes.ToArray();
        }
    }
}
