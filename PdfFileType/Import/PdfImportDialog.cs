// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaintDotNet;
using PdfFileTypePlugin.Localization;
using PDFiumSharp;
using PDFiumSharp.Enums;

namespace PdfFileTypePlugin.Import
{
    internal partial class PdfImportDialog : Form
    {
        #region Fields

        private const RenderingFlags SupportedRenderingFlags = RenderingFlags.Grayscale | RenderingFlags.LcdText | RenderingFlags.NoSmoothImage |
            RenderingFlags.NoSmoothPath | RenderingFlags.NoSmoothText;
        private object lastModifiedNud;
        private bool dontUpdate;
        private readonly PdfDocument pdf;
        private CancellationTokenSource cts;
        private readonly int maxPageWidth;
        private readonly int maxPageHeight;
        private readonly bool pageSizeVaries;
        private readonly bool hasPassword;
        private Color? pickedColor;
        private RenderingFlags activeFlags;
        private Document result;
        private Exception reason;

        #endregion

        #region Properties

        public Document Result
        {
            get
            {
                if (result != null) { return result; }
                if (reason != null) { throw reason; }
                throw new WarningException(StringResources.CanceledUponYourRequest, new OperationCanceledException());
            }
            private set => result = value;
        }

        #endregion

        #region Constructor

        public PdfImportDialog(PdfDocument pdf, bool hasPassword)
        {
            Owner = UI.MainForm;
            InitializeComponent();
            UI.ApplyTheme(this, immersive: false);
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = true;
            ShowIcon = false;
            Text = String.Join(" v", MyPluginSupportInfo.Instance.DisplayName, MyPluginSupportInfo.Instance.Version);

            this.pdf = pdf;
            this.hasPassword = hasPassword;
            pageSizeVaries = pdf.GetMaxPageSize(out maxPageWidth, out maxPageHeight);

            PopulateLabels();
            PopulateControls();
            HookEvents();
        }

        #endregion

        #region Private Methods

        private void PopulateLabels()
        {
            // Information
            GbInformation.Text = StringResources.Information;
            LblLblPageCount.Text = StringResources.PageCount;
            LblLblPassword.Text = StringResources.Password;
            LblLblPageSize.Text = StringResources.PageSize;

            // Render options
            GbRenderOptions.Text = StringResources.RenderOptions;

            // Page(s)
            GbPages.Text = StringResources.Pages;
            RbAllPages.Text = StringResources.AllPages;
            RbCustomPages.Text = StringResources.Custom;
            TxtCustomPages.Text = StringResources.CustomPagesPlaceHolderText;
            TxtCustomPages.Font = new Font(TxtCustomPages.Font, FontStyle.Italic);
            TxtCustomPages.ForeColor = Color.DimGray;
            LblBlendOnto.Text = StringResources.BlendOnto;
            LblAlignment.Text = StringResources.Alignment;


            // Layer settings
            GbLayers.Text = StringResources.Layers;
            CbRestoreLayerProperties.Text = StringResources.RestoreLayerProperties;
            CbDescending.Text = StringResources.Descending;
            CbInvisibleLayers.Text = StringResources.Invisible;
            LblLayerOpacity.Text = StringResources.Opacity;
            LblBlendMode.Text = StringResources.BlendMode;

            // Dimensions
            GbDimensions.Text = StringResources.Dimensions;
            LblWidth.Text = StringResources.Width;
            LblHeight.Text = StringResources.Height;
            LblPx1.Text = StringResources.Px;
            LblPx2.Text = StringResources.Px;
            CbKeepAspectRatio.Text = StringResources.MaintainAspectRatio;

            // Other
            UpdateAvailLabel.Text = String.Empty;
            LnkGitHub.Text = StringResources.GitHubLink;
            LnkForum.Text = StringResources.ForumLink;
            LblProgress.Text = StringResources.Ready;
            BtnReset.Text = StringResources.Reset;
            BtnOK.Text = StringResources.OK;
            BtnCancel.Text = StringResources.Cancel;
        }

