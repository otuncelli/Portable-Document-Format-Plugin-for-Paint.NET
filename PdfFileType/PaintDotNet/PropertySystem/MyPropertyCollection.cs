// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PaintDotNet.PropertySystem;

public sealed class MyPropertyCollection : IEnumerable<Property>, IEnumerable, ICloneable
{
    #region Fields

    private readonly KeyedPropertyCollection props = new KeyedPropertyCollection();

    private readonly List<PropertyCollectionRule> rules = new List<PropertyCollectionRule>();

    #endregion

    #region Properties

    public IReadOnlyList<PropertyCollectionRule> Rules => rules;

    public IReadOnlyList<Property> Properties => props;

    public IReadOnlyList<PropertyName> PropertyNames => props.Select(p => (PropertyName)p.Name).ToList();

    public Property this[PropertyName propertyName] => props[propertyName];

    public int Count => props.Count;

    Property this[int index] => GetPropertyAt(index);

    #endregion

    #region Constructors

    public MyPropertyCollection(IEnumerable<Property> props, IEnumerable<PropertyCollectionRule> rules)
    {
        if (props == null) { return; }

        foreach (Property prop in props)
        {
            this.props.Add(prop.Clone());
        }

        if (rules != null)
        {
            foreach (PropertyCollectionRule rule in rules)
            {
                this.rules.Add(rule.Clone());
            }
        }
    }

    public MyPropertyCollection(IEnumerable<Property> props)
        : this(props, null)
    {
    }

    public MyPropertyCollection()
        : this(null, null)
    {
    }

    #endregion

    #region Int32Property

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
    public MyPropertyCollection AddInt32(PropertyName name)
        => _Add(new Int32Property(name));

    /// <inheritdoc cref="AddInt32(PropertyName)" />
    public MyPropertyCollection AddInt32(PropertyName name, int defaultValue, int minValue, int maxValue, bool readOnly, ValueValidationFailureResult vvfResult)
        => _Add(new Int32Property(name, defaultValue, minValue, maxValue, readOnly, vvfResult));

    /// <inheritdoc cref="AddInt32(PropertyName)" />
    public MyPropertyCollection AddInt32(PropertyName name, int defaultValue, int minValue, int maxValue, bool readOnly)
        => _Add(new Int32Property(name, defaultValue, minValue, maxValue, readOnly));

    /// <inheritdoc cref="AddInt32(PropertyName)" />
    public MyPropertyCollection AddInt32(PropertyName name, int defaultValue, int minValue, int maxValue)
        => _Add(new Int32Property(name, defaultValue, minValue, maxValue));

    /// <inheritdoc cref="AddInt32(PropertyName)" />
    public MyPropertyCollection AddInt32(PropertyName name, int defaultValue)
        => _Add(new Int32Property(name, defaultValue));

    /// <inheritdoc cref="AddInt32(PropertyName)" />
    public MyPropertyCollection AddInt32(PropertyName name, ColorBgra defaultValue, bool readOnly, bool alpha = false)
    {
        GetColorWheelValues(defaultValue, alpha, out int min, out int max, out int def);
        return AddInt32(name, def, min, max, readOnly);
    }

    /// <inheritdoc cref="AddInt32(PropertyName, int)" />
    public MyPropertyCollection AddInt32(PropertyName name, ColorBgra defaultValue, bool alpha = false)
    {
        GetColorWheelValues(defaultValue, alpha, out int min, out int max, out int def);
        return AddInt32(name, def, min, max);
    }

    #endregion

    #region DoubleProperty

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
    public MyPropertyCollection AddDouble(PropertyName name)
        => _Add(new DoubleProperty(name));

    /// <inheritdoc cref="AddDouble(PropertyName)" />
    public MyPropertyCollection AddDouble(PropertyName name, double defaultValue, double minValue, double maxValue, bool readOnly, ValueValidationFailureResult vvfResult)
        => _Add(new DoubleProperty(name, defaultValue, minValue, maxValue, readOnly, vvfResult));

    /// <inheritdoc cref="AddDouble(PropertyName)" />
    public MyPropertyCollection AddDouble(PropertyName name, double defaultValue, double minValue, double maxValue, bool readOnly)
        => _Add(new DoubleProperty(name, defaultValue, minValue, maxValue, readOnly));

