// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PaintDotNet;
using PaintDotNet.Threading;

namespace PdfFileTypePlugin.Import
{
    internal static partial class UI
    {
        private static readonly Lazy<Form> MainFormLazy = new Lazy<Form>(() =>
        {
            IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
            Form form = (Form)Control.FromHandle(handle) ?? Application.OpenForms["MainForm"] ?? Application.OpenForms[0];
            Debug.WriteLineIf(form == null, "Can't get the MainForm");
            return form;
        });

        public static Form MainForm => MainFormLazy.Value;

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

        public static Lazy<SizeF> DPI => new Lazy<SizeF>(() =>
        {
            using (var g = Graphics.FromHwnd(IntPtr.Zero))
            {
                return new SizeF(g.DpiX, g.DpiY);
            }
        });

        public static PdnBaseForm SaveConfigDialog
        {
            get => Application.OpenForms
                .OfType<PdnBaseForm>()
                .Where(form => form != MainForm)
                .Reverse()
                .FirstOrDefault(form => form.Descendants().AsParallel().OfType<SaveConfigWidget>().Any());
        }

        public static T Invoke<T>(Control control, Func<T> action) => control.InvokeRequired ? (T)control.Invoke(action) : action();

        public static void InvokeNoReturn(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        public static T Invoke<T>(Form owner, Func<IWin32Window, T> action) => owner.InvokeRequired ? (T)owner.Invoke(action, owner) : action(owner);

        public static void Invoke(Form owner, Action<IWin32Window> action)
        {
            if (owner.InvokeRequired)
            {
                owner.Invoke(action, owner);
            }
            else
            {
                action(owner);
            }
        }

        #region Root Invoke Method 1

        private static void ThrowIfNotNull(Exception ex)
        {
            if (ex != null) { throw ex; }
        }

        public static T Invoke<T>(Func<T> action)
        {
            T result = default;
            Exception reason = null;
            PdnSynchronizationContext.Instance.Send(_ => result = SafeAction(action, out reason), null);
            ThrowIfNotNull(reason);
            return result;
        }

        public static void InvokeNoReturn(Action action)
        {
            Exception reason = null;
            PdnSynchronizationContext.Instance.Send(_ => SafeAction(action, out reason), null);
            ThrowIfNotNull(reason);
        }

        private static void SafeAction(Action action, out Exception exception)
        {
            exception = null;
            try
            {
                action();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
        }

        private static T SafeAction<T>(Func<T> action, out Exception exception)
        {
            exception = null;
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                exception = ex;
                return default;
            }
        }

        #endregion

        #region Root Invoke Method 2

        //public static T Invoke<T>(Func<T> action)
        //{
        //    return (T)MainForm.Invoke(action);
        //}

        //public static void InvokeNoReturn(Action action)
        //{
        //    MainForm.Invoke(action);
        //}

        #endregion

        #region Get Theme Colors Method 1

        private sealed class DummyForm : PdnBaseForm
        {
            public static Lazy<(Color, Color)> GetThemeColors = new Lazy<(Color, Color)>(() =>
            {
                using (var form = new DummyForm())
                {
                    return (form.ForeColor, form.BackColor);
                }
            });

            private DummyForm()
            {
                UseAppThemeColors = true;
            }
        }

        #endregion
    }
}