        private void PopulateControls()
        {
            LblPageCount.Text = pdf.Pages.Count.ToString("N0", CultureInfo.CurrentCulture.NumberFormat);
            LblPassword.Text = hasPassword ? StringResources.Yes : StringResources.No;
            NudWidth.Value = maxPageWidth;
            NudHeight.Value = maxPageHeight;
            if (pageSizeVaries)
            {
                LblPageSize.Text = StringResources.Varies;
                CbAlignment.Enabled = true;
                LblAlignment.Enabled = true;
            }
            else
            {
                LblPageSize.Text = String.Join("×", maxPageWidth, maxPageHeight);
                CbAlignment.Enabled = false;
                LblAlignment.Enabled = false;
            }
            NudLyrOpacity.Value = LastUsedConfig.Opacity;
            CbRestoreLayerProperties.Checked = LastUsedConfig.RestoreProperties;
            CbDescending.Checked = LastUsedConfig.Descending;
            CbInvisibleLayers.Checked = LastUsedConfig.Invisible;
            PopulateColors();
            PopulateBlendModes();
            PopulateRenderingOptions();
            PopulateAlignment();
        }

        private void PopulateColors()
        {
            List<object> ds = typeof(Color).GetProperties()
                .Where(x => x.PropertyType == typeof(Color))
                .Select(x => x.GetValue(null))
                .ToList();
            ds.Insert(1, StringResources.PickAColor);
            CbBlendOnto.Items.Clear();
            CbBlendOnto.DataSource = ds;
            Color selected = LastUsedConfig.BlendColor;
            if (selected.A < 255)
            {
                CbBlendOnto.SelectedItem = Color.Transparent;
            }
            else if (selected.IsKnownColor)
            {
                CbBlendOnto.SelectedItem = selected;
            }
            else
            {
                CbBlendOnto.SelectedIndex = 1;
                pickedColor = selected;
            }
        }

        private void PopulateBlendModes()
        {
            CbLyrBlendMode.Items.Clear();
            CbLyrBlendMode.DataSource = Enum.GetValues(typeof(LayerBlendMode));
            CbLyrBlendMode.SelectedItem = LastUsedConfig.BlendMode;
        }

        private void PopulateRenderingOptions()
        {
            ClbRenderOptions.BeginUpdate();
            ClbRenderOptions.DisplayMember = nameof(CheckedListBoxItem.Text);
            ClbRenderOptions.ValueMember = nameof(CheckedListBoxItem.Value);
            CheckedListBox.ObjectCollection items = ClbRenderOptions.Items;
            items.Clear();
            foreach (RenderingFlags flag in Enum.GetValues(typeof(RenderingFlags)))
            {
                if ((flag & SupportedRenderingFlags) == 0) { continue; }
                bool isChecked = (flag & LastUsedConfig.RenderingFlags) != 0;
                CheckedListBoxItem item = new CheckedListBoxItem
                {
                    Text = Localize.GetDisplayName(flag),
                    Value = flag,
                    Description = Localize.GetDescription(flag)
                };
                items.Add(item, isChecked);
            }
            ClbRenderOptions.EndUpdate();
        }

        private void PopulateAlignment()
        {
            CbAlignment.Items.Clear();
            CbAlignment.DataSource = Enum.GetValues(typeof(ContentAlignment));
            CbAlignment.SelectedItem = ContentAlignment.MiddleCenter;
        }

        private void HookEvents()
        {
            BtnOK.Click += BtnOK_Click;
            BtnCancel.Click += BtnCancel_Click;
            BtnReset.Click += BtnReset_Click;
            NudWidth.KeyUp += Nud_KeyUp;
            NudHeight.KeyUp += Nud_KeyUp;
            NudHeight.ValueChanged += Nud_ValueChanged;
            NudWidth.ValueChanged += Nud_ValueChanged;
            CbKeepAspectRatio.CheckedChanged += CbKeepAR_CheckedChanged;
            ClbRenderOptions.ItemCheck += ClbRenderOptions_ItemCheck;
            ClbRenderOptions.MouseMove += ClbRenderOptions_MouseMove;
            ClbRenderOptions.MouseLeave += ClbRenderOptions_MouseLeave;
            RbCustomPages.CheckedChanged += RbCustomPages_CheckedChanged;
            PagesTableLayoutPanel.MouseDown += PagesTableLayoutPanel_MouseDown;
            TxtCustomPages.TextChanged += TxtCustomPages_TextChanged;
            TxtCustomPages.GotFocus += TxtCustomPages_GotFocus;
            TxtCustomPages.LostFocus += TxtCustomPages_LostFocus;
            CbBlendOnto.DrawItem += CbBgColor_DrawItem;
            CbBlendOnto.SelectedIndexChanged += CbBgColor_SelectedIndexChanged;
            CbLyrBlendMode.DrawItem += DrawEnumString<LayerBlendMode>;
            CbAlignment.DrawItem += DrawEnumString<ContentAlignment>;
            LnkGitHub.LinkClicked += LnkGitHub_LinkClicked;
            LnkForum.LinkClicked += LnkForum_LinkClicked;
        }

