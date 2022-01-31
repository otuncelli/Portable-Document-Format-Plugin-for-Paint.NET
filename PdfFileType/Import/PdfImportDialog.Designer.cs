namespace PdfFileTypePlugin.Import
{
    internal partial class PdfImportDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.RootPanel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnReset = new System.Windows.Forms.Button();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.LblProgress = new System.Windows.Forms.Label();
            this.MainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.GbDimensions = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.CbKeepAspectRatio = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.LblWidth = new System.Windows.Forms.Label();
            this.LblPx2 = new System.Windows.Forms.Label();
            this.NudWidth = new System.Windows.Forms.NumericUpDown();
            this.LblPx1 = new System.Windows.Forms.Label();
            this.NudHeight = new System.Windows.Forms.NumericUpDown();
            this.LblHeight = new System.Windows.Forms.Label();
            this.GbLayers = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.LblLayerOpacity = new System.Windows.Forms.Label();
            this.NudLyrOpacity = new System.Windows.Forms.NumericUpDown();
            this.LblBlendMode = new System.Windows.Forms.Label();
            this.CbLyrBlendMode = new System.Windows.Forms.ComboBox();
            this.CbInvisibleLayers = new System.Windows.Forms.CheckBox();
            this.CbDescending = new System.Windows.Forms.CheckBox();
            this.CbRestoreLayerProperties = new System.Windows.Forms.CheckBox();
            this.GbPages = new System.Windows.Forms.GroupBox();
            this.PagesTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.CbAlignment = new System.Windows.Forms.ComboBox();
            this.LblAlignment = new System.Windows.Forms.Label();
            this.CbBlendOnto = new System.Windows.Forms.ComboBox();
            this.LblBlendOnto = new System.Windows.Forms.Label();
            this.RbCustomPages = new System.Windows.Forms.RadioButton();
            this.TxtCustomPages = new System.Windows.Forms.TextBox();
            this.RbAllPages = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.GbRenderOptions = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ClbRenderOptions = new System.Windows.Forms.CheckedListBox();
            this.LblRenderingFlagsDesc = new System.Windows.Forms.Label();
            this.GbInformation = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.LblLblPageSize = new System.Windows.Forms.Label();
            this.LblLblPageCount = new System.Windows.Forms.Label();
            this.LblLblPassword = new System.Windows.Forms.Label();
            this.LblPageCount = new System.Windows.Forms.Label();
            this.LblPassword = new System.Windows.Forms.Label();
            this.LblPageSize = new System.Windows.Forms.Label();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.UpdateAvailLabel = new System.Windows.Forms.Label();
            this.LnkForum = new System.Windows.Forms.LinkLabel();
            this.LnkGitHub = new System.Windows.Forms.LinkLabel();
            this.RootPanel1.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.MainTableLayoutPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.GbDimensions.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudHeight)).BeginInit();
            this.GbLayers.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudLyrOpacity)).BeginInit();
            this.GbPages.SuspendLayout();
            this.PagesTableLayoutPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.GbRenderOptions.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.GbInformation.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // colorDialog
            // 
            this.colorDialog.FullOpen = true;
            // 
            // RootPanel1
            // 
            this.RootPanel1.AutoSize = true;
            this.RootPanel1.Controls.Add(this.tableLayoutPanel9);
            this.RootPanel1.Controls.Add(this.tableLayoutPanel7);
            this.RootPanel1.Controls.Add(this.MainTableLayoutPanel);
            this.RootPanel1.Controls.Add(this.tableLayoutPanel8);
            this.RootPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.RootPanel1.Location = new System.Drawing.Point(0, 0);
            this.RootPanel1.Name = "RootPanel1";
            this.RootPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.RootPanel1.Size = new System.Drawing.Size(464, 532);
            this.RootPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.AutoSize = true;
            this.tableLayoutPanel9.ColumnCount = 5;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Controls.Add(this.BtnCancel, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.BtnOK, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.BtnReset, 1, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(10, 485);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(444, 37);
            this.tableLayoutPanel9.TabIndex = 0;
            // 
            // BtnCancel
            // 
            this.BtnCancel.AutoSize = true;
            this.BtnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BtnCancel.Location = new System.Drawing.Point(267, 4);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCancel.MinimumSize = new System.Drawing.Size(75, 0);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 29);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "&Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // BtnOK
            // 
            this.BtnOK.AutoSize = true;
            this.BtnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BtnOK.Location = new System.Drawing.Point(184, 4);
            this.BtnOK.Margin = new System.Windows.Forms.Padding(4);
            this.BtnOK.MinimumSize = new System.Drawing.Size(75, 0);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(75, 29);
            this.BtnOK.TabIndex = 0;
            this.BtnOK.Text = "&OK";
            this.BtnOK.UseVisualStyleBackColor = true;
            // 
            // BtnReset
            // 
            this.BtnReset.AutoSize = true;
            this.BtnReset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnReset.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BtnReset.Location = new System.Drawing.Point(101, 4);
            this.BtnReset.Margin = new System.Windows.Forms.Padding(4);
            this.BtnReset.MinimumSize = new System.Drawing.Size(75, 0);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(75, 29);
            this.BtnReset.TabIndex = 2;
            this.BtnReset.Text = "&Reset";
            this.BtnReset.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.AutoSize = true;
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.ProgressBar1, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.LblProgress, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(10, 454);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.Size = new System.Drawing.Size(444, 31);
            this.tableLayoutPanel7.TabIndex = 1;
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProgressBar1.Location = new System.Drawing.Point(4, 19);
            this.ProgressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.ProgressBar1.MinimumSize = new System.Drawing.Size(0, 5);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(436, 8);
            this.ProgressBar1.TabIndex = 0;
            // 
            // LblProgress
            // 
            this.LblProgress.AutoSize = true;
            this.LblProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblProgress.Location = new System.Drawing.Point(4, 0);
            this.LblProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblProgress.Name = "LblProgress";
            this.LblProgress.Size = new System.Drawing.Size(436, 15);
            this.LblProgress.TabIndex = 1;
            this.LblProgress.Text = "Ready!";
            this.LblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.AutoSize = true;
            this.MainTableLayoutPanel.ColumnCount = 2;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.MainTableLayoutPanel.Controls.Add(this.panel2, 1, 0);
            this.MainTableLayoutPanel.Controls.Add(this.panel1, 0, 0);
            this.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MainTableLayoutPanel.Location = new System.Drawing.Point(10, 25);
            this.MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            this.MainTableLayoutPanel.RowCount = 1;
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.Size = new System.Drawing.Size(444, 429);
            this.MainTableLayoutPanel.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.GbDimensions);
            this.panel2.Controls.Add(this.GbLayers);
            this.panel2.Controls.Add(this.GbPages);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(180, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(261, 423);
            this.panel2.TabIndex = 0;
            // 
            // GbDimensions
            // 
            this.GbDimensions.AutoSize = true;
            this.GbDimensions.Controls.Add(this.tableLayoutPanel11);
            this.GbDimensions.Controls.Add(this.tableLayoutPanel4);
            this.GbDimensions.Dock = System.Windows.Forms.DockStyle.Top;
            this.GbDimensions.Location = new System.Drawing.Point(0, 305);
            this.GbDimensions.Margin = new System.Windows.Forms.Padding(4);
            this.GbDimensions.Name = "GbDimensions";
            this.GbDimensions.Padding = new System.Windows.Forms.Padding(4);
            this.GbDimensions.Size = new System.Drawing.Size(261, 118);
            this.GbDimensions.TabIndex = 0;
            this.GbDimensions.TabStop = false;
            this.GbDimensions.Text = "Dimensions";
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.AutoSize = true;
            this.tableLayoutPanel11.ColumnCount = 2;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel11.Controls.Add(this.CbKeepAspectRatio, 1, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(4, 82);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel11.Size = new System.Drawing.Size(253, 32);
            this.tableLayoutPanel11.TabIndex = 0;
            // 
            // CbKeepAspectRatio
            // 
            this.CbKeepAspectRatio.AutoSize = true;
            this.CbKeepAspectRatio.Checked = true;
            this.CbKeepAspectRatio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbKeepAspectRatio.Dock = System.Windows.Forms.DockStyle.Top;
            this.CbKeepAspectRatio.Location = new System.Drawing.Point(128, 4);
            this.CbKeepAspectRatio.Margin = new System.Windows.Forms.Padding(4);
            this.CbKeepAspectRatio.Name = "CbKeepAspectRatio";
            this.CbKeepAspectRatio.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.CbKeepAspectRatio.Size = new System.Drawing.Size(121, 24);
            this.CbKeepAspectRatio.TabIndex = 0;
            this.CbKeepAspectRatio.Text = "Keep Aspect Ratio";
            this.CbKeepAspectRatio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CbKeepAspectRatio.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.ColumnCount = 5;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.LblWidth, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.LblPx2, 3, 1);
            this.tableLayoutPanel4.Controls.Add(this.NudWidth, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.LblPx1, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.NudHeight, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.LblHeight, 1, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(4, 20);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(253, 62);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // LblWidth
            // 
            this.LblWidth.AutoSize = true;
            this.LblWidth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblWidth.Location = new System.Drawing.Point(57, 0);
            this.LblWidth.Name = "LblWidth";
            this.LblWidth.Size = new System.Drawing.Size(45, 31);
            this.LblWidth.TabIndex = 0;
            this.LblWidth.Text = "Width";
            this.LblWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblPx2
            // 
            this.LblPx2.AutoSize = true;
            this.LblPx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblPx2.Location = new System.Drawing.Point(176, 31);
            this.LblPx2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblPx2.Name = "LblPx2";
            this.LblPx2.Size = new System.Drawing.Size(19, 31);
            this.LblPx2.TabIndex = 1;
            this.LblPx2.Text = "px";
            this.LblPx2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NudWidth
            // 
            this.NudWidth.AutoSize = true;
            this.NudWidth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NudWidth.Location = new System.Drawing.Point(109, 4);
            this.NudWidth.Margin = new System.Windows.Forms.Padding(4);
            this.NudWidth.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.NudWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NudWidth.Name = "NudWidth";
            this.NudWidth.Size = new System.Drawing.Size(59, 23);
            this.NudWidth.TabIndex = 2;
            this.NudWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NudWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // LblPx1
            // 
            this.LblPx1.AutoSize = true;
            this.LblPx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblPx1.Location = new System.Drawing.Point(175, 0);
            this.LblPx1.Name = "LblPx1";
            this.LblPx1.Size = new System.Drawing.Size(21, 31);
            this.LblPx1.TabIndex = 3;
            this.LblPx1.Text = "px";
            this.LblPx1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NudHeight
            // 
            this.NudHeight.AutoSize = true;
            this.NudHeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NudHeight.Location = new System.Drawing.Point(109, 35);
            this.NudHeight.Margin = new System.Windows.Forms.Padding(4);
            this.NudHeight.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.NudHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NudHeight.Name = "NudHeight";
            this.NudHeight.Size = new System.Drawing.Size(59, 23);
            this.NudHeight.TabIndex = 4;
            this.NudHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NudHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // LblHeight
            // 
            this.LblHeight.AutoSize = true;
            this.LblHeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblHeight.Location = new System.Drawing.Point(58, 31);
            this.LblHeight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblHeight.Name = "LblHeight";
            this.LblHeight.Size = new System.Drawing.Size(43, 31);
            this.LblHeight.TabIndex = 5;
            this.LblHeight.Text = "Height";
            this.LblHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GbLayers
            // 
            this.GbLayers.AutoSize = true;
            this.GbLayers.Controls.Add(this.tableLayoutPanel3);
            this.GbLayers.Controls.Add(this.CbInvisibleLayers);
            this.GbLayers.Controls.Add(this.CbDescending);
            this.GbLayers.Controls.Add(this.CbRestoreLayerProperties);
            this.GbLayers.Dock = System.Windows.Forms.DockStyle.Top;
            this.GbLayers.Location = new System.Drawing.Point(0, 146);
            this.GbLayers.Margin = new System.Windows.Forms.Padding(4);
            this.GbLayers.Name = "GbLayers";
            this.GbLayers.Padding = new System.Windows.Forms.Padding(4);
            this.GbLayers.Size = new System.Drawing.Size(261, 159);
            this.GbLayers.TabIndex = 1;
            this.GbLayers.TabStop = false;
            this.GbLayers.Text = "Layers";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.LblLayerOpacity, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.NudLyrOpacity, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.LblBlendMode, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.CbLyrBlendMode, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 92);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(253, 63);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // LblLayerOpacity
            // 
            this.LblLayerOpacity.AutoSize = true;
            this.LblLayerOpacity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblLayerOpacity.Location = new System.Drawing.Point(4, 0);
            this.LblLayerOpacity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblLayerOpacity.Name = "LblLayerOpacity";
            this.LblLayerOpacity.Size = new System.Drawing.Size(71, 31);
            this.LblLayerOpacity.TabIndex = 0;
            this.LblLayerOpacity.Text = "Opacity";
            this.LblLayerOpacity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NudLyrOpacity
            // 
            this.NudLyrOpacity.AutoSize = true;
            this.NudLyrOpacity.Dock = System.Windows.Forms.DockStyle.Left;
            this.NudLyrOpacity.Location = new System.Drawing.Point(83, 4);
            this.NudLyrOpacity.Margin = new System.Windows.Forms.Padding(4);
            this.NudLyrOpacity.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.NudLyrOpacity.Name = "NudLyrOpacity";
            this.NudLyrOpacity.Size = new System.Drawing.Size(41, 23);
            this.NudLyrOpacity.TabIndex = 1;
            this.NudLyrOpacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NudLyrOpacity.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // LblBlendMode
            // 
            this.LblBlendMode.AutoSize = true;
            this.LblBlendMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblBlendMode.Location = new System.Drawing.Point(4, 31);
            this.LblBlendMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblBlendMode.Name = "LblBlendMode";
            this.LblBlendMode.Size = new System.Drawing.Size(71, 32);
            this.LblBlendMode.TabIndex = 2;
            this.LblBlendMode.Text = "Blend mode";
            this.LblBlendMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CbLyrBlendMode
            // 
            this.CbLyrBlendMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CbLyrBlendMode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CbLyrBlendMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbLyrBlendMode.FormattingEnabled = true;
            this.CbLyrBlendMode.Location = new System.Drawing.Point(83, 35);
            this.CbLyrBlendMode.Margin = new System.Windows.Forms.Padding(4);
            this.CbLyrBlendMode.Name = "CbLyrBlendMode";
            this.CbLyrBlendMode.Size = new System.Drawing.Size(166, 24);
            this.CbLyrBlendMode.TabIndex = 3;
            // 
            // CbInvisibleLayers
            // 
            this.CbInvisibleLayers.AutoSize = true;
            this.CbInvisibleLayers.Checked = true;
            this.CbInvisibleLayers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbInvisibleLayers.Dock = System.Windows.Forms.DockStyle.Top;
            this.CbInvisibleLayers.Location = new System.Drawing.Point(4, 68);
            this.CbInvisibleLayers.Margin = new System.Windows.Forms.Padding(4);
            this.CbInvisibleLayers.Name = "CbInvisibleLayers";
            this.CbInvisibleLayers.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.CbInvisibleLayers.Size = new System.Drawing.Size(253, 24);
            this.CbInvisibleLayers.TabIndex = 1;
            this.CbInvisibleLayers.Text = "Invisible (except first page)";
            this.CbInvisibleLayers.UseVisualStyleBackColor = true;
            // 
            // CbDescending
            // 
            this.CbDescending.AutoSize = true;
            this.CbDescending.Dock = System.Windows.Forms.DockStyle.Top;
            this.CbDescending.Location = new System.Drawing.Point(4, 44);
            this.CbDescending.Margin = new System.Windows.Forms.Padding(4);
            this.CbDescending.Name = "CbDescending";
            this.CbDescending.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.CbDescending.Size = new System.Drawing.Size(253, 24);
            this.CbDescending.TabIndex = 2;
            this.CbDescending.Text = "Descending";
            this.CbDescending.UseVisualStyleBackColor = true;
            // 
            // CbRestoreLayerProperties
            // 
            this.CbRestoreLayerProperties.AutoSize = true;
            this.CbRestoreLayerProperties.Checked = true;
            this.CbRestoreLayerProperties.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbRestoreLayerProperties.Dock = System.Windows.Forms.DockStyle.Top;
            this.CbRestoreLayerProperties.Location = new System.Drawing.Point(4, 20);
            this.CbRestoreLayerProperties.Margin = new System.Windows.Forms.Padding(4);
            this.CbRestoreLayerProperties.Name = "CbRestoreLayerProperties";
            this.CbRestoreLayerProperties.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.CbRestoreLayerProperties.Size = new System.Drawing.Size(253, 24);
            this.CbRestoreLayerProperties.TabIndex = 3;
            this.CbRestoreLayerProperties.Text = "Restore properties (if embedded)";
            this.CbRestoreLayerProperties.UseVisualStyleBackColor = true;
            // 
            // GbPages
            // 
            this.GbPages.AutoSize = true;
            this.GbPages.Controls.Add(this.PagesTableLayoutPanel);
            this.GbPages.Dock = System.Windows.Forms.DockStyle.Top;
            this.GbPages.Location = new System.Drawing.Point(0, 0);
            this.GbPages.Margin = new System.Windows.Forms.Padding(4);
            this.GbPages.Name = "GbPages";
            this.GbPages.Padding = new System.Windows.Forms.Padding(4);
            this.GbPages.Size = new System.Drawing.Size(261, 146);
            this.GbPages.TabIndex = 2;
            this.GbPages.TabStop = false;
            this.GbPages.Text = "Pages";
            // 
            // PagesTableLayoutPanel
            // 
            this.PagesTableLayoutPanel.AutoSize = true;
            this.PagesTableLayoutPanel.ColumnCount = 2;
            this.PagesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PagesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PagesTableLayoutPanel.Controls.Add(this.CbAlignment, 2, 3);
            this.PagesTableLayoutPanel.Controls.Add(this.LblAlignment, 0, 3);
            this.PagesTableLayoutPanel.Controls.Add(this.CbBlendOnto, 1, 2);
            this.PagesTableLayoutPanel.Controls.Add(this.LblBlendOnto, 0, 2);
            this.PagesTableLayoutPanel.Controls.Add(this.RbCustomPages, 0, 1);
            this.PagesTableLayoutPanel.Controls.Add(this.TxtCustomPages, 1, 1);
            this.PagesTableLayoutPanel.Controls.Add(this.RbAllPages, 0, 0);
            this.PagesTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.PagesTableLayoutPanel.Location = new System.Drawing.Point(4, 20);
            this.PagesTableLayoutPanel.Name = "PagesTableLayoutPanel";
            this.PagesTableLayoutPanel.RowCount = 4;
            this.PagesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PagesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PagesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PagesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PagesTableLayoutPanel.Size = new System.Drawing.Size(253, 122);
            this.PagesTableLayoutPanel.TabIndex = 0;
            // 
            // CbAlignment
            // 
            this.CbAlignment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CbAlignment.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CbAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbAlignment.DropDownWidth = 250;
            this.CbAlignment.FormattingEnabled = true;
            this.CbAlignment.IntegralHeight = false;
            this.CbAlignment.Location = new System.Drawing.Point(85, 94);
            this.CbAlignment.Margin = new System.Windows.Forms.Padding(4);
            this.CbAlignment.MaxDropDownItems = 10;
            this.CbAlignment.Name = "CbAlignment";
            this.CbAlignment.Size = new System.Drawing.Size(164, 24);
            this.CbAlignment.TabIndex = 6;
            // 
            // LblAlignment
            // 
            this.LblAlignment.AutoSize = true;
            this.LblAlignment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblAlignment.Location = new System.Drawing.Point(4, 90);
            this.LblAlignment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblAlignment.Name = "LblAlignment";
            this.LblAlignment.Size = new System.Drawing.Size(73, 32);
            this.LblAlignment.TabIndex = 5;
            this.LblAlignment.Text = "Alignment";
            this.LblAlignment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CbBlendOnto
            // 
            this.CbBlendOnto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CbBlendOnto.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CbBlendOnto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbBlendOnto.DropDownWidth = 250;
            this.CbBlendOnto.FormattingEnabled = true;
            this.CbBlendOnto.IntegralHeight = false;
            this.CbBlendOnto.Location = new System.Drawing.Point(85, 62);
            this.CbBlendOnto.Margin = new System.Windows.Forms.Padding(4);
            this.CbBlendOnto.MaxDropDownItems = 10;
            this.CbBlendOnto.Name = "CbBlendOnto";
            this.CbBlendOnto.Size = new System.Drawing.Size(164, 24);
            this.CbBlendOnto.TabIndex = 0;
            // 
            // LblBlendOnto
            // 
            this.LblBlendOnto.AutoSize = true;
            this.LblBlendOnto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblBlendOnto.Location = new System.Drawing.Point(4, 58);
            this.LblBlendOnto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblBlendOnto.Name = "LblBlendOnto";
            this.LblBlendOnto.Size = new System.Drawing.Size(73, 32);
            this.LblBlendOnto.TabIndex = 1;
            this.LblBlendOnto.Text = "Blend onto";
            this.LblBlendOnto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RbCustomPages
            // 
            this.RbCustomPages.AutoSize = true;
            this.RbCustomPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RbCustomPages.Location = new System.Drawing.Point(4, 31);
            this.RbCustomPages.Margin = new System.Windows.Forms.Padding(4);
            this.RbCustomPages.Name = "RbCustomPages";
            this.RbCustomPages.Size = new System.Drawing.Size(73, 23);
            this.RbCustomPages.TabIndex = 2;
            this.RbCustomPages.Text = "Custom";
            this.RbCustomPages.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.RbCustomPages.UseVisualStyleBackColor = true;
            // 
            // TxtCustomPages
            // 
            this.TxtCustomPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtCustomPages.Enabled = false;
            this.TxtCustomPages.ForeColor = System.Drawing.Color.DimGray;
            this.TxtCustomPages.Location = new System.Drawing.Point(85, 31);
            this.TxtCustomPages.Margin = new System.Windows.Forms.Padding(4);
            this.TxtCustomPages.MaxLength = 255;
            this.TxtCustomPages.Name = "TxtCustomPages";
            this.TxtCustomPages.Size = new System.Drawing.Size(164, 23);
            this.TxtCustomPages.TabIndex = 3;
            // 
            // RbAllPages
            // 
            this.RbAllPages.AutoSize = true;
            this.RbAllPages.Checked = true;
            this.RbAllPages.Location = new System.Drawing.Point(4, 4);
            this.RbAllPages.Margin = new System.Windows.Forms.Padding(4);
            this.RbAllPages.Name = "RbAllPages";
            this.RbAllPages.Size = new System.Drawing.Size(73, 19);
            this.RbAllPages.TabIndex = 4;
            this.RbAllPages.TabStop = true;
            this.RbAllPages.Text = "All pages";
            this.RbAllPages.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.RbAllPages.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.GbRenderOptions);
            this.panel1.Controls.Add(this.GbInformation);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(171, 423);
            this.panel1.TabIndex = 1;
            // 
            // GbRenderOptions
            // 
            this.GbRenderOptions.AutoSize = true;
            this.GbRenderOptions.Controls.Add(this.tableLayoutPanel2);
            this.GbRenderOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GbRenderOptions.Location = new System.Drawing.Point(0, 73);
            this.GbRenderOptions.Margin = new System.Windows.Forms.Padding(4);
            this.GbRenderOptions.Name = "GbRenderOptions";
            this.GbRenderOptions.Padding = new System.Windows.Forms.Padding(4);
            this.GbRenderOptions.Size = new System.Drawing.Size(171, 350);
            this.GbRenderOptions.TabIndex = 0;
            this.GbRenderOptions.TabStop = false;
            this.GbRenderOptions.Text = "Render options";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.ClbRenderOptions, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.LblRenderingFlagsDesc, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 20);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(163, 326);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // ClbRenderOptions
            // 
            this.ClbRenderOptions.BackColor = System.Drawing.SystemColors.Control;
            this.ClbRenderOptions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ClbRenderOptions.CheckOnClick = true;
            this.ClbRenderOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClbRenderOptions.FormattingEnabled = true;
            this.ClbRenderOptions.Location = new System.Drawing.Point(4, 4);
            this.ClbRenderOptions.Margin = new System.Windows.Forms.Padding(4);
            this.ClbRenderOptions.Name = "ClbRenderOptions";
            this.ClbRenderOptions.Size = new System.Drawing.Size(155, 303);
            this.ClbRenderOptions.TabIndex = 0;
            // 
            // LblRenderingFlagsDesc
            // 
            this.LblRenderingFlagsDesc.AutoSize = true;
            this.LblRenderingFlagsDesc.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LblRenderingFlagsDesc.Enabled = false;
            this.LblRenderingFlagsDesc.Location = new System.Drawing.Point(3, 311);
            this.LblRenderingFlagsDesc.Name = "LblRenderingFlagsDesc";
            this.LblRenderingFlagsDesc.Size = new System.Drawing.Size(157, 15);
            this.LblRenderingFlagsDesc.TabIndex = 1;
            // 
            // GbInformation
            // 
            this.GbInformation.AutoSize = true;
            this.GbInformation.Controls.Add(this.tableLayoutPanel1);
            this.GbInformation.Dock = System.Windows.Forms.DockStyle.Top;
            this.GbInformation.Location = new System.Drawing.Point(0, 0);
            this.GbInformation.Margin = new System.Windows.Forms.Padding(4);
            this.GbInformation.Name = "GbInformation";
            this.GbInformation.Padding = new System.Windows.Forms.Padding(4);
            this.GbInformation.Size = new System.Drawing.Size(171, 73);
            this.GbInformation.TabIndex = 1;
            this.GbInformation.TabStop = false;
            this.GbInformation.Text = "Information";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.LblLblPageSize, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.LblLblPageCount, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.LblLblPassword, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.LblPageCount, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.LblPassword, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.LblPageSize, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 20);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(163, 49);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // LblLblPageSize
            // 
            this.LblLblPageSize.AutoSize = true;
            this.LblLblPageSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblLblPageSize.Location = new System.Drawing.Point(5, 33);
            this.LblLblPageSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblLblPageSize.Name = "LblLblPageSize";
            this.LblLblPageSize.Size = new System.Drawing.Size(67, 15);
            this.LblLblPageSize.TabIndex = 0;
            this.LblLblPageSize.Text = "Page size";
            this.LblLblPageSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblLblPageCount
            // 
            this.LblLblPageCount.AutoSize = true;
            this.LblLblPageCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblLblPageCount.Location = new System.Drawing.Point(5, 1);
            this.LblLblPageCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblLblPageCount.Name = "LblLblPageCount";
            this.LblLblPageCount.Size = new System.Drawing.Size(67, 15);
            this.LblLblPageCount.TabIndex = 1;
            this.LblLblPageCount.Text = "Page count";
            this.LblLblPageCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblLblPassword
            // 
            this.LblLblPassword.AutoSize = true;
            this.LblLblPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblLblPassword.Location = new System.Drawing.Point(5, 17);
            this.LblLblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblLblPassword.Name = "LblLblPassword";
            this.LblLblPassword.Size = new System.Drawing.Size(67, 15);
            this.LblLblPassword.TabIndex = 2;
            this.LblLblPassword.Text = "Password";
            this.LblLblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblPageCount
            // 
            this.LblPageCount.AutoSize = true;
            this.LblPageCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblPageCount.Location = new System.Drawing.Point(81, 1);
            this.LblPageCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblPageCount.Name = "LblPageCount";
            this.LblPageCount.Size = new System.Drawing.Size(77, 15);
            this.LblPageCount.TabIndex = 3;
            this.LblPageCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblPassword
            // 
            this.LblPassword.AutoSize = true;
            this.LblPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblPassword.Location = new System.Drawing.Point(81, 17);
            this.LblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblPassword.Name = "LblPassword";
            this.LblPassword.Size = new System.Drawing.Size(77, 15);
            this.LblPassword.TabIndex = 4;
            this.LblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblPageSize
            // 
            this.LblPageSize.AutoSize = true;
            this.LblPageSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblPageSize.Location = new System.Drawing.Point(81, 33);
            this.LblPageSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblPageSize.Name = "LblPageSize";
            this.LblPageSize.Size = new System.Drawing.Size(77, 15);
            this.LblPageSize.TabIndex = 5;
            this.LblPageSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.AutoSize = true;
            this.tableLayoutPanel8.ColumnCount = 4;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.Controls.Add(this.UpdateAvailLabel, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.LnkForum, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.LnkGitHub, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.Size = new System.Drawing.Size(444, 15);
            this.tableLayoutPanel8.TabIndex = 3;
            // 
            // UpdateAvailLabel
            // 
            this.UpdateAvailLabel.AutoSize = true;
            this.UpdateAvailLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UpdateAvailLabel.Location = new System.Drawing.Point(106, 0);
            this.UpdateAvailLabel.Name = "UpdateAvailLabel";
            this.UpdateAvailLabel.Size = new System.Drawing.Size(335, 15);
            this.UpdateAvailLabel.TabIndex = 0;
            this.UpdateAvailLabel.Text = "An update is available";
            this.UpdateAvailLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.UpdateAvailLabel.Visible = false;
            // 
            // LnkForum
            // 
            this.LnkForum.AutoSize = true;
            this.LnkForum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LnkForum.Location = new System.Drawing.Point(57, 0);
            this.LnkForum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LnkForum.Name = "LnkForum";
            this.LnkForum.Size = new System.Drawing.Size(42, 15);
            this.LnkForum.TabIndex = 1;
            this.LnkForum.TabStop = true;
            this.LnkForum.Text = "Forum";
            this.LnkForum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LnkGitHub
            // 
            this.LnkGitHub.AutoSize = true;
            this.LnkGitHub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LnkGitHub.Location = new System.Drawing.Point(4, 0);
            this.LnkGitHub.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LnkGitHub.Name = "LnkGitHub";
            this.LnkGitHub.Size = new System.Drawing.Size(45, 15);
            this.LnkGitHub.TabIndex = 2;
            this.LnkGitHub.TabStop = true;
            this.LnkGitHub.Text = "GitHub";
            this.LnkGitHub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PdfImportDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(464, 441);
            this.Controls.Add(this.RootPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "PdfImportDialog";
            this.RootPanel1.ResumeLayout(false);
            this.RootPanel1.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.MainTableLayoutPanel.ResumeLayout(false);
            this.MainTableLayoutPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.GbDimensions.ResumeLayout(false);
            this.GbDimensions.PerformLayout();
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudHeight)).EndInit();
            this.GbLayers.ResumeLayout(false);
            this.GbLayers.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudLyrOpacity)).EndInit();
            this.GbPages.ResumeLayout(false);
            this.GbPages.PerformLayout();
            this.PagesTableLayoutPanel.ResumeLayout(false);
            this.PagesTableLayoutPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.GbRenderOptions.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.GbInformation.ResumeLayout(false);
            this.GbInformation.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Panel RootPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.LinkLabel LnkForum;
        private System.Windows.Forms.LinkLabel LnkGitHub;
        private System.Windows.Forms.Label UpdateAvailLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.ProgressBar ProgressBar1;
        private System.Windows.Forms.Label LblProgress;
        private System.Windows.Forms.TableLayoutPanel MainTableLayoutPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox GbDimensions;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label LblWidth;
        private System.Windows.Forms.Label LblPx2;
        private System.Windows.Forms.NumericUpDown NudWidth;
        private System.Windows.Forms.Label LblPx1;
        private System.Windows.Forms.NumericUpDown NudHeight;
        private System.Windows.Forms.Label LblHeight;
        private System.Windows.Forms.GroupBox GbLayers;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label LblLayerOpacity;
        private System.Windows.Forms.NumericUpDown NudLyrOpacity;
        private System.Windows.Forms.Label LblBlendMode;
        private System.Windows.Forms.ComboBox CbLyrBlendMode;
        private System.Windows.Forms.CheckBox CbInvisibleLayers;
        private System.Windows.Forms.CheckBox CbDescending;
        private System.Windows.Forms.CheckBox CbRestoreLayerProperties;
        private System.Windows.Forms.GroupBox GbPages;
        private System.Windows.Forms.TableLayoutPanel PagesTableLayoutPanel;
        private System.Windows.Forms.ComboBox CbBlendOnto;
        private System.Windows.Forms.Label LblBlendOnto;
        private System.Windows.Forms.RadioButton RbCustomPages;
        private System.Windows.Forms.TextBox TxtCustomPages;
        private System.Windows.Forms.RadioButton RbAllPages;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox GbRenderOptions;
        private System.Windows.Forms.CheckedListBox ClbRenderOptions;
        private System.Windows.Forms.GroupBox GbInformation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.CheckBox CbKeepAspectRatio;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button BtnReset;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label LblLblPageSize;
        private System.Windows.Forms.Label LblLblPageCount;
        private System.Windows.Forms.Label LblLblPassword;
        private System.Windows.Forms.Label LblPageCount;
        private System.Windows.Forms.Label LblPassword;
        private System.Windows.Forms.Label LblPageSize;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label LblRenderingFlagsDesc;
        private System.Windows.Forms.ComboBox CbAlignment;
        private System.Windows.Forms.Label LblAlignment;
    }
}
