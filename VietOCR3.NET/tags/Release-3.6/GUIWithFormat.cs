/**
 * Copyright @ 2008 Quan Nguyen
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Globalization;
using Net.SourceForge.Vietpad.Utilities;
using VietOCR.NET.Utilities;

namespace VietOCR.NET
{
    public partial class GUIWithFormat : VietOCR.NET.GUIWithImage
    {
        const string strSelectedCase = "SelectedCase";

        private string selectedCase;

        public GUIWithFormat()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Changes localized text and messages
        /// </summary>
        /// <param name="locale"></param>
        protected override void ChangeUILanguage(string locale)
        {
            base.ChangeUILanguage(locale);

            foreach (Form form in this.OwnedForms)
            {
                ChangeCaseDialog changeCaseDlg = form as ChangeCaseDialog;
                if (changeCaseDlg != null)
                {
                    FormLocalizer localizer = new FormLocalizer(changeCaseDlg, typeof(ChangeCaseDialog));
                    localizer.ApplyCulture(new CultureInfo(locale));
                    break;
                }
            }
        }
        protected override void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            mi.Checked ^= true;
            this.textBox1.WordWrap = mi.Checked;
        }

        protected override void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontdlg = new FontDialog();

            fontdlg.ShowColor = true;
            fontdlg.Font = this.textBox1.Font;
            fontdlg.Color = this.textBox1.ForeColor;

            if (fontdlg.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Font = fontdlg.Font;
                this.textBox1.ForeColor = fontdlg.Color;
            }
        }

        protected override void changeCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OwnedForms.Length > 0)
            {
                foreach (Form form in this.OwnedForms)
                {
                    ChangeCaseDialog changeCaseDlg1 = form as ChangeCaseDialog;
                    if (changeCaseDlg1 != null)
                    {
                        changeCaseDlg1.Show();
                        return;
                    }
                }
            }

            textBox1.HideSelection = false;

            ChangeCaseDialog changeCaseDlg = new ChangeCaseDialog();
            changeCaseDlg.Owner = this;
            changeCaseDlg.SelectedCase = selectedCase;
            changeCaseDlg.ChangeCase += new EventHandler(ChangeCaseDialogChangeCase);
            changeCaseDlg.CloseDlg += new EventHandler(ChangeCaseDialogCloseDlg);

            if (textBox1.SelectedText == "")
            {
                textBox1.SelectAll();
            }
            changeCaseDlg.Show();
        }

        void ChangeCaseDialogChangeCase(object obj, EventArgs ea)
        {
            if (textBox1.SelectedText == "")
            {
                textBox1.SelectAll();
                return;
            }

            ChangeCaseDialog dlg = (ChangeCaseDialog)obj;
            selectedCase = dlg.SelectedCase;
            changeCase(selectedCase);
        }
        
        void ChangeCaseDialogCloseDlg(object obj, EventArgs ea)
        {
            //textBox1.HideSelection = true;
            this.Focus();
        }

        /// <summary>
        /// Changes letter case.
        /// </summary>
        /// <param name="typeOfCase"></param>
        private void changeCase(string typeOfCase)
        {
            int start = textBox1.SelectionStart;
            string result = TextUtilities.ChangeCase(textBox1.SelectedText, typeOfCase);
            textBox1.SelectedText = result;
            textBox1.Select(start, result.Length);
        }

        /// <summary>
        /// Removes line breaks.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void removeLineBreaksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectedText == "")
            {
                textBox1.SelectAll();
                if (textBox1.SelectedText == "") return;
            }

            int start = textBox1.SelectionStart;
            string result = TextUtilities.RemoveLineBreaks(textBox1.SelectedText);
            textBox1.SelectedText = result;
            textBox1.Select(start, result.Length);
        }

        protected override void LoadRegistryInfo(RegistryKey regkey)
        {
            base.LoadRegistryInfo(regkey);
            selectedCase = (string)regkey.GetValue(strSelectedCase, String.Empty);
        }

        protected override void SaveRegistryInfo(RegistryKey regkey)
        {
            base.SaveRegistryInfo(regkey);
            regkey.SetValue(strSelectedCase, selectedCase);
        }
    }
}
