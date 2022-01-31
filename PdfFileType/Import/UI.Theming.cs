// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using ContentAlignment = System.Drawing.ContentAlignment;

namespace PdfFileTypePlugin.Import
{
    internal static partial class UI
    {
        public static void ApplyTheme(Form form, bool immersive = false)
        {
            if (form.Owner == null) { return; }
            Form owner = form.Owner;
            form.ForeColor = owner.ForeColor;
            form.BackColor = owner.BackColor;
            ApplyTheme((Control)form, immersive);
        }

        private static void ApplyTheme(Control root, bool immersive = false)
        {
            Ensure.IsNotNull(root, nameof(root));
            Color foreColor = root.ForeColor;
            Color backColor = root.BackColor;
            bool darkMode = foreColor.GetBrightness() > backColor.GetBrightness();

            if (immersive && root is Form)
            {
                Native.SetPreferredAppMode(true);
                Native.UseImmersiveDarkModeColors(root, darkMode);
                Native.UseImmersiveDarkMode(root, darkMode);
            }

            foreach (Control control in root.Descendants())
            {
                Type ctype = control.GetType();
                if (ctype == typeof(LinkLabel))
                {
                    LinkLabel link = (LinkLabel)control;
                    if (darkMode)
                    {
                        link.LinkColor = Color.Orange;
                        link.VisitedLinkColor = Color.Orange;
                    }
                    else
                    {
                        link.LinkColor = Color.Empty;
                        link.VisitedLinkColor = Color.Empty;
                    }
                }
                else if (ctype == typeof(Label))
                {
                    ((Label)control).FlatStyle = FlatStyle.Standard;
                    Subscribe(control, Label_Paint);
                }
                else if (ctype == typeof(CheckBox) || ctype == typeof(RadioButton))
                {
                    ((ButtonBase)control).FlatStyle = FlatStyle.Standard;
                    Subscribe(control, CheckRadio_Paint);
                }
                else if (ctype == typeof(Button))
                {
                    ((ButtonBase)control).FlatStyle = FlatStyle.System;
                }
                else if (ctype == typeof(GroupBox))
                {
                    ((GroupBox)control).FlatStyle = FlatStyle.Standard;
                    Subscribe(control, GroupBox_Paint);
                }

                control.ForeColor = foreColor;

                if (!darkMode)
                {
                    if (control.GetType() == typeof(ComboBox) ||
                        control.GetType() == typeof(TextBox))
                    {
                        control.BackColor = SystemColors.Window;
                    }
                    else
                    {
                        control.BackColor = backColor;
                    }
                }
                else
                {
                    control.BackColor = backColor;
                }


                if (immersive)
                {
                    if (control.IsHandleCreated)
                    {
                        Native.ControlSetDarkMode(control, darkMode);
                    }
                    control.HandleCreated -= Control_HandleCreated;
                    control.HandleCreated -= Control_HandleCreated;
                    control.HandleCreated += Control_HandleCreated;
                }
            }
        }

        private static void Control_HandleCreated(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            bool isDark = control.ForeColor.GetBrightness() > control.BackColor.GetBrightness();
            Native.ControlSetDarkMode(control, isDark);
        }

        private static void Subscribe(Control control, PaintEventHandler handler)
        {
            control.Paint -= handler;
            control.Paint -= handler;
            control.Paint += handler;
        }

        private static void DrawBackground(Graphics g, Color backColor, Rectangle rect)
        {
            using (var solidBrush = new SolidBrush(backColor))
            {
                g.FillRectangle(solidBrush, rect);
            }
        }

        private static void GroupBox_Paint(object sender, PaintEventArgs e)
        {
            GroupBox groupBox = (GroupBox)sender;
            if (groupBox.Enabled) { return; }
            Color foreColor = SystemColors.GrayText;
            bool rtl = groupBox.RightToLeft == RightToLeft.Yes;
            TextFormatFlags tff = ConvertAlignment(ContentAlignment.MiddleLeft, rtl) | TextFormatFlags.WordBreak;
            DrawBackground(e.Graphics, groupBox.BackColor, e.ClipRectangle);
            GroupBoxRenderer.DrawGroupBox(e.Graphics, e.ClipRectangle, groupBox.Text, groupBox.Font, foreColor, tff, GroupBoxState.Disabled);
        }

        private static void Label_Paint(object sender, PaintEventArgs e)
        {
            Label label = (Label)sender;
            if (label.Enabled) { return; }
            Color foreColor = SystemColors.GrayText;
            bool rtl = label.RightToLeft == RightToLeft.Yes;
            TextFormatFlags tff = ConvertAlignment(label.TextAlign, rtl) | TextFormatFlags.WordBreak;
            DrawBackground(e.Graphics, label.BackColor, e.ClipRectangle);
            TextRenderer.DrawText(e.Graphics, label.Text, label.Font, e.ClipRectangle, foreColor, tff);
        }

        private static void CheckRadio_Paint(object sender, PaintEventArgs e)
        {
            ButtonBase button = (ButtonBase)sender;
            if (button.Enabled) { return; }
            Color foreColor = SystemColors.GrayText;
            Size glyphSize = CheckBoxRenderer.GetGlyphSize(e.Graphics, CheckBoxState.UncheckedNormal);
            bool rtl = button.RightToLeft == RightToLeft.Yes;
            TextFormatFlags tff = ConvertAlignment(button.TextAlign, rtl) |
                TextFormatFlags.LeftAndRightPadding |
                TextFormatFlags.Internal;
            Rectangle rect = e.ClipRectangle;
            Size size = new Size(rect.Width - 1 - glyphSize.Width, rect.Height);
            Point point = rtl ? new Point(rect.X - 1, rect.Y) : new Point(rect.X + glyphSize.Width + 1, rect.Y + 1);
            rect = new Rectangle(point, size);
            DrawBackground(e.Graphics, button.BackColor, rect);
            TextRenderer.DrawText(e.Graphics, button.Text, button.Font, rect, foreColor, tff);
        }

