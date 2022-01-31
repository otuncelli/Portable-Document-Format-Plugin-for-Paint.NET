// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;

namespace PaintDotNet.IndirectUI
{
    internal static class ControlInfoExtensions
    {
        public static ControlInfo Property(this ControlInfo info, object propertyName, Func<PropertyControlInfo, PropertyControlInfo> selector, bool throwOnError = true)
        {
            PropertyControlInfo pci = info.FindControlForPropertyName(propertyName);
            if (pci == null)
            {
                return throwOnError
                    ? throw new ArgumentException($"Can not find control for property: {propertyName}.", nameof(propertyName))
                    : info;
            }
            _ = selector(pci);
            return info;
        }

        public static ControlInfo Property(this ControlInfo info, object propertyName, string displayName, Func<PropertyControlInfo, PropertyControlInfo> selector = null, bool throwOnError = true)
        {
            return info.Property(propertyName, p =>
            {
                PropertyControlInfo pci = p.DisplayName(displayName);
                return selector != null ? selector(pci) : pci;
            }, throwOnError);
        }

        public static ControlInfo Property(this ControlInfo info, object propertyName, string displayName, string description, Func<PropertyControlInfo, PropertyControlInfo> selector = null, bool throwOnError = true)
        {
            return info.Property(propertyName, displayName, p =>
              {
                  PropertyControlInfo pci = p.Description(description);
                  return selector != null ? selector(pci) : pci;
              }, throwOnError);
        }

        public static bool TryGetControl(this ControlInfo info, object propertyName, out PropertyControlInfo pci)
        {
            pci = info.FindControlForPropertyName(propertyName);
            return pci != null;
        }
    }
}
