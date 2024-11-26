// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PdfFileTypePlugin.Extensions;

internal static class ControlExtensions
{
    public static IEnumerable<Control> Descendants(this Control root)
    {
        foreach (Control control in root.Controls)
        {
            yield return control;
            if (control.HasChildren)
            {
                foreach (Control child in Descendants(control))
                {
                    yield return child;
                }
            }
        }
    }

    public static void RunOnUIThread<TAction>(this Control control, TAction action, params object[] args) where TAction : Delegate
    {
        if (control.InvokeRequired)
        {
            control.Invoke(action, args);
        }
        else
        {
            action.DynamicInvoke(args);
        }
    }

    public static TResult? RunOnUIThread<TResult>(this Control control, Func<Control, TResult> action)
    {
        return (TResult?)(control.InvokeRequired ? control.Invoke(action) : action.Invoke(control));
    }
}