    /// <inheritdoc cref="AddDouble(PropertyName)" />
    public MyPropertyCollection AddDouble(PropertyName name, double defaultValue, double minValue, double maxValue)
        => _Add(new DoubleProperty(name, defaultValue, minValue, maxValue));

    /// <inheritdoc cref="AddDouble(PropertyName)" />
    public MyPropertyCollection AddDouble(PropertyName name, double defaultValue)
        => _Add(new DoubleProperty(name, defaultValue));

    #endregion

    #region BooleanProperty

    /// <summary>
    /// Adds <see cref="BooleanProperty" />
    /// </summary>
    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.CheckBox" />
    /// </remarks>
    public MyPropertyCollection AddBoolean(PropertyName name)
        => _Add(new BooleanProperty(name));

    /// <inheritdoc cref="AddBoolean(PropertyName)" />
    public MyPropertyCollection AddBoolean(PropertyName name, bool defaultValue, bool readOnly, ValueValidationFailureResult vvfResult)
        => _Add(new BooleanProperty(name, defaultValue, readOnly, vvfResult));

    /// <inheritdoc cref="AddBoolean(PropertyName)" />
    public MyPropertyCollection AddBoolean(PropertyName name, bool defaultValue, bool readOnly)
        => _Add(new BooleanProperty(name, defaultValue, readOnly));

    /// <inheritdoc cref="AddBoolean(PropertyName)" />
    public MyPropertyCollection AddBoolean(PropertyName name, bool defaultValue)
        => _Add(new BooleanProperty(name, defaultValue));

    #endregion

    #region DoubleVectorProperty

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
    public MyPropertyCollection AddDoubleVector(PropertyName name)
        => _Add(new DoubleVectorProperty(name));

    /// <inheritdoc cref="AddDoubleVector(PropertyName)" />
    public MyPropertyCollection AddDoubleVector(PropertyName name, Pair<double, double> defaultValues, Pair<double, double> minValues, Pair<double, double> maxValues, bool readOnly, ValueValidationFailureResult vvfResult)
        => _Add(new DoubleVectorProperty(name, defaultValues, minValues, maxValues, readOnly, vvfResult));

    /// <inheritdoc cref="AddDoubleVector(PropertyName)" />
    public MyPropertyCollection AddDoubleVector(PropertyName name, Pair<double, double> defaultValues, Pair<double, double> minValues, Pair<double, double> maxValues, bool readOnly)
        => _Add(new DoubleVectorProperty(name, defaultValues, minValues, maxValues, readOnly));

    /// <inheritdoc cref="AddDoubleVector(PropertyName)" />
    public MyPropertyCollection AddDoubleVector(PropertyName name, Pair<double, double> defaultValues, Pair<double, double> minValues, Pair<double, double> maxValues)
        => _Add(new DoubleVectorProperty(name, defaultValues, minValues, maxValues));

    /// <inheritdoc cref="AddDoubleVector(PropertyName)" />
    public MyPropertyCollection AddDoubleVector(PropertyName name, Pair<double, double> defaultValues)
        => _Add(new DoubleVectorProperty(name, defaultValues));

    #endregion

    #region DoubleVector3Property

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
    public MyPropertyCollection AddDoubleVector3(PropertyName name)
        => _Add(new DoubleVector3Property(name));

    /// <inheritdoc cref="AddDoubleVector3(PropertyName)" />
    public MyPropertyCollection AddDoubleVector3(PropertyName name, Tuple<double, double, double> defaultValues, Tuple<double, double, double> minValues, Tuple<double, double, double> maxValues, bool readOnly, ValueValidationFailureResult vvfResult)
        => _Add(new DoubleVector3Property(name, defaultValues, minValues, maxValues, readOnly, vvfResult));

    /// <inheritdoc cref="AddDoubleVector3(PropertyName)" />
    public MyPropertyCollection AddDoubleVector3(PropertyName name, Tuple<double, double, double> defaultValues, Tuple<double, double, double> minValues, Tuple<double, double, double> maxValues, bool readOnly)
        => _Add(new DoubleVector3Property(name, defaultValues, minValues, maxValues, readOnly));