        private async Task CheckUpdatesAsync()
        {
            // Check if a new version is available using GitHub API
            try
            {

                Version latest = await Util.CheckUpdatesAsync.Value;
                Version current = MyPluginSupportInfo.Instance.Version;
                UI.InvokeNoReturn(UpdateAvailLabel, () =>
                {
                    if (current < latest)
                    {
                        UpdateAvailLabel.Visible = true;
                        UpdateAvailLabel.Text = StringResources.AnUpdateIsAvailable + $" ({latest})";
                        UpdateAvailLabel.ForeColor = Color.Red;
                    }
                    else
                    {
                        UpdateAvailLabel.Visible = false;
                    }
                });
            }
            catch (Exception ex)
            {
                // ignore any exception here, it's not important.
                Debug.WriteLine("Error while checking updates: " + ex.Message);
            }
        }

        #region Progress Reporting

        private void SetupProgress(int max)
        {
            if (Disposing || IsDisposed) { return; }
            if (ProgressBar1.InvokeRequired)
            {
                ProgressBar1.Invoke((Action<int>)SetupProgress, max);
                return;
            }

            ProgressBar1.Value = 0;
            ProgressBar1.Maximum = max;
            if (max == 1)
            {
                ProgressBar1.Style = ProgressBarStyle.Marquee;
            }
            UpdateProgressLabel();
        }

        private void IncrementProgress()
        {
            if (Disposing || IsDisposed) { return; }
            if (ProgressBar1.InvokeRequired)
            {
                ProgressBar1.Invoke((MethodInvoker)IncrementProgress);
                return;
            }

            // ProgressBar animation is slow to catch up
            // This workaround prevents the animation
            int value = ProgressBar1.Value + 1;
            if (value >= ProgressBar1.Maximum)
            {
                value = Util.Clamp(value, ProgressBar1.Minimum, ProgressBar1.Maximum);
                ProgressBar1.Value = value;
                ProgressBar1.Value = value - 1;
            }
            else
            {
                ProgressBar1.Value = value + 1;
            }
            ProgressBar1.Value = value;
            UpdateProgressLabel();
        }

        private void UpdateProgressLabel()
        {
            if (Disposing || IsDisposed) { return; }
            if (LblProgress.InvokeRequired)
            {
                LblProgress.Invoke((MethodInvoker)UpdateProgressLabel);
                return;
            }

            LblProgress.Text = ProgressBar1.Maximum == 1
                ? StringResources.Running
                : String.Join(" / ", ProgressBar1.Value, ProgressBar1.Maximum);
        }

        #endregion

        #endregion

        #region Events

        protected override async void OnLoad(EventArgs e)
        {
            ClientSize = RootPanel1.Size;
            base.OnLoad(e);
            if (DesignMode) { return; }
            await CheckUpdatesAsync();
        }

        private void Nud_ValueChanged(object sender, EventArgs e)
        {
            if (dontUpdate) { return; }
            lastModifiedNud = sender;
            UpdateOtherInput();
        }

