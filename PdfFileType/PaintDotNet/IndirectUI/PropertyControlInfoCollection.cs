// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using PaintDotNet.IndirectUI.Extensions;
using PaintDotNet.PropertySystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PaintDotNet.IndirectUI
{
    public sealed class PropertyControlInfoCollection : IEnumerable<PropertyControlInfo>, IEnumerable
    {
        #region Fields

        private readonly HashSet<PropertyName> addedToPanel = new HashSet<PropertyName>();

        private readonly KeyedPropertyControlInfoCollection items = new KeyedPropertyControlInfoCollection();

        #endregion

        #region Properties

        public IReadOnlyList<Property> Properties => items.Select(pci => pci.Property).ToList();

        public IReadOnlyList<PropertyName> PropertyNames => Properties.Select(p => (PropertyName)p.Name).ToList();

        public PropertyControlInfo this[PropertyName propertyName] => items[propertyName];

        public int Count => items.Count;

        PropertyControlInfo this[int index] => GetPropertyControlInfoAt(index);

        #endregion

        #region Constructors

        public PropertyControlInfoCollection(IEnumerable<Property> props)
        {
            foreach (Property prop in props)
            {
                PropertyControlInfo pci = PropertyControlInfo.CreateFor(prop);
                pci.DisplayName(prop.Name);
                items.Add(pci);
            }
        }

        #endregion

        #region Methods

        public PropertyControlInfoCollection Configure(PropertyName propertyName, Func<PropertyControlInfo, PropertyControlInfo> selector)
        {
            PropertyControlInfo pci = items[propertyName];
            _ = selector(pci);
            return this;
        }

        public PropertyControlInfoCollection Configure(PropertyName propertyName, string displayName, Func<PropertyControlInfo, PropertyControlInfo> selector = null)
        {
            return Configure(propertyName, p =>
            {
                PropertyControlInfo pci = p.DisplayName(displayName);
                return selector?.Invoke(pci) ?? pci;
            });
        }

        public PropertyControlInfoCollection Configure(PropertyName propertyName, string displayName, string description, Func<PropertyControlInfo, PropertyControlInfo> selector = null)
        {
            return Configure(propertyName, displayName, p =>
            {
                PropertyControlInfo pci = p.Description(description);
                return selector?.Invoke(pci) ?? pci;
            });
        }

        public bool TryGetControl(PropertyName propertyName, out PropertyControlInfo pci)
        {
#if NETCOREAPP
            return items.TryGetValue(propertyName, out pci);
#else
            pci = default;
            if (!items.Contains(propertyName)) { return false; }
            pci = items[propertyName];
            return true;
#endif
        }

        public PropertyControlInfo GetPropertyControlInfoAt(int index) => ((Collection<PropertyControlInfo>)items)[index];

        public PanelControlInfo CreatePanelControlInfo(params PropertyName[] propertyNames)
        {
            if (propertyNames == null || propertyNames.Length == 0)
            {
                propertyNames = PropertyNames.ToArray();
            }

            PanelControlInfo panel = new PanelControlInfo();
            foreach (PropertyName propertyName in PropertyNames)
            {
                if (addedToPanel.Contains(propertyName)) { continue; }
                if (!propertyNames.Contains(propertyName)) { continue; }
                PropertyControlInfo pci = items[propertyName];
                panel.AddChildControl(pci);
                addedToPanel.Add(propertyName);
            }
            if (panel.ChildControls.Count == 0) { return null; }
            return panel;
        }

        #endregion

        #region IEnumerable

        public IEnumerator<PropertyControlInfo> GetEnumerator()
            => items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        #endregion

        #region KeyedPropertyControlInfoCollection

        private sealed class KeyedPropertyControlInfoCollection : KeyedCollection<PropertyName, PropertyControlInfo>
        {
            protected override PropertyName GetKeyForItem(PropertyControlInfo item) => (PropertyName)item.Property.Name;
        }

        #endregion
    }
}
