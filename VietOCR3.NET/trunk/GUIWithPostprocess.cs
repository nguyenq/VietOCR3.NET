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
using VietOCR.NET.Postprocessing;
using System.IO;
using Microsoft.Win32;

namespace VietOCR.NET
{
    public partial class GUIWithPostprocess : VietOCR.NET.GUIWithOCR
    {
        const string strDangAmbigsPath = "DangAmbigsPath";
        const string strDangAmbigsOn = "DangAmbigsOn";

        protected string dangAmbigsPath;
        protected bool dangAmbigsOn;

        public GUIWithPostprocess()
        {
            InitializeComponent();
        }

        protected override void postprocessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (curLangCode == null) return;

            this.toolStripStatusLabel1.Text = Properties.Resources.Correcting_errors;
            this.Cursor = Cursors.WaitCursor;
            this.pictureBox1.UseWaitCursor = true;
            this.textBox1.Cursor = Cursors.WaitCursor;
            this.postprocessToolStripMenuItem.Enabled = false;
            this.toolStripProgressBar1.Enabled = true;
            this.toolStripProgressBar1.Visible = true;
            this.toolStripProgressBar1.Style = ProgressBarStyle.Marquee;

            this.backgroundWorkerCorrect.RunWorkerAsync(this.textBox1.SelectionLength > 0 ? this.textBox1.SelectedText : this.textBox1.Text);
        }

        private void backgroundWorkerCorrect_DoWork(object sender, DoWorkEventArgs e)
        {
            // Perform post-OCR corrections
            string text = (string)e.Argument;
            e.Result = Processor.PostProcess(text, curLangCode, dangAmbigsPath, dangAmbigsOn);
        }

        private void backgroundWorkerCorrect_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.toolStripProgressBar1.Enabled = false;
            this.toolStripProgressBar1.Visible = false;

            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                Console.WriteLine(e.Error.StackTrace);
                string why;

                if (e.Error.GetBaseException() is NotSupportedException)
                {
                    why = string.Format("Post-processing not supported for {0} language.\nYou can provide one via a \"{1}.DangAmbigs.txt\" file.", this.toolStripCbLang.Text, curLangCode);
                }
                else
                {
                    why = e.Error.Message;
                }
                MessageBox.Show(this, why, strProgName);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                this.toolStripStatusLabel1.Text = "Post-OCR correction " + Properties.Resources.canceled;
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                string result = e.Result.ToString();

                if (this.textBox1.SelectionLength > 0)
                {
                    int start = this.textBox1.SelectionStart;
                    this.textBox1.SelectedText = result;
                    this.textBox1.Select(start, result.Length);
                }
                else
                {
                    this.textBox1.Text = result;
                }
                this.toolStripStatusLabel1.Text = Properties.Resources.Correcting_completed;
            }

            this.Cursor = Cursors.Default;
            this.pictureBox1.UseWaitCursor = false;
            this.textBox1.Cursor = Cursors.Default;
            this.postprocessToolStripMenuItem.Enabled = true;
        }

        protected override void LoadRegistryInfo(RegistryKey regkey)
        {
            base.LoadRegistryInfo(regkey);

            dangAmbigsPath = (string)regkey.GetValue(strDangAmbigsPath, Path.Combine(baseDir, "Data"));
            dangAmbigsOn = Convert.ToBoolean(
                (int)regkey.GetValue(strDangAmbigsOn, Convert.ToInt32(true)));
        }

        protected override void SaveRegistryInfo(RegistryKey regkey)
        {
            base.SaveRegistryInfo(regkey);

            regkey.SetValue(strDangAmbigsPath, dangAmbigsPath);
            regkey.SetValue(strDangAmbigsOn, Convert.ToInt32(dangAmbigsOn));
        }
    }
}
