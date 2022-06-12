// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;

namespace PaintDotNet.PropertySystem
{
    public class PropertyName : IEquatable<PropertyName>
    {
        public string Name { get; }

        public PropertyName(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public bool Equals(PropertyName other)
        {
            if (other == null) { return false; }
            if (ReferenceEquals(this, other)) { return true; }
            return Name.Equals(other.Name);
        }

        public override bool Equals(object obj)
            => Equals(obj as PropertyName);

        public override int GetHashCode()
            => Name.GetHashCode();

        public override string ToString()
            => Name;

        public TEnum? ToEnum<TEnum>(bool ignoreCase = false) where TEnum: struct, Enum
            => Enum.TryParse(Name, ignoreCase, out TEnum @enum) ? (TEnum?)@enum : null;

        public static implicit operator PropertyName(Enum @enum)
        {
            if (@enum is null) { throw new ArgumentNullException(nameof(@enum)); }
            return new PropertyName(Enum.GetName(@enum.GetType(), @enum));
        }

        public static implicit operator PropertyName(string s)
            => new PropertyName(s);

        public static implicit operator string(PropertyName pn)
            => pn.Name;

        public static bool operator ==(PropertyName pn, Enum @enum)
        {
            if (@enum is null) { return false; }
            return Enum.GetName(@enum.GetType(), @enum).Equals(pn.Name, StringComparison.Ordinal);
        }

        public static bool operator !=(PropertyName pn, Enum @enum)
            => !(pn == @enum);
    }
}
