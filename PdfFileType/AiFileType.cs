// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.IO;
using PaintDotNet;

namespace PdfFileTypePlugin;

[PluginSupportInfo(typeof(MyPluginSupportInfo))]
public sealed class AiFileType : PdfFileType
{
    private static readonly FileTypeOptions BaseOptions = new FileTypeOptions
    {
        LoadExtensions = [".ai"],
        SupportsCancellation = true,
        SupportsLayers = true,
        SaveExtensions = []
    };

    public AiFileType() : base(GetName("AI"), BaseOptions)
    {
    }

    protected sealed override void OnSaveT(Document input, Stream output, PropertyBasedSaveConfigToken token, Surface scratchSurface, ProgressEventHandler progressCallback)
    {
        throw new NotSupportedException();
    }
}
