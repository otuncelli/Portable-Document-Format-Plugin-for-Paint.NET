// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

namespace PaintDotNet.PropertySystem
{
    internal static class PropertyCollectionExtensions
    {
        public static T GetPropertyValue<T>(this PropertyCollection collection, object propertyName)
            => (T)collection.GetPropertyValue(propertyName);

        public static object GetPropertyValue(this PropertyCollection collection, object propertyName)
            => collection[propertyName].Value;
    }
}