        private void Nud_KeyUp(object sender, KeyEventArgs e)
        {
            if (dontUpdate) { return; }
            lastModifiedNud = sender;
            if (e.KeyValue >= '0' || e.KeyValue <= '9' || e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                // handle digit, delete and backspace keys
                UpdateOtherInput();
            }
            else
            {
                // ignore any other key
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void CbKeepAR_CheckedChanged(object sender, EventArgs e)
        {
            if (dontUpdate) { return; }
            UpdateOtherInput();
        }

        private void UpdateOtherInput()
        {
            if (lastModifiedNud == null)
            {
                lastModifiedNud = NudWidth.Value > NudHeight.Value ? NudWidth : NudHeight;
            }

            if (ReferenceEquals(lastModifiedNud, NudWidth))
            {
                decimal newHeight = CbKeepAspectRatio.Checked ? NudWidth.Value * maxPageHeight / maxPageWidth : NudHeight.Value;
                newHeight = Util.Clamp(newHeight, 1, Math.Min(NudHeight.Maximum, int.MaxValue / (NudWidth.Value * 4)));
                NudHeight.Value = newHeight;
            }
            else
            {
                decimal newWidth = CbKeepAspectRatio.Checked ? NudHeight.Value * maxPageWidth / maxPageHeight : NudWidth.Value;
                newWidth = Util.Clamp(newWidth, 1, Math.Min(NudWidth.Maximum, int.MaxValue / (NudHeight.Value * 4)));
                NudWidth.Value = newWidth;
            }
        }

        private void ClbRenderOptions_MouseLeave(object sender, EventArgs e)
        {
            LblRenderingFlagsDesc.Text = String.Empty;
        }

        private void ClbRenderOptions_MouseMove(object sender, MouseEventArgs e)
        {
            int idx = ClbRenderOptions.IndexFromPoint(e.Location);
            string text;
            if (idx > -1)
            {
                CheckedListBoxItem item = (CheckedListBoxItem)ClbRenderOptions.Items[idx];
                text = item.Description;
            }
            else
            {
                text = String.Empty;
            }

            if (!LblRenderingFlagsDesc.Text.Equals(text, StringComparison.Ordinal))
            {
                LblRenderingFlagsDesc.Text = text;
            }
        }

        private void CbBgColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbBlendOnto.SelectedIndex != 1)
            {
                pickedColor = null;
                return;
            }

            DialogResult dr = UI.Invoke(this, form => colorDialog.ShowDialog(form));
            if (dr != DialogResult.OK)
            {
                CbBlendOnto.SelectedIndex = 0;
                return;
            }
            pickedColor = colorDialog.Color;
            CbBlendOnto.Invalidate();
        }