    /// <inheritdoc cref="AddDoubleVector3(PropertyName)" />
    public MyPropertyCollection AddDoubleVector3(PropertyName name, Tuple<double, double, double> defaultValues, Tuple<double, double, double> minValues, Tuple<double, double, double> maxValues)
        => _Add(new DoubleVector3Property(name, defaultValues, minValues, maxValues));

    /// <inheritdoc cref="AddDoubleVector3(PropertyName)" />
    public MyPropertyCollection AddDoubleVector3(PropertyName name, Tuple<double, double, double> defaultValues)
        => _Add(new DoubleVector3Property(name, defaultValues));

    #endregion

    #region StringProperty

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
    public MyPropertyCollection AddString(PropertyName name)
        => _Add(new StringProperty(name));

    /// <inheritdoc cref="AddString(PropertyName)" />
    public MyPropertyCollection AddString(PropertyName name, string defaultValue, int maxLength, bool readOnly, ValueValidationFailureResult vvfResult)
        => _Add(new StringProperty(name, defaultValue, maxLength, readOnly, vvfResult));

    /// <inheritdoc cref="AddString(PropertyName)" />
    public MyPropertyCollection AddString(PropertyName name, string defaultValue, int maxLength, bool readOnly)
        => _Add(new StringProperty(name, defaultValue, maxLength, readOnly));

    /// <inheritdoc cref="AddString(PropertyName)" />
    public MyPropertyCollection AddString(PropertyName name, string defaultValue, int maxLength)
        => _Add(new StringProperty(name, defaultValue, maxLength));

    /// <inheritdoc cref="AddString(PropertyName)" />
    public MyPropertyCollection AddString(PropertyName name, string defaultValue)
        => _Add(new StringProperty(name, defaultValue));

    #endregion

    #region UriProperty

    /// <summary>
    /// Adds <see cref="UriProperty" />
    /// </summary>
    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.LinkLabel" />
    /// </remarks>
    public MyPropertyCollection AddUri(PropertyName name)
        => _Add(new UriProperty(name));

    /// <inheritdoc cref="AddUri(PropertyName)" />
    public MyPropertyCollection AddUri(PropertyName name, Uri defaultValue, bool readOnly, ValueValidationFailureResult vvfResult)
        => _Add(new UriProperty(name, defaultValue, readOnly, vvfResult));

    /// <inheritdoc cref="AddUri(PropertyName)" />
    public MyPropertyCollection AddUri(PropertyName name, Uri defaultValue, bool readOnly)
        => _Add(new UriProperty(name, defaultValue, readOnly));

    /// <inheritdoc cref="AddUri(PropertyName)" />
    public MyPropertyCollection AddUri(PropertyName name, Uri defaultValue)
        => _Add(new UriProperty(name, defaultValue));

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
    public MyPropertyCollection AddStaticListChoice(PropertyName name, IEnumerable valueChoices)
        => _Add(new StaticListChoiceProperty(name, valueChoices.Cast<object>().ToArray()));

    /// <inheritdoc cref="AddStaticListChoice(PropertyName, IEnumerable)" />
    public MyPropertyCollection AddStaticListChoice(PropertyName name, IEnumerable valueChoices, int defaultChoiceIndex, bool readOnly, ValueValidationFailureResult vvfResult)
        => _Add(new StaticListChoiceProperty(name, valueChoices.Cast<object>().ToArray(), defaultChoiceIndex, readOnly, vvfResult));

    /// <inheritdoc cref="AddStaticListChoice(PropertyName, IEnumerable)" />
    public MyPropertyCollection AddStaticListChoice(PropertyName name, IEnumerable valueChoices, int defaultChoiceIndex, bool readOnly)
        => _Add(new StaticListChoiceProperty(name, valueChoices.Cast<object>().ToArray(), defaultChoiceIndex, readOnly));

    /// <inheritdoc cref="AddStaticListChoice(PropertyName, IEnumerable)" />
    public MyPropertyCollection AddStaticListChoice(PropertyName name, IEnumerable valueChoices, int defaultChoiceIndex)
        => _Add(new StaticListChoiceProperty(name, valueChoices.Cast<object>().ToArray(), defaultChoiceIndex));

