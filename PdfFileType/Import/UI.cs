// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using PaintDotNet;
using PaintDotNet.AppModel;

namespace PdfFileTypePlugin.Import;

internal static partial class UI
{
    private static readonly Lazy<Form?> MainFormLazy = new(() =>
    {
        IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
        Form? form = (Form?)Control.FromHandle(handle) ?? Application.OpenForms["MainForm"] ?? Application.OpenForms[0];
        Debug.WriteLineIf(form == null, "Can't get the MainForm");
        return form;
    });

    public static Form? MainForm => MainFormLazy.Value;

    public static Color BackColor { get; }

    public static Color ForeColor { get; }

    public static bool IsDarkMode { get; }

    static UI()
    {
        // get theme colors method 1
        (ForeColor, BackColor) = DummyForm.GetThemeColors.Value;

        // get theme colors method 2
        // ForeColor = MainForm.ForeColor;
        // BackColor = MainForm.BackColor;

        IsDarkMode = ForeColor.GetBrightness() > BackColor.GetBrightness();
    }

    public static Lazy<SizeF> DPI => new(() =>
    {
        using var g = Graphics.FromHwnd(IntPtr.Zero);
        return new SizeF(g.DpiX, g.DpiY);
    });

    public static PdnBaseForm? SaveConfigDialog
    {
        get => Application.OpenForms
            .OfType<PdnBaseForm>()
            .Where(form => form != MainForm)
            .Reverse()
            .FirstOrDefault(form => form.Descendants().AsParallel().OfType<SaveConfigWidget>().Any());
    }

    public static TResult? RunOnUIThread<TResult>(Func<TResult> d, bool waitForComplete = true)
    {
        IUISynchronizationContext ctx = Services.Get<IUISynchronizationContext>();
        if (ctx.IsOnUIThread)
        {
            return d();
        }
        TResult? result = default;
        Exception? error = null;
        Action<SendOrPostCallback, object?> cb = waitForComplete ? ctx.Send : ctx.Post;
        cb(new SendOrPostCallback(_ =>
        {
            try
            {
                result = d();
            }
            catch (Exception ex)
            {
                error = ex;
            }
        }), null);

        if (error != null)
        {
            throw error;
        }
        return result;
    }

    public static void RunOnUIThread(Action d, bool waitForComplete = true)
    {
        IUISynchronizationContext ctx = Services.Get<IUISynchronizationContext>();
        if (ctx.IsOnUIThread)
        {
            d();
            return;
        }
        Exception? error = null;
        Action<SendOrPostCallback, object?> cb = waitForComplete ? ctx.Send : ctx.Post;
        cb(new SendOrPostCallback(_ =>
        {
            try
            {
                d();
            }
            catch (Exception ex)
            {
                error = ex;
            }
        }), null);

        if (error != null)
        {
            throw error;
        }
    }

    #region Get Theme Colors Method 1

    private sealed class DummyForm : PdnBaseForm
    {
        public static Lazy<(Color, Color)> GetThemeColors = new(() =>
        {
            using var form = new DummyForm();
            return (form.ForeColor, form.BackColor);
        });

        private DummyForm()
        {
            UseAppThemeColors = true;
        }
    }

    #endregion
}
