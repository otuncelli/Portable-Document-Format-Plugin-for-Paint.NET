// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.Reflection;
using PaintDotNet;

namespace PdfFileTypePlugin
{
    public sealed class MyPluginSupportInfo : IPluginSupportInfo
    {
        internal static readonly MyPluginSupportInfo Instance = new MyPluginSupportInfo();

        #region IPluginSupportInfo

        public string Author { get; } = "Osman Tunçelli";

        public string Copyright { get; } = GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;

        public string DisplayName { get; } = GetCustomAttribute<AssemblyProductAttribute>().Product;

        public Version Version { get; } = GetAssembly().GetName().Version;

        public Uri WebsiteUri { get; } = new Uri("https://github.com/otuncelli/Portable-Document-Format-Plugin-for-Paint.NET");

        public Uri ForumUri { get; } = new Uri("https://forums.getpaint.net/index.php?showtopic=118681");

        #endregion

        #region Static 

        private static T GetCustomAttribute<T>() where T : Attribute
            => GetAssembly().GetCustomAttribute<T>();

        private static Assembly GetAssembly()
            => typeof(MyPluginSupportInfo).Assembly;

        #endregion
    }
}