    /// <inheritdoc cref="AddStaticListChoice(PropertyName, IEnumerable)" />
    public MyPropertyCollection AddStaticListChoice<TEnum>(PropertyName name, TEnum defaultValue, IComparer comparer = null) where TEnum : Enum
        => AddStaticListChoice(typeof(TEnum), name, defaultValue, readOnly: false, comparer: comparer);

    /// <inheritdoc cref="AddStaticListChoice(PropertyName, IEnumerable)" />
    public MyPropertyCollection AddStaticListChoice<TEnum>(PropertyName name, TEnum defaultValue, bool readOnly, IComparer comparer = null) where TEnum : Enum
        => AddStaticListChoice(typeof(TEnum), name, defaultValue, readOnly, comparer: comparer);

    /// <inheritdoc cref="AddStaticListChoice(PropertyName, IEnumerable)" />
    public MyPropertyCollection AddStaticListChoice(Type enumType, PropertyName name, object defaultValue, IComparer comparer = null)
        => AddStaticListChoice(enumType, name, defaultValue, readOnly: false, comparer: comparer);

    /// <inheritdoc cref="AddStaticListChoice(PropertyName, IEnumerable)" />
    public MyPropertyCollection AddStaticListChoice(Type enumType, PropertyName name, object defaultValue, bool readOnly, IComparer comparer = null)
    {
        if (enumType.GetCustomAttributes(typeof(FlagsAttribute), inherit: true).Length != 0)
        {
            throw new ArgumentException($"Enums with '{nameof(FlagsAttribute)}' are not supported");
        }

        if (!Enum.IsDefined(enumType, defaultValue))
        {
            throw new ArgumentOutOfRangeException($"{nameof(defaultValue)} '{defaultValue}' is not a valid enum value for '{enumType.FullName}'");
        }

        Array values = Enum.GetValues(enumType);

        if (comparer != null)
        {
            Array.Sort(values, comparer);
        }

        int defaultValueIndex = Array.IndexOf(values, defaultValue);

        object[] array = new object[values.Length];
        values.CopyTo(array, 0);

        Property property = new StaticListChoiceProperty(name, array, defaultValueIndex, readOnly);
        return _Add(property);
    }

    #endregion

    #region WithReadOnlyRule

    public MyPropertyCollection WithReadOnlyRule(PropertyCollectionRule rule)
    {
        rules.Add(rule.Clone());
        return this;
    }

    #endregion

    #region WithReadOnlyRule/ReadOnlyBoundToBooleanRule

    public MyPropertyCollection WithReadOnlyRule(PropertyName targetPropertyName, PropertyName sourceBooleanPropertyName, bool inverse = false)
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

    #region WithReadOnlyRule/ReadOnlyBoundToValueRule

    public MyPropertyCollection WithReadOnlyRule<TValue, TSourceProperty>(PropertyName targetPropertyName, PropertyName sourcePropertyName, TValue[] valuesForReadOnly, bool inverse = false) where TSourceProperty : Property<TValue>
    {
        ReadOnlyBoundToValueRule<TValue, TSourceProperty> rule = new ReadOnlyBoundToValueRule<TValue, TSourceProperty>(targetPropertyName, sourcePropertyName, valuesForReadOnly, inverse);
        return WithReadOnlyRule(rule);
    }

    public MyPropertyCollection WithReadOnlyRule(PropertyName targetPropertyName, PropertyName sourcePropertyName, object valueForReadOnly, bool inverse = false)
    {
        if (valueForReadOnly == null)
        {
            throw new ArgumentNullException(nameof(valueForReadOnly));
        }
        Property sourceProperty = props[sourcePropertyName];
        Type genericType = typeof(ReadOnlyBoundToValueRule<,>).MakeGenericType(sourceProperty.ValueType, sourceProperty.GetType());
        PropertyCollectionRule rule = (PropertyCollectionRule)Activator.CreateInstance(genericType, targetPropertyName, sourcePropertyName, valueForReadOnly, inverse);
        return WithReadOnlyRule(rule);
    }

