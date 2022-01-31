// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PaintDotNet.PropertySystem
{
    internal sealed class MyPropertyCollection : KeyedCollection<object, Property>
    {
        #region Constructors

        public MyPropertyCollection(IEnumerable<Property> props) : base(PropertyNameComparer.Instance)
        {
            foreach (Property prop in props)
            {
                Add(prop);
            }
        }

        public MyPropertyCollection(IEnumerable<Property> props, IEnumerable<PropertyCollectionRule> rules) : this(props)
        {
            this.rules.AddRange(rules);
        }

        public MyPropertyCollection() : base(PropertyNameComparer.Instance)
        {
        }

        #endregion

        private readonly List<PropertyCollectionRule> rules = new List<PropertyCollectionRule>();

        public IReadOnlyList<PropertyCollectionRule> Rules => rules;

        protected override object GetKeyForItem(Property item) => item.GetOriginalNameValue();

        public MyPropertyCollection Add(Property property, int index)
        {
            if (index < 0 || index >= Count)
            {
                Add(property);
            }
            else
            {
                Insert(index, property);
            }
            return this;
        }

        #region Int32

        /// <summary>
        /// Adds <see cref="Int32Property" />
        /// </summary>
        /// <remarks>
        /// <strong>Supported Controls</strong>: 
        /// <see cref="PropertyControlType.Slider" />, 
        /// <see cref="PropertyControlType.ColorWheel" />,
        /// <see cref="PropertyControlType.IncrementButton" />
        /// <br/>
        /// <strong>Default Control</strong>: 
        /// <see cref="PropertyControlType.Slider" />
        /// </remarks>
        public MyPropertyCollection AddInt32Property(object name, int index = -1)
            => Add(new Int32Property(name), index);

        /// <inheritdoc cref="AddInt32Property(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, int defaultValue, int minValue, int maxValue, bool readOnly, ValueValidationFailureResult vvfResult, int index = -1)
            => Add(new Int32Property(name, defaultValue, minValue, maxValue, readOnly, vvfResult), index);

        /// <inheritdoc cref="AddInt32Property(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, int defaultValue, int minValue, int maxValue, bool readOnly, int index = -1)
            => Add(new Int32Property(name, defaultValue, minValue, maxValue, readOnly), index);

        /// <inheritdoc cref="AddInt32Property(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, int defaultValue, int minValue, int maxValue, int index = -1)
            => Add(new Int32Property(name, defaultValue, minValue, maxValue), index);

        /// <inheritdoc cref="AddInt32Property(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, int defaultValue, int index = -1)
            => Add(new Int32Property(name, defaultValue), index);

        /// <inheritdoc cref="AddInt32Property(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, ColorBgra defaultValue, bool readOnly, int index = -1)
            => Add(new Int32Property(name, ColorBgra.ToOpaqueInt32(defaultValue), 0, 0xffffff, readOnly), index);

        /// <inheritdoc cref="AddInt32Property(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, ColorBgra defaultValue, int index = -1)
            => Add(new Int32Property(name, ColorBgra.ToOpaqueInt32(defaultValue), 0, 0xffffff), index);

        #endregion

        #region Double

        /// <summary>
        /// Adds <see cref="DoubleProperty" />
        /// </summary>
        /// <remarks>
        /// <strong>Supported Controls</strong>:
        /// <see cref="PropertyControlType.Slider" />, 
        /// <see cref="PropertyControlType.AngleChooser" />
        /// <br/>
        /// <strong>Default Control</strong>: 
        /// <see cref="PropertyControlType.Slider" />
        /// <br />
        /// <strong>Default Minimum</strong>: -32768
        /// <br />
        /// <strong>Default Maximum</strong>: 32767
        /// </remarks>
        public MyPropertyCollection AddDoubleProperty(object name, int index = -1)
            => Add(new DoubleProperty(name), index);

        /// <inheritdoc cref="AddDoubleProperty(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, double defaultValue, double minValue, double maxValue, bool readOnly, ValueValidationFailureResult vvfResult, int index = -1)
            => Add(new DoubleProperty(name, defaultValue, minValue, maxValue, readOnly, vvfResult), index);

        /// <inheritdoc cref="AddDoubleProperty(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, double defaultValue, double minValue, double maxValue, bool readOnly, int index = -1)
            => Add(new DoubleProperty(name, defaultValue, minValue, maxValue, readOnly), index);

        /// <inheritdoc cref="AddDoubleProperty(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, double defaultValue, double minValue, double maxValue, int index = -1)
            => Add(new DoubleProperty(name, defaultValue, minValue, maxValue), index);

        /// <inheritdoc cref="AddDoubleProperty(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, double defaultValue, int index = -1)
            => Add(new DoubleProperty(name, defaultValue), index);

        #endregion

        #region Boolean

        /// <summary>
        /// Adds <see cref="BooleanProperty" />
        /// </summary>
        /// <remarks>
        /// <strong>Supported Controls</strong>:
        /// <see cref="PropertyControlType.CheckBox" />
        /// </remarks>
        public MyPropertyCollection AddBooleanProperty(object name, int index = -1)
            => Add(new BooleanProperty(name), index);

        /// <inheritdoc cref="AddBooleanProperty(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, bool defaultValue, bool readOnly, ValueValidationFailureResult vvfResult, int index = -1)
            => Add(new BooleanProperty(name, defaultValue, readOnly, vvfResult), index);

        /// <inheritdoc cref="AddBooleanProperty(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, bool defaultValue, bool readOnly, int index = -1)
            => Add(new BooleanProperty(name, defaultValue, readOnly), index);

        /// <inheritdoc cref="AddBooleanProperty(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, bool defaultValue, int index = -1)
            => Add(new BooleanProperty(name, defaultValue), index);

        #endregion

        #region Double Vector

        /// <summary>
        /// Adds <see cref="DoubleVectorProperty" />
        /// </summary>
        /// <remarks>
        /// <strong>Supported Controls</strong>:
        /// <see cref="PropertyControlType.PanAndSlider" />
        /// <see cref="PropertyControlType.Slider" />,
        /// <br/>
        /// <strong>Default Control</strong>: 
        /// <see cref="PropertyControlType.PanAndSlider" />
        /// </remarks>
        public MyPropertyCollection AddDoubleVectorProperty(object name, int index = -1)
            => Add(new DoubleVectorProperty(name), index);

        /// <inheritdoc cref="AddDoubleVectorProperty(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, Pair<double, double> defaultValues, Pair<double, double> minValues, Pair<double, double> maxValues, bool readOnly, ValueValidationFailureResult vvfResult, int index = -1)
            => Add(new DoubleVectorProperty(name, defaultValues, minValues, maxValues, readOnly, vvfResult), index);

        /// <inheritdoc cref="AddDoubleVectorProperty(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, Pair<double, double> defaultValues, Pair<double, double> minValues, Pair<double, double> maxValues, bool readOnly, int index = -1)
            => Add(new DoubleVectorProperty(name, defaultValues, minValues, maxValues, readOnly), index);

        /// <inheritdoc cref="AddDoubleVectorProperty(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, Pair<double, double> defaultValues, Pair<double, double> minValues, Pair<double, double> maxValues, int index = -1)
            => Add(new DoubleVectorProperty(name, defaultValues, minValues, maxValues), index);

        /// <inheritdoc cref="AddDoubleVectorProperty(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, Pair<double, double> defaultValues, int index = -1)
            => Add(new DoubleVectorProperty(name, defaultValues), index);

        #endregion

        #region Double Vector3

        /// <summary>
        /// Adds <see cref="DoubleVector3Property" />
        /// </summary>
        /// <remarks>
        /// <strong>Supported Controls</strong>:
        /// <see cref="PropertyControlType.Slider" />,
        /// <see cref="PropertyControlType.RollBallAndSliders" />
        /// <br/>
        /// <strong>Default Control</strong>: 
        /// <see cref="PropertyControlType.Slider" />
        /// </remarks>
        public MyPropertyCollection AddDoubleVector3Property(object name, int index = -1)
            => Add(new DoubleVector3Property(name), index);

        /// <inheritdoc cref="AddDoubleVector3Property(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, Tuple<double, double, double> defaultValues, Tuple<double, double, double> minValues, Tuple<double, double, double> maxValues, bool readOnly, ValueValidationFailureResult vvfResult, int index = -1)
            => Add(new DoubleVector3Property(name, defaultValues, minValues, maxValues, readOnly, vvfResult), index);

        /// <inheritdoc cref="AddDoubleVector3Property(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, Tuple<double, double, double> defaultValues, Tuple<double, double, double> minValues, Tuple<double, double, double> maxValues, bool readOnly, int index = -1)
            => Add(new DoubleVector3Property(name, defaultValues, minValues, maxValues, readOnly), index);

        /// <inheritdoc cref="AddDoubleVector3Property(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, Tuple<double, double, double> defaultValues, Tuple<double, double, double> minValues, Tuple<double, double, double> maxValues, int index = -1)
            => Add(new DoubleVector3Property(name, defaultValues, minValues, maxValues), index);

        /// <inheritdoc cref="AddDoubleVector3Property(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, Tuple<double, double, double> defaultValues, int index = -1)
            => Add(new DoubleVector3Property(name, defaultValues), index);

        #endregion

        #region Image Property

        /// <summary>
        /// Adds <see cref="ImageProperty" />
        /// </summary>
        public MyPropertyCollection AddImageProperty(object name, int index = -1)
            => Add(new ImageProperty(name), index);

        /// <inheritdoc cref="AddImageProperty(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, ImageResource image, bool readOnly, int index = -1)
            => Add(new ImageProperty(name, image, readOnly), index);

        /// <inheritdoc cref="AddImageProperty(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, ImageResource image, int index = -1)
            => Add(new ImageProperty(name, image), index);

        #endregion

        #region String

        /// <summary>
        /// Adds <see cref="StringProperty" />
        /// </summary>
        /// <remarks>
        /// <strong>Supported Controls</strong>:
        /// <see cref="PropertyControlType.TextBox" />
        /// <see cref="PropertyControlType.FileChooser"/>
        /// <br/>
        /// <strong>Default Control</strong>: 
        /// <see cref="PropertyControlType.TextBox" />
        /// </remarks>
        public MyPropertyCollection AddStringProperty(object name, int index = -1)
            => Add(new StringProperty(name), index);

        /// <inheritdoc cref="AddStringProperty(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, string defaultValue, int maxLength, bool readOnly, ValueValidationFailureResult vvfResult, int index = -1)
            => Add(new StringProperty(name, defaultValue, maxLength, readOnly, vvfResult), index);

        /// <inheritdoc cref="AddStringProperty(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, string defaultValue, int maxLength, bool readOnly, int index = -1)
            => Add(new StringProperty(name, defaultValue, maxLength, readOnly), index);

        /// <inheritdoc cref="AddStringProperty(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, string defaultValue, int maxLength, int index = -1)
            => Add(new StringProperty(name, defaultValue, maxLength), index);

        /// <inheritdoc cref="AddStringProperty(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, string defaultValue, int index = -1)
            => Add(new StringProperty(name, defaultValue), index);

        #endregion

        #region Uri

        /// <summary>
        /// Adds <see cref="UriProperty" />
        /// </summary>
        /// <remarks>
        /// <strong>Supported Controls</strong>:
        /// <see cref="PropertyControlType.LinkLabel" />
        /// </remarks>
        public MyPropertyCollection AddUriProperty(object name, int index = -1)
            => Add(new UriProperty(name), index);

        /// <inheritdoc cref="AddUriProperty(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, Uri defaultValue, bool readOnly, ValueValidationFailureResult vvfResult, int index = -1)
            => Add(new UriProperty(name, defaultValue, readOnly, vvfResult), index);

        /// <inheritdoc cref="AddUriProperty(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, Uri defaultValue, bool readOnly, int index = -1)
            => Add(new UriProperty(name, defaultValue, readOnly), index);

        /// <inheritdoc cref="AddUriProperty(PropertyCollection, object)" />
        public MyPropertyCollection Add(object name, Uri defaultValue, int index = -1)
            => Add(new UriProperty(name, defaultValue), index);

        #endregion

        #region StaticListChoiceProperty

        /// <summary>
        /// Adds <see cref="StaticListChoiceProperty" />
        /// </summary>
        /// <remarks>
        /// <strong>Supported Controls</strong>:
        /// <see cref="PropertyControlType.DropDown" />
        /// <see cref="PropertyControlType.RadioButton" />,
        /// <br/>
        /// <strong>Default Control</strong>: 
        /// <see cref="PropertyControlType.DropDown" />
        /// </remarks>
        public MyPropertyCollection Add(object name, IEnumerable valueChoices, int index = -1)
            => Add(new StaticListChoiceProperty(name, valueChoices.Cast<object>().ToArray()), index);

        /// <inheritdoc cref="Add(PropertyCollection, object, IEnumerable)" />
        public MyPropertyCollection Add(object name, IEnumerable valueChoices, int defaultChoiceIndex, bool readOnly, ValueValidationFailureResult vvfResult, int index = -1)
            => Add(new StaticListChoiceProperty(name, valueChoices.Cast<object>().ToArray(), defaultChoiceIndex, readOnly, vvfResult), index);

        /// <inheritdoc cref="Add(PropertyCollection, object, IEnumerable)" />
        public MyPropertyCollection Add(object name, IEnumerable valueChoices, int defaultChoiceIndex, bool readOnly, int index = -1)
            => Add(new StaticListChoiceProperty(name, valueChoices.Cast<object>().ToArray(), defaultChoiceIndex, readOnly), index);

        /// <inheritdoc cref="Add(PropertyCollection, object, IEnumerable)" />
        public MyPropertyCollection Add(object name, IEnumerable valueChoices, int defaultChoiceIndex, int index = -1)
            => Add(new StaticListChoiceProperty(name, valueChoices.Cast<object>().ToArray(), defaultChoiceIndex), index);

        /// <inheritdoc cref="Add(PropertyCollection, object, IEnumerable)" />
        public MyPropertyCollection Add<TEnum>(object name, TEnum defaultValue, bool readOnly, int index = -1) where TEnum : Enum
            => Add(StaticListChoiceProperty.CreateForEnum(name, defaultValue, readOnly), index);

        /// <inheritdoc cref="Add(PropertyCollection, object, IEnumerable)" />
        public MyPropertyCollection Add<TEnum>(object name, TEnum defaultValue, int index = -1) where TEnum : Enum
            => Add(StaticListChoiceProperty.CreateForEnum(name, defaultValue), index);

        /// <inheritdoc cref="Add(PropertyCollection, object, IEnumerable)" />
        public MyPropertyCollection Add(Type enumType, object name, object defaultValue, int index = -1)
            => Add(StaticListChoiceProperty.CreateForEnum(enumType, name, defaultValue), index);

        /// <inheritdoc cref="Add(PropertyCollection, object, IEnumerable)" />
        public MyPropertyCollection Add<TEnum>(Type enumType, object name, TEnum defaultValue, bool readOnly, int index = -1) where TEnum : Enum
            => Add(StaticListChoiceProperty.CreateForEnum(enumType, name, defaultValue, readOnly), index);

        #endregion

        #region WithReadOnlyRule

        public MyPropertyCollection WithReadOnlyRule(PropertyCollectionRule rule)
        {
            rules.Add(rule);
            return this;
        }

        #endregion

        #region WithReadOnlyRule ReadOnlyBoundToBooleanRule

        public MyPropertyCollection WithReadOnlyRule(object targetPropertyName, object sourceBooleanPropertyName, bool inverse = false)
        {
            ReadOnlyBoundToBooleanRule rule = new ReadOnlyBoundToBooleanRule(targetPropertyName, sourceBooleanPropertyName, inverse);
            return WithReadOnlyRule(rule);
        }

        public MyPropertyCollection WithReadOnlyRule(Property targetProperty, BooleanProperty sourceProperty, bool inverse = false)
        {
            ReadOnlyBoundToBooleanRule rule = new ReadOnlyBoundToBooleanRule(targetProperty, sourceProperty, inverse);
            return WithReadOnlyRule(rule);
        }

        #endregion

        #region WithReadOnlyRule ReadOnlyBoundToValueRule

        public MyPropertyCollection WithReadOnlyRule<TValue, TSourceProperty>(object targetPropertyName, object sourcePropertyName, TValue[] valuesForReadOnly, bool inverse = false) where TSourceProperty : Property<TValue>
        {
            ReadOnlyBoundToValueRule<TValue, TSourceProperty> rule = new ReadOnlyBoundToValueRule<TValue, TSourceProperty>(targetPropertyName, sourcePropertyName, valuesForReadOnly, inverse);
            return WithReadOnlyRule(rule);
        }

        public MyPropertyCollection WithReadOnlyRule(object targetPropertyName, object sourcePropertyName, object valueForReadOnly, bool inverse = false)
        {
            if (valueForReadOnly == null)
            {
                throw new ArgumentNullException(nameof(valueForReadOnly));
            }
            Property sourceProperty = this[sourcePropertyName];
            Type genericType = typeof(ReadOnlyBoundToValueRule<,>).MakeGenericType(sourceProperty.ValueType, sourceProperty.GetType());
            PropertyCollectionRule rule = (PropertyCollectionRule)Activator.CreateInstance(genericType, targetPropertyName, sourcePropertyName, valueForReadOnly, inverse);
            return WithReadOnlyRule(rule);
        }

        public MyPropertyCollection WithReadOnlyRule(object targetPropertyName, object sourcePropertyName, IEnumerable valuesForReadOnly, bool inverse = false)
        {
            if (valuesForReadOnly == null)
            {
                throw new ArgumentNullException(nameof(valuesForReadOnly));
            }
            Property sourceProperty = this[sourcePropertyName];
            Type genericType = typeof(ReadOnlyBoundToValueRule<,>).MakeGenericType(sourceProperty.ValueType, sourceProperty.GetType());
            PropertyCollectionRule rule = (PropertyCollectionRule)Activator.CreateInstance(genericType, targetPropertyName, sourcePropertyName, valuesForReadOnly.Cast<object>().ToArray(), inverse);
            return WithReadOnlyRule(rule);
        }

        public MyPropertyCollection WithReadOnlyRule<TValue, TSourceProperty>(Property targetProperty, TSourceProperty sourceProperty, TValue valueForReadOnly, bool inverse = false) where TSourceProperty : Property<TValue>
        {
            ReadOnlyBoundToValueRule<TValue, TSourceProperty> rule = new ReadOnlyBoundToValueRule<TValue, TSourceProperty>(targetProperty, sourceProperty, valueForReadOnly, inverse);
            return WithReadOnlyRule(rule);
        }

        public MyPropertyCollection WithReadOnlyRule<TValue, TSourceProperty>(Property targetProperty, TSourceProperty sourceProperty, TValue[] valuesForReadOnly, bool inverse = false) where TSourceProperty : Property<TValue>
        {
            ReadOnlyBoundToValueRule<TValue, TSourceProperty> rule = new ReadOnlyBoundToValueRule<TValue, TSourceProperty>(targetProperty, sourceProperty, valuesForReadOnly, inverse);
            return WithReadOnlyRule(rule);
        }

        #endregion

        #region WithReadOnlyRule ReadOnlyBoundToNameValuesRule

        public MyPropertyCollection WithReadOnlyRule(Property targetProperty, TupleStruct<object, object>[] sourcePropertyNameValuePairs, bool inverse = false)
        {
            ReadOnlyBoundToNameValuesRule rule = new ReadOnlyBoundToNameValuesRule(targetProperty, inverse, sourcePropertyNameValuePairs);
            return WithReadOnlyRule(rule);
        }

        public MyPropertyCollection WithReadOnlyRule(Property targetProperty, IDictionary sourcePropertyNameValuePairs, bool inverse = false)
        {
            ReadOnlyBoundToNameValuesRule rule = new ReadOnlyBoundToNameValuesRule(targetProperty, inverse, FromDictionary(sourcePropertyNameValuePairs));
            return WithReadOnlyRule(rule);
        }

        public MyPropertyCollection WithReadOnlyRule(object targetPropertyName, TupleStruct<object, object>[] sourcePropertyNameValuePairs, bool inverse = false)
        {
            ReadOnlyBoundToNameValuesRule rule = new ReadOnlyBoundToNameValuesRule(targetPropertyName, inverse, sourcePropertyNameValuePairs);
            return WithReadOnlyRule(rule);
        }

        public MyPropertyCollection WithReadOnlyRule(object targetPropertyName, IDictionary sourcePropertyNameValuePairs, bool inverse = false)
        {
            ReadOnlyBoundToNameValuesRule rule = new ReadOnlyBoundToNameValuesRule(targetPropertyName, inverse, FromDictionary(sourcePropertyNameValuePairs));
            return WithReadOnlyRule(rule);
        }

        private static TupleStruct<object, object>[] FromDictionary(IDictionary rules)
        {
            List<TupleStruct<object, object>> result = new List<TupleStruct<object, object>>(rules.Count);
            foreach (DictionaryEntry pair in rules)
            {
                if (pair.Value is IEnumerable iterable)
                {
                    foreach (object value in iterable)
                    {
                        result.Add(TupleStruct.Create(pair.Key, value));
                    }
                }
                else
                {
                    result.Add(TupleStruct.Create(pair.Key, pair.Value));
                }
            }
            return result.ToArray();
        }

        #endregion

        #region Implicit operators

        public static implicit operator PropertyCollection(MyPropertyCollection list)
            => new PropertyCollection(list, list.Rules);

        public static implicit operator MyPropertyCollection(PropertyCollection collection)
            => new MyPropertyCollection(collection, collection.Rules);

        #endregion

        #region ToDictionary

        public Dictionary<object, object> ToDictionary() => Dictionary.ToDictionary(a => a, a => a.Value.Value, PropertyNameComparer.Instance);

        #endregion

        #region Update

        public void CopyCompatibleValuesFrom(MyPropertyCollection srcProps)
            => CopyCompatibleValuesFrom(srcProps, ignoreReadOnlyFlags: false);

        public void CopyCompatibleValuesFrom(MyPropertyCollection srcProps, bool ignoreReadOnlyFlags)
        {
            foreach (Property srcProp in srcProps)
            {
                Property property = this[srcProp.Name];
                if (property == null || !(property.ValueType == srcProp.ValueType))
                {
                    continue;
                }
                if (property.ReadOnly && ignoreReadOnlyFlags)
                {
                    using (property.InternalUseAsWritable())
                    {
                        property.Value = srcProp.Value;
                    }
                }
                else
                {
                    property.Value = srcProp.Value;
                }
            }
        }

        public bool IsValueChanged(object propertyName, object newValue)
            => !Dictionary[propertyName].Equals(newValue);

        #endregion

        #region PropertyNameComparer

        private sealed class PropertyNameComparer : EqualityComparer<object>
        {
            public static readonly PropertyNameComparer Instance = new PropertyNameComparer();

            private readonly StringComparer InnerComparer = StringComparer.Ordinal;

            private string ObjectToString(object obj) => obj?.ToString();

            public override bool Equals(object x, object y) => InnerComparer.Equals(ObjectToString(x), ObjectToString(y));

            public override int GetHashCode(object obj) => InnerComparer.GetHashCode(ObjectToString(obj));
        }

        #endregion
    }
}
