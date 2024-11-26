// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;

namespace PaintDotNet.PropertySystem.Extensions;

public static class PropertyCollectionExtensions
{
    public static T GetPropertyValue<T>(this PropertyCollection collection, PropertyName propertyName)
        => (T)Convert.ChangeType(collection.GetPropertyValue(propertyName), typeof(T));

    public static object GetPropertyValue(this PropertyCollection collection, PropertyName propertyName)
        => collection[propertyName].Value;
}