    public MyPropertyCollection WithReadOnlyRule(PropertyName targetPropertyName, PropertyName sourcePropertyName, IEnumerable valuesForReadOnly, bool inverse = false)
    {
        if (valuesForReadOnly == null)
        {
            throw new ArgumentNullException(nameof(valuesForReadOnly));
        }
        Property sourceProperty = props[sourcePropertyName];
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

    #region WithReadOnlyRule/ReadOnlyBoundToNameValuesRule

    public MyPropertyCollection WithReadOnlyRule(Property targetProperty, ValueTuple<object, object>[] sourcePropertyNameValuePairs, bool inverse = false)
    {
        ReadOnlyBoundToNameValuesRule rule = new ReadOnlyBoundToNameValuesRule(targetProperty, inverse, sourcePropertyNameValuePairs);
        return WithReadOnlyRule(rule);
    }

    public MyPropertyCollection WithReadOnlyRule(Property targetProperty, IDictionary sourcePropertyNameValuePairs, bool inverse = false)
    {
        ReadOnlyBoundToNameValuesRule rule = new ReadOnlyBoundToNameValuesRule(targetProperty, inverse, FromDictionary(sourcePropertyNameValuePairs));
        return WithReadOnlyRule(rule);
    }

    public MyPropertyCollection WithReadOnlyRule(object targetPropertyName, ValueTuple<object, object>[] sourcePropertyNameValuePairs, bool inverse = false)
    {
        ReadOnlyBoundToNameValuesRule rule = new ReadOnlyBoundToNameValuesRule(targetPropertyName, inverse, sourcePropertyNameValuePairs);
        return WithReadOnlyRule(rule);
    }

    public MyPropertyCollection WithReadOnlyRule(object targetPropertyName, IDictionary sourcePropertyNameValuePairs, bool inverse = false)
    {
        ReadOnlyBoundToNameValuesRule rule = new ReadOnlyBoundToNameValuesRule(targetPropertyName, inverse, FromDictionary(sourcePropertyNameValuePairs));
        return WithReadOnlyRule(rule);
    }

    private static ValueTuple<object, object>[] FromDictionary(IDictionary rules)
    {
        List<ValueTuple<object, object>> result = new List<ValueTuple<object, object>>(rules.Count);
        foreach (DictionaryEntry pair in rules)
        {
            if (pair.Value is IEnumerable iterable)
            {
                foreach (object value in iterable)
                {
                    result.Add(ValueTuple.Create(pair.Key, value));
                }
            }
            else
            {
                result.Add(ValueTuple.Create(pair.Key, pair.Value));
            }
        }
        return result.ToArray();
    }

    #endregion

    #region Other Methods

#pragma warning disable IDE1006 // Naming Styles
    private MyPropertyCollection _Add(Property property)
#pragma warning restore IDE1006 // Naming Styles
    {
        props.Add(property);
        return this;
    }

    private static void GetColorWheelValues(ColorBgra defaultValue, bool alpha, out int min, out int max, out int def)
    {
        if (alpha)
        {
            min = int.MinValue;
            max = int.MaxValue;
            def = unchecked((int)defaultValue.Bgra);
        }
        else
        {
            min = 0;
            max = (1 << 24) - 1;
            def = ColorBgra.ToOpaqueInt32(defaultValue);
        }
    }

    public MyPropertyCollection Add(Property property) => _Add(property.Clone());

    public Property GetPropertyAt(int index) => ((Collection<Property>)props)[index];

    public object GetPropertyValue(PropertyName propertyName) => this[propertyName].Value;

    public T GetPropertyValue<T>(PropertyName propertyName) => (T)GetPropertyValue(propertyName);

    public Dictionary<PropertyName, object> ToDictionary() => props.ToDictionary(a => (PropertyName)a.Name, a => a.Value, EqualityComparer<PropertyName>.Default);

    #endregion

    #region Implicit operators

    public static implicit operator PropertyCollection(MyPropertyCollection list) => new PropertyCollection(list, list.Rules);

    public static implicit operator MyPropertyCollection(PropertyCollection collection) => new MyPropertyCollection(collection, collection.Rules);

    #endregion

    #region ICloneable

    public object Clone() => new MyPropertyCollection(this, Rules);

    #endregion

    #region IEnumerable

    public IEnumerator<Property> GetEnumerator() => props.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion

    #region KeyedPropertyCollection

    private sealed class KeyedPropertyCollection : KeyedCollection<PropertyName, Property>
    {
        protected override PropertyName GetKeyForItem(Property item) => (PropertyName)item.Name;
    }

    #endregion
}
