// Copyright 2022 Osman Tunçelli. All rights reserved.
// Use of this source code is governed by GNU General Public License (GPL-2.0) that can be found in the COPYING file.

using System.Windows.Forms;
using PdfFileTypePlugin.Localization;

namespace PdfFileTypePlugin.Import
{
    internal partial class PasswordInputDialog : Form
    {
        public string Password => TxtPwd.Text;

        public PasswordInputDialog()
        {
            Owner = UI.MainForm;
            InitializeComponent();

            Label.Text = StringResources.PleaseEnterThePassword;
            BtnOK.Text = StringResources.OK;
            BtnCancel.Text = StringResources.Cancel;

            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = MinimizeBox = false;
            ShowIcon = false;
            UI.ApplyTheme(this, immersive: false);

            TxtPwd.KeyDown += TxtPwd_KeyDown;
            _ = TxtPwd.Focus();
        }

        private void TxtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DialogResult = DialogResult.OK;
                return;
            }

            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}