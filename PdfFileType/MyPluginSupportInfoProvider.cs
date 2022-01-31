// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using PaintDotNet;

namespace PdfFileTypePlugin
{
    public sealed class MyPluginSupportInfoProvider : IPluginSupportInfoProvider
    {
        public IPluginSupportInfo GetPluginSupportInfo() => new MyPluginSupportInfo();
    }
}
