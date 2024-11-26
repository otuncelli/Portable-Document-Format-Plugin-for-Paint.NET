// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.Reflection;
using PaintDotNet;

namespace PdfFileTypePlugin;

public sealed class MyPluginSupportInfo : IPluginSupportInfo
{
    internal static readonly MyPluginSupportInfo Instance = new();

    #region IPluginSupportInfo

    public string Author { get; } = "Osman Tunçelli";

    public string Copyright { get; } = GetCopyright();

    public string DisplayName { get; } = GetDisplayName();

    public Version Version { get; } = GetVersion();

    public Uri WebsiteUri { get; } = new Uri("https://github.com/otuncelli/Portable-Document-Format-Plugin-for-Paint.NET");

    public Uri ForumUri { get; } = new Uri("https://forums.getpaint.net/index.php?showtopic=118681");

    #endregion

    #region Static 

    private static string GetCopyright()
    {
        return GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright 
            ?? $"Copyright © {DateTime.Now.Year} Osman Tunçelli";
    }

    private static string GetDisplayName()
    {
        return GetCustomAttribute<AssemblyProductAttribute>()?.Product
            ?? "PDF/AI File Type Plugin for Paint.NET";
    }

    private static Version GetVersion()
    {
        return GetAssembly().GetName().Version
            ?? new Version(1, 0, 0, 0);
    }

    private static T? GetCustomAttribute<T>() where T : Attribute
    {
        return GetAssembly().GetCustomAttribute<T>();
    }

    private static Assembly GetAssembly()
    {
        return typeof(MyPluginSupportInfo).Assembly;
    }

    #endregion
}
