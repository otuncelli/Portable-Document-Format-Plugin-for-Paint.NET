// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.Collections.Generic;
using PaintDotNet;

namespace PdfFileTypePlugin;

public sealed class PdfFileTypeFactory : IFileTypeFactory2
{
    public FileType[] GetFileTypeInstances(IFileTypeHost host)
    {
        Services.Init(host!.Services);
        List<FileType> filetypes = [];
        if (!Utils.FeatureFileExists("PdfFileType.DisableAi.txt"))
        {
            filetypes.Add(new AiFileType());
        }

        FileTypeOptions options = new FileTypeOptions
        {
            LoadExtensions = [".pdf"],
            SupportsCancellation = true,
            SupportsLayers = true
        };

        options.SaveExtensions = Utils.FeatureFileExists("PdfFileType.DisableSave.txt")
            ? []
            : options.LoadExtensions;
        filetypes.Add(new PdfFileType(options));
        return [.. filetypes];
    }
}