        private static IEnumerable<Control> Descendants(this Control root)
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

        #region ContentAlignment<->TextFormatting Conversions

        private static bool IsRight(ContentAlignment alignment)
        {
            switch (alignment)
            {
                case ContentAlignment.TopRight:
                case ContentAlignment.BottomRight:
                case ContentAlignment.MiddleRight:
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsLeft(ContentAlignment alignment)
        {
            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.BottomLeft:
                case ContentAlignment.MiddleLeft:
                    return true;
                default:
                    return false;
            }
        }

        private static TextFormatFlags ConvertAlignment(ContentAlignment alignment, bool rtl)
        {
            TextFormatFlags tff = TextFormatFlags.Default;
            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopRight:
                case ContentAlignment.TopCenter:
                    tff |= TextFormatFlags.Top;
                    break;
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomRight:
                case ContentAlignment.BottomCenter:
                    tff |= TextFormatFlags.Bottom;
                    break;
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.MiddleCenter:
                    tff |= TextFormatFlags.VerticalCenter;
                    break;
            }
            if ((IsLeft(alignment) && !rtl) || (IsRight(alignment) && rtl))
            {
                tff |= TextFormatFlags.Left;
            }
            else if ((IsRight(alignment) && !rtl) || (IsLeft(alignment) && rtl))
            {
                tff |= TextFormatFlags.Right;
            }
            else
            {
                tff |= TextFormatFlags.HorizontalCenter;
            }
            return tff;
        }

        #endregion

        #region Native

        [SuppressUnmanagedCodeSecurity]
        private static class Native
        {
            #region Scaling Factor

            [DllImport("gdi32", SetLastError = true)]
            private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

            public static float GetScalingFactor()
            {
                const int VERTRES = 10;
                const int DESKTOPVERTRES = 117;
                using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr desktop = g.GetHdc();
                    int logicalScreenHeight = GetDeviceCaps(desktop, VERTRES);
                    int physicalScreenHeight = GetDeviceCaps(desktop, DESKTOPVERTRES);
                    return physicalScreenHeight / (float)logicalScreenHeight; // 1.25 = 125%
                }
            }

            #endregion

            #region Experimental Dark Mode

            private const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
            private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
            private const int PREFFERED_APP_MODE__ALLOW_DARK = 1;
            private const int PREFFERED_APP_MODE__DEFAULT = 0;
            private const int S_OK = 0;

            public static bool IsDarkModeSupported { get; } = IsWindows10OrGreater(17763);

            public static bool ControlSetDarkMode(IWin32Window window, bool enable)
            {
                if (!IsDarkModeSupported) { return false; }
                int hres = SetWindowTheme(window.Handle, enable ? "darkmode_explorer" : "explorer", null);
                Debug.WriteLineIf(hres != S_OK, $"Error SetWindowTheme: {Marshal.GetLastWin32Error()}");
                return Check(hres);
            }

            public static bool UseImmersiveDarkModeColors(IWin32Window window, bool enable)
            {
                if (!IsDarkModeSupported) { return false; }
                var data = new WindowCompositionAttributeData()
                {
                    Attribute = WindowCompositionAttribute.WCA_USEDARKMODECOLORS,
                    Data = enable ? 1 : 0,
                    SizeOfData = Marshal.SizeOf(typeof(int))
                };
                return SetWindowCompositionAttribute(window.Handle, data);
            }

            public static bool SetPreferredAppMode(bool dark)
            {
                if (!IsDarkModeSupported) { return false; }
                int hres = SetPreferredAppMode(dark ? 1 : 0);
                Debug.WriteLineIf(hres != S_OK, $"Error SetPreferredAppMode: {Marshal.GetLastWin32Error()}");
                return Check(hres);
            }

            public static bool UseImmersiveDarkMode(IWin32Window window, bool enabled)
            {
                if (!IsDarkModeSupported) { return false; }
                int attr = IsWindows10OrGreater(18985)
                    ? DWMWA_USE_IMMERSIVE_DARK_MODE
                    : DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
                int attrValue = enabled ? 1 : 0;
                int hres = DwmSetWindowAttribute(window.Handle, attr, ref attrValue, sizeof(int));
                Debug.WriteLineIf(hres != S_OK, $"Error DwmSetWindowAttribute: {Marshal.GetLastWin32Error()}");
                return Check(hres);
            }

            private static bool Check(int hResult) => hResult == S_OK;

            private static bool IsWindows10OrGreater(int build = -1)
                => Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= build;

            [DllImport("uxtheme", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
            private static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

            [DllImport("uxtheme", SetLastError = true, EntryPoint = "#133")]
            private static extern bool AllowDarkModeForWindow(IntPtr hWnd, bool allow);

            [DllImport("uxtheme", SetLastError = true, EntryPoint = "#135")]
            private static extern int SetPreferredAppMode(int i);

            [DllImport("user32", SetLastError = true)]
            private static extern bool SetWindowCompositionAttribute(IntPtr hwnd, WindowCompositionAttributeData data);

            [DllImport("dwmapi", SetLastError = true)]
            private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

            private enum WindowCompositionAttribute
            {
                WCA_ACCENT_POLICY = 19,
                WCA_USEDARKMODECOLORS = 26
            }

            [StructLayout(LayoutKind.Sequential)]
            private ref struct WindowCompositionAttributeData
            {
                public WindowCompositionAttribute Attribute;
                public int Data;
                public int SizeOfData;
            }

            #endregion
        }

        #endregion
    }
}
