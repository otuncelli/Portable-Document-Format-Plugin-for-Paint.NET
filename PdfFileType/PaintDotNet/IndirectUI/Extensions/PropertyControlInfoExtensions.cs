// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using PaintDotNet.PropertySystem;
using System;
using System.Drawing;
using System.IO;

namespace PaintDotNet.IndirectUI.Extensions;

public static class PropertyControlInfoExtensions
{
    private static PropertyControlInfo SetPropertyControlValue(this PropertyControlInfo pci, ControlInfoPropertyNames controlPropertyName, object controlPropertyValue)
    {
        Property property = pci.ControlProperties[controlPropertyName]
            ?? throw new ArgumentException($"Can not find property: {controlPropertyName}.", nameof(controlPropertyName));
        property.Value = controlPropertyValue;
        return pci;
    }

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.FileChooser" />
    /// </remarks>
    public static PropertyControlInfo AllowAllFiles(this PropertyControlInfo pci, bool value = true)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.AllowAllFiles, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.IncrementButton" />
    /// </remarks>
    public static PropertyControlInfo ButtonText(this PropertyControlInfo pci, string value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.ButtonText, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.Slider" /> using <see cref="Int32Property"/> or <see cref="DoubleProperty"/>
    /// </remarks>
    public static PropertyControlInfo ControlColors(this PropertyControlInfo pci, ColorBgra[] values)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.ControlColors, values);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.Slider" /> using <see cref="Int32Property"/> or <see cref="DoubleProperty"/>
    /// </remarks>
    public static PropertyControlInfo ControlStyle(this PropertyControlInfo pci, SliderControlStyle value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.ControlStyle, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.Slider" /> using <see cref="DoubleProperty"/> or <see cref="DoubleVectorProperty"/>
    /// </remarks>
    public static PropertyControlInfo DecimalPlaces(this PropertyControlInfo pci, int value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.DecimalPlaces, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>: All
    /// </remarks>
    public static PropertyControlInfo Description(this PropertyControlInfo pci, string value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.Description, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>: All
    /// </remarks>
    public static PropertyControlInfo DisplayName(this PropertyControlInfo pci, string value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.DisplayName, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.FileChooser" />
    /// </remarks>
    public static PropertyControlInfo FileTypes(this PropertyControlInfo pci, params string[] values)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.FileTypes, values);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.CheckBox" />
    /// </remarks>
    public static PropertyControlInfo Footnote(this PropertyControlInfo pci, string value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.Footnote, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.TextBox" />
    /// </remarks>
    public static PropertyControlInfo Multiline(this PropertyControlInfo pci, bool value = true)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.Multiline, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.Slider" /> using <see cref="Int32Property"/> or <see cref="DoubleProperty"/>
    /// </remarks>
    public static PropertyControlInfo RangeWraps(this PropertyControlInfo pci, bool value = true)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.RangeWraps, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.Slider" />,
    /// <see cref="PropertyControlType.AngleChooser" />,
    /// <see cref="PropertyControlType.PanAndSlider" />,
    /// <see cref="PropertyControlType.RollBallAndSliders" />,
    /// <see cref="PropertyControlType.ColorWheel" /> 
    /// </remarks>
    public static PropertyControlInfo ShowResetButton(this PropertyControlInfo pci, bool value = true)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.ShowResetButton, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.Slider" /> using <see cref="Int32Property"/> or <see cref="DoubleProperty"/>
    /// </remarks>
    public static PropertyControlInfo SliderLargeChange(this PropertyControlInfo pci, double value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.SliderLargeChange, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.Slider" /> using <see cref="DoubleVectorProperty"/> or <see cref="DoubleVector3Property"/>,
    /// <see cref="PropertyControlType.PanAndSlider" />,
    /// <see cref="PropertyControlType.RollBallAndSliders" />
    /// </remarks>
    public static PropertyControlInfo SliderLargeChangeX(this PropertyControlInfo pci, double value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.SliderLargeChangeX, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.Slider" /> using <see cref="DoubleVectorProperty"/> or <see cref="DoubleVector3Property"/>,
    /// <see cref="PropertyControlType.PanAndSlider" />,
    /// <see cref="PropertyControlType.RollBallAndSliders" />
    /// </remarks>
    public static PropertyControlInfo SliderLargeChangeY(this PropertyControlInfo pci, double value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.SliderLargeChangeY, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.Slider" /> using <see cref="DoubleVector3Property"/>,
    /// <see cref="PropertyControlType.RollBallAndSliders"/>
    /// </remarks>
    public static PropertyControlInfo SliderLargeChangeZ(this PropertyControlInfo pci, double value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.SliderLargeChangeZ, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.Slider" /> using <see cref="Int32Property"/>
    /// </remarks>
    public static PropertyControlInfo SliderShowTickMarks(this PropertyControlInfo pci, bool value = true)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.SliderShowTickMarks, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.Slider" /> using <see cref="DoubleVectorProperty"/> or <see cref="DoubleVector3Property"/>,
    /// <see cref="PropertyControlType.PanAndSlider" />,
    /// <see cref="PropertyControlType.RollBallAndSliders" />
    /// </remarks>
    public static PropertyControlInfo SliderShowTickMarksX(this PropertyControlInfo pci, bool value = true)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.SliderShowTickMarksX, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.Slider" /> using <see cref="DoubleVectorProperty"/> or <see cref="DoubleVector3Property"/>,
    /// <see cref="PropertyControlType.PanAndSlider" />,
    /// <see cref="PropertyControlType.RollBallAndSliders" />
    /// </remarks>
    public static PropertyControlInfo SliderShowTickMarksY(this PropertyControlInfo pci, bool value = true)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.SliderShowTickMarksY, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.Slider" /> using <see cref="DoubleVector3Property"/>,
    /// <see cref="PropertyControlType.RollBallAndSliders" />
    /// </remarks>
    public static PropertyControlInfo SliderShowTickMarksZ(this PropertyControlInfo pci, bool value = true)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.SliderShowTickMarksZ, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.Slider" /> using <see cref="Int32Property"/> or <see cref="DoubleProperty"/>
    /// </remarks>
    public static PropertyControlInfo SliderSmallChange(this PropertyControlInfo pci, double value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.SliderSmallChange, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.Slider" /> using <see cref="DoubleVectorProperty"/> or <see cref="DoubleVector3Property"/>,
    /// <see cref="PropertyControlType.PanAndSlider" />,
    /// <see cref="PropertyControlType.RollBallAndSliders" />
    /// </remarks>
    public static PropertyControlInfo SliderSmallChangeX(this PropertyControlInfo pci, double value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.SliderSmallChangeX, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.Slider" /> using <see cref="DoubleVectorProperty"/> or <see cref="DoubleVector3Property"/>,
    /// <see cref="PropertyControlType.PanAndSlider" />,
    /// <see cref="PropertyControlType.RollBallAndSliders" />
    public static PropertyControlInfo SliderSmallChangeY(this PropertyControlInfo pci, double value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.SliderSmallChangeY, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.Slider" /> using <see cref="DoubleVector3Property"/>,
    /// <see cref="PropertyControlType.RollBallAndSliders"/>
    /// </remarks>
    public static PropertyControlInfo SliderSmallChangeZ(this PropertyControlInfo pci, double value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.SliderSmallChangeZ, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="DoubleVectorProperty"/>,
    /// </remarks>
    public static PropertyControlInfo StaticImageUnderlay(this PropertyControlInfo pci, ImageResource value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.StaticImageUnderlay, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="DoubleVectorProperty"/>,
    /// </remarks>
    public static PropertyControlInfo StaticImageUnderlay(this PropertyControlInfo pci, byte[] bytes)
    {
        using (var ms = new MemoryStream(bytes))
        {
            pci.StaticImageUnderlay(ImageResource.FromImage(Image.FromStream(ms)));
        }
        return pci;
    }

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="DoubleVectorProperty"/>,
    /// </remarks>
    public static PropertyControlInfo StaticImageUnderlay(this PropertyControlInfo pci, string base64string)
        => pci.StaticImageUnderlay(Convert.FromBase64String(base64string));

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.Slider" /> using <see cref="Int32Property"/> or <see cref="DoubleProperty"/>,
    /// <see cref="PropertyControlType.AngleChooser" />
    /// </remarks>
    public static PropertyControlInfo UpDownIncrement(this PropertyControlInfo pci, double value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.UpDownIncrement, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.Slider" /> using <see cref="DoubleVectorProperty"/> or <see cref="DoubleVector3Property"/>,
    /// <see cref="PropertyControlType.PanAndSlider" />,
    /// <see cref="PropertyControlType.RollBallAndSliders" />
    /// </remarks>
    public static PropertyControlInfo UpDownIncrementX(this PropertyControlInfo pci, double value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.UpDownIncrementX, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.Slider" /> using <see cref="DoubleVectorProperty"/> or <see cref="DoubleVector3Property"/>,
    /// <see cref="PropertyControlType.PanAndSlider" />,
    /// <see cref="PropertyControlType.RollBallAndSliders" />
    /// </remarks>
    public static PropertyControlInfo UpDownIncrementY(this PropertyControlInfo pci, double value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.UpDownIncrementY, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.Slider" /> using <see cref="DoubleVector3Property"/>,
    /// <see cref="PropertyControlType.RollBallAndSliders"/>
    /// </remarks>
    public static PropertyControlInfo UpDownIncrementZ(this PropertyControlInfo pci, double value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.UpDownIncrementZ, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="PropertyControlType.Slider" /> using <see cref="DoubleProperty"/> or <see cref="DoubleVectorProperty"/>
    /// </remarks>
    public static PropertyControlInfo UseExponentialScale(this PropertyControlInfo pci, bool value = true)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.UseExponentialScale, value);

    public static PropertyControlInfo WindowHelpContent(this PropertyControlInfo pci, string value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.WindowHelpContent, value);

    public static PropertyControlInfo WindowHelpContentType(this PropertyControlInfo pci, WindowHelpContentType value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.WindowHelpContentType, value);

    public static PropertyControlInfo WindowIsSizable(this PropertyControlInfo pci, bool value = true)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.WindowIsSizable, value);

    public static PropertyControlInfo WindowTitle(this PropertyControlInfo pci, string value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.WindowTitle, value);

    public static PropertyControlInfo WindowWidthScale(this PropertyControlInfo pci, double value)
        => pci.SetPropertyControlValue(ControlInfoPropertyNames.WindowWidthScale, value);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="StaticListChoiceProperty" />
    /// </remarks>
    public static PropertyControlInfo ValueDisplayName(this PropertyControlInfo pci, object value, string displayName)
    {
        pci.SetValueDisplayName(value, displayName);
        return pci;
    }

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="StaticListChoiceProperty" />
    /// </remarks>
    public static PropertyControlInfo ValueDisplayName<T>(this PropertyControlInfo pci, T value, string displayName) where T: notnull
        => pci.ValueDisplayName((object)value, displayName);

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="StaticListChoiceProperty" />
    /// </remarks>
    public static PropertyControlInfo ValueDisplayNameCallback<T>(this PropertyControlInfo pci, Func<T, string> displayNameGetter)
        => pci.ValueDisplayNameCallback(o => displayNameGetter((T)o));

    /// <remarks>
    /// <strong>Supported Controls</strong>:
    /// <see cref="StaticListChoiceProperty" />
    /// </remarks>
    public static PropertyControlInfo ValueDisplayNameCallback(this PropertyControlInfo pci, Func<object, string> displayNameGetter)
    {
        if (pci.Property is not StaticListChoiceProperty property)
        {
            throw new ArgumentException($"Property must be {nameof(StaticListChoiceProperty)}", nameof(pci));
        }

        Array.ForEach(property.ValueChoices, choice => pci.SetValueDisplayName(choice, displayNameGetter(choice)));
        return pci;
    }

    /// <remarks>
    /// <strong>Supported Controls</strong>: All
    /// </remarks>
    public static PropertyControlInfo ControlType(this PropertyControlInfo pci, PropertyControlType newControlType)
    {
        if (Array.IndexOf(pci.ControlType.ValueChoices, newControlType) == -1)
        {
            throw new ArgumentException($"Can not set control type of {newControlType} for property: {pci.Property.Name}.", nameof(newControlType));
        }
        pci.ControlType.Value = newControlType;
        return pci;
    }

    public static PropertyControlInfo OnValueChanged(this PropertyControlInfo pci, ValueEventHandler<object> handler)
    {
        pci.Property.ValueChanged += handler;
        return pci;
    }

    public static PropertyControlInfo OnReadOnlyChanged(this PropertyControlInfo pci, ValueEventHandler<bool> handler)
    {
        pci.Property.ReadOnlyChanged += handler;
        return pci;
    }
}
