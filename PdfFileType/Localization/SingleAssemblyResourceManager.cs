// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;

namespace PdfFileTypePlugin.Localization;

internal sealed class SingleAssemblyResourceManager : ResourceManager
{
    private readonly ConcurrentDictionary<CultureInfo, Lazy<ResourceSet?>> resourceSets = new();

    public SingleAssemblyResourceManager() : base()
    {
    }

    public SingleAssemblyResourceManager(Type resourceSource) : base(resourceSource)
    {
    }

    public SingleAssemblyResourceManager(string baseName, Assembly assembly) : base(baseName, assembly)
    {
    }

    public SingleAssemblyResourceManager(string baseName, Assembly assembly, Type usingResourceSet) : base(baseName, assembly, usingResourceSet)
    {
    }

    protected override ResourceSet? InternalGetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
    {
        /* If you call GetOrAdd simultaneously on different threads, addValueFactory may be called 
         * multiple times, but its key/value pair might not be added to the dictionary for every call.*/
        Debug.Assert(culture != null);
        Lazy<ResourceSet?> lazyResourceSet = resourceSets.GetOrAdd(culture, c =>
        {
            return new Lazy<ResourceSet?>(() =>
            {
                string filename = GetResourceFileName(c);
                Stream? stream = MainAssembly?.GetManifestResourceStream(filename);
                return stream != null ? new ResourceSet(stream) : base.InternalGetResourceSet(c, createIfNotExists, tryParents);
            });
        });
        return lazyResourceSet.Value;
    }

    protected override string GetResourceFileName(CultureInfo culture)
    {
        StringBuilder sb = new StringBuilder(255);
        sb.Append(BaseName);
        // If this is the neutral culture, don't append culture name.
        if (culture.Name != CultureInfo.InvariantCulture.Name)
        {
            VerifyCultureName(culture.Name, throwException: true);
            sb.Append('_');
            sb.Append(culture.Name);
        }
        sb.Append(".resources");
        return sb.ToString();
    }

    private static bool VerifyCultureName(string cultureName, bool throwException)
    {
        // This function is used by ResourceManager.GetResourceFileName().
        // ResourceManager searches for resource using CultureInfo.Name,
        // so we should check against CultureInfo.Name.
        for (int i = 0; i < cultureName.Length; i++)
        {
            char c = cultureName[i];
            // TODO: Names can only be RFC4646 names (ie: a-zA-Z0-9) while this allows any unicode letter/digit
            if (char.IsLetterOrDigit(c) || c == '-' || c == '_')
            {
                continue;
            }
            if (throwException)
            {
                throw new ArgumentException(string.Format("The given culture name '{0}' cannot be used to locate a resource file. " +
                    "Resource filenames must consist of only letters, numbers, hyphens or underscores.", cultureName));
            }
            return false;
        }
        return true;
    }
}
