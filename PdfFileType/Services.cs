// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.Threading;
using PaintDotNet;

namespace PdfFileTypePlugin;

internal static class Services
{
    private static IServiceProvider? Provider;

    internal static void Init(IServiceProvider provider)
    {
        Interlocked.Exchange(ref Provider, provider);
    }

    public static TService? Get<TService>() where TService : class
    {
        IServiceProvider? provider = Interlocked.CompareExchange(ref Provider, null, null);
        return provider?.GetService<TService>();
    }
}
