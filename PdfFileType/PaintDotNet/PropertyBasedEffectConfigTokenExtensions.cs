// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

#if EFFECT
using System;
using PaintDotNet.Effects;
using PaintDotNet.PropertySystem;

namespace PaintDotNet
{
    internal static class PropertyBasedEffectConfigTokenExtensions
    {
        public static TValue GetPropertyValue<TProperty, TValue>(this PropertyBasedEffectConfigToken token, object propertyName) where TProperty : Property
            => (TValue)token.GetProperty<TProperty>(propertyName).Value;

        public static TValue GetScalarPropertyValue<TProperty, TValue>(this PropertyBasedEffectConfigToken token, object propertyName) where TValue : struct, IComparable<TValue> where TProperty : ScalarProperty<TValue>
            => token.GetPropertyValue<TProperty, TValue>(propertyName);

        public static int GetInt32PropertyValue(this PropertyBasedEffectConfigToken token, object propertyName)
            => token.GetScalarPropertyValue<Int32Property, int>(propertyName);

        public static double GetDoublePropertyValue(this PropertyBasedEffectConfigToken token, object propertyName)
            => token.GetScalarPropertyValue<DoubleProperty, double>(propertyName);

        public static bool GetBooleanPropertyValue(this PropertyBasedEffectConfigToken token, object propertyName)
            => token.GetPropertyValue<BooleanProperty, bool>(propertyName);

        public static Pair<double, double> GetDoubleVectorPropertyValue(this PropertyBasedEffectConfigToken token, object propertyName)
            => token.GetPropertyValue<DoubleVectorProperty, Pair<double, double>>(propertyName);

        public static Tuple<double, double, double> GetDoubleVector3PropertyValue(this PropertyBasedEffectConfigToken token, object propertyName)
            => token.GetPropertyValue<DoubleVector3Property, Tuple<double, double, double>>(propertyName);

        public static ImageResource GetImagePropertyValue(this PropertyBasedEffectConfigToken token, object propertyName)
            => token.GetPropertyValue<ImageProperty, ImageResource>(propertyName);

        public static string GetStringPropertyValue(this PropertyBasedEffectConfigToken token, object propertyName)
            => token.GetPropertyValue<StringProperty, string>(propertyName);

        public static Uri GetUriPropertyValue(this PropertyBasedEffectConfigToken token, object propertyName)
            => token.GetPropertyValue<UriProperty, Uri>(propertyName);

        public static object GetStaticListChoicePropertyValue(this PropertyBasedEffectConfigToken token, object propertyName)
            => token.GetPropertyValue<StaticListChoiceProperty, object>(propertyName);
    }
}
#endif