        private void CbBgColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index > -1 && sender is ComboBox control)
            {
                object cur = control.Items[e.Index];

                string text;
                Color color;
                Rectangle bounds = e.Bounds;
                Rectangle? borderBox = null;
                Rectangle? fillBox = null;
                Rectangle textBox;

                if (cur is Color current)
                {
                    color = current;
                    text = color == Color.Transparent
                        ? $"{StringResources.No} ({StringResources.AsIs})"
                        : SplitCamelCase(color.Name);
                }
                else
                {
                    color = pickedColor ?? Color.Transparent;
                    text = cur.ToString();
                }

                if (color != Color.Transparent)
                {
                    borderBox = new Rectangle(bounds.X + 1, bounds.Y + 1, bounds.Height * 2 - 2, bounds.Height - 2);
                    fillBox = new Rectangle(bounds.X + 2, bounds.Y + 2, bounds.Height * 2 - 3, bounds.Height - 3);
                    textBox = new Rectangle(bounds.X + 2 * bounds.Height, bounds.Y + 1, bounds.Width - bounds.Height * 2, bounds.Height);
                }
                else
                {
                    textBox = bounds;
                }

                using (var solidBrush = new SolidBrush(e.ForeColor))
                {
                    Graphics g = e.Graphics;
                    g.DrawString(text, e.Font, solidBrush, textBox);

                    if (borderBox != null && fillBox != null)
                    {
                        solidBrush.Color = color;
                        g.FillRectangle(solidBrush, fillBox.Value);
                        g.DrawRectangle(Pens.Black, borderBox.Value);
                    }
                }
            }
        }

        private void RbCustomPages_CheckedChanged(object sender, EventArgs e)
        {
            TxtCustomPages.Enabled = RbCustomPages.Checked;
            if (RbCustomPages.Checked)
            {
                TxtCustomPages.Focus();
            }
        }

        private void TxtCustomPages_TextChanged(object sender, EventArgs e)
        {
            if (TxtCustomPages.Text.Equals(StringResources.CustomPagesPlaceHolderText))
            {
                TxtCustomPages.Font = new Font(TxtCustomPages.Font, FontStyle.Italic);
                TxtCustomPages.ForeColor = Color.DimGray;
            }
        }

        private void TxtCustomPages_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtCustomPages.Text))
            {
                TxtCustomPages.Text = StringResources.CustomPagesPlaceHolderText;
                TxtCustomPages.Font = new Font(TxtCustomPages.Font, FontStyle.Italic);
                TxtCustomPages.ForeColor = Color.DimGray;
            }
        }

        private void TxtCustomPages_GotFocus(object sender, EventArgs e)
        {
            if (TxtCustomPages.Text.Equals(StringResources.CustomPagesPlaceHolderText, StringComparison.Ordinal))
            {
                TxtCustomPages.Text = string.Empty;
                TxtCustomPages.Font = new Font(TxtCustomPages.Font, FontStyle.Regular);
                TxtCustomPages.ForeColor = TxtCustomPages.Parent.ForeColor;
            }
        }

        private void PagesTableLayoutPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (PagesTableLayoutPanel.GetChildAtPoint(e.Location) == TxtCustomPages)
            {
                RbCustomPages.Checked = true;
            }
        }

        private void ClbRenderOptions_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (dontUpdate) { return; }
            RenderingFlags flag = (RenderingFlags)((CheckedListBoxItem)ClbRenderOptions.Items[e.Index]).Value;
            if (e.NewValue == CheckState.Checked)
            {
                activeFlags |= flag;
            }
            else
            {
                activeFlags &= ~flag;
            }
        }

        private async void BtnOK_Click(object sender, EventArgs e)
        {
            int pageCount = pdf.Pages.Count;
            List<int> pageIndices = new List<int>(pageCount);

            void OnParseFail()
            {
                pageIndices.Clear();
                string text = String.Format(StringResources.CustomFieldError, StringResources.Custom);
                UI.Invoke(this, form => MessageBoxEx.Show(form, text, StringResources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error));
            }

            if (RbCustomPages.Checked)
            {
                string customPages = TxtCustomPages.Text;

                if (customPages.Equals(StringResources.CustomPagesPlaceHolderText, StringComparison.Ordinal) || string.IsNullOrWhiteSpace(customPages))
                {
                    OnParseFail();
                    return;
                }

                string[] commaSep = customPages.Split(',');
                foreach (string token in commaSep)
                {
                    if (string.IsNullOrWhiteSpace(token))
                    {
                        continue;
                    }

                    if (int.TryParse(token, out int p))
                    {
                        if (p > 0 && p <= pageCount)
                        {
                            pageIndices.Add(p - 1);
                            continue;
                        }
                        else
                        {
                            OnParseFail();
                            return;
                        }
                    }

                    string[] dashSep = token.Split('-');
                    if (dashSep.Length == 2 &&
                        int.TryParse(dashSep[0], out int from) &&
                        int.TryParse(dashSep[1], out int to) &&
                        from <= to &&
                        from <= pageCount && to <= pageCount &&
                        from > 0 && to > 0)
                    {
                        pageIndices.AddRange(Enumerable.Range(from - 1, to - from + 1));
                    }
                    else
                    {
                        OnParseFail();
                        return;
                    }
                }
            }
            else
            {
                pageIndices.AddRange(Enumerable.Range(0, pageCount));
            }

            SetupProgress(pageIndices.Count);
            MainTableLayoutPanel.Enabled = false;
            BtnReset.Enabled = false;
            BtnOK.Enabled = false;

            try
            {
                using (cts = new CancellationTokenSource())
                {
                    CancellationToken ctoken = cts.Token;
                    PdfImport context = new PdfImport((int)NudWidth.Value, (int)NudHeight.Value, maxPageWidth, maxPageHeight)
                    {
                        RestoreProperties = CbRestoreLayerProperties.Checked,
                        Alignment = (ContentAlignment)CbAlignment.SelectedItem,
                        Invisible = CbInvisibleLayers.Checked,
                        Descending = CbDescending.Checked,
                        BlendColor = CbBlendOnto.SelectedIndex == 1 ? pickedColor.Value : (Color)CbBlendOnto.Items[CbBlendOnto.SelectedIndex],
                        BlendMode = (LayerBlendMode)CbLyrBlendMode.SelectedItem,
                        Opacity = (byte)NudLyrOpacity.Value,
                        RenderingFlags = activeFlags,
                        PageIndices = pageIndices
                    };

                    Result = await Task.Run(() => context.Convert(pdf, IncrementProgress, ctoken), ctoken);
                    LastUsedConfig.CopyFromContext(context);
                }
            }
            catch (Exception ex) when (ex is OperationCanceledException)
            {
                reason = new WarningException(StringResources.CanceledUponYourRequest, ex);
            }
            catch (Exception ex)
            {
                reason = ex;
            }
            finally
            {
                if (reason != null || result != null)
                {
                    Close();
                }
                else
                {
                    MainTableLayoutPanel.Enabled = true;
                    BtnReset.Enabled = true;
                    BtnOK.Enabled = true;
                    ProgressBar1.Style = ProgressBarStyle.Blocks;
                    cts = null;
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            BtnCancel.Enabled = false;
            if (cts != null)
            {
                try
                {
                    cts.Cancel();
                }
                catch (ObjectDisposedException)
                {
                    // this should never happen
                    Close();
                }
            }
            else
            {
                reason = new WarningException(StringResources.CanceledUponYourRequest, new OperationCanceledException());
                Close();
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            dontUpdate = true;
            for (int i = 0; i < ClbRenderOptions.Items.Count; i++)
            {
                ClbRenderOptions.SetItemChecked(i, false);
            }
            TxtCustomPages.Text = StringResources.CustomPagesPlaceHolderText;
            RbAllPages.Checked = true;
            CbDescending.Checked = false;
            CbInvisibleLayers.Checked = true;
            CbBlendOnto.SelectedIndex = 0;
            CbLyrBlendMode.SelectedItem = LayerBlendMode.Normal;
            NudLyrOpacity.Value = byte.MaxValue;
            CbKeepAspectRatio.Checked = true;
            NudWidth.Value = maxPageWidth;
            NudHeight.Value = maxPageHeight;
            CbRestoreLayerProperties.Checked = true;
            dontUpdate = false;
        }

        private void LnkGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { return; }
            LaunchUrl(MyPluginSupportInfo.Instance.WebsiteUri);
        }

        private void LnkForum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { return; }
            LaunchUrl(MyPluginSupportInfo.Instance.ForumUri);
        }

        #endregion

        #region Static

        private static void LaunchUrl(Uri uri) => Process.Start("explorer", $@"""{uri}""");

        private static string SplitCamelCase(string s) => Regex.Replace(s, "(\\B[A-Z])", " $1");

        private static string SplitCamelCase<T>(T flag) where T : Enum => SplitCamelCase(Enum.GetName(typeof(T), flag));

        private static void DrawEnumString<T>(object sender, DrawItemEventArgs e) where T : Enum
        {
            if (!(sender is ComboBox control)) { return; }
            e.DrawBackground();
            if (e.Index > -1)
            {
                T value = (T)control.Items[e.Index];
                using (var solidBrush = new SolidBrush(e.ForeColor))
                {
                    string resourceKey = value.GetType().Name + Enum.GetName(typeof(T), value);
                    string text = StringResources.ResourceManager.GetString(resourceKey) ?? SplitCamelCase(value);
                    e.Graphics.DrawString(text, e.Font, solidBrush, e.Bounds);
                }
            }
        }

        #endregion

        #region CheckedListBoxItem

        private sealed class CheckedListBoxItem
        {
            public string Text { get; set; }

            public object Value { get; set; }

            public string Description { get; set; }
        }

        #endregion

        #region LastUsedConfig

        private static class LastUsedConfig
        {
            public static Color BlendColor { get; set; } = Color.Empty;

            public static RenderingFlags RenderingFlags { get; set; } = RenderingFlags.None;

            public static bool RestoreProperties { get; set; } = true;

            public static bool Descending { get; set; } = false;

            public static bool Invisible { get; set; } = true;

            public static byte Opacity { get; set; } = 255;

            public static LayerBlendMode BlendMode { get; set; } = LayerBlendMode.Normal;

            public static void CopyFromContext(PdfImport context)
            {
                Descending = context.Descending;
                Opacity = context.Opacity;
                Invisible = context.Invisible;
                BlendMode = context.BlendMode;
                BlendColor = context.BlendColor;
                RenderingFlags = context.RenderingFlags;
                RestoreProperties = context.RestoreProperties;
            }
        }

        #endregion
    }
}
