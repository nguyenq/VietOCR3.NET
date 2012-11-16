/**
 * Copyright @ 2012 Quan Nguyen
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
using System.IO;
using VietOCR.NET.Utilities;
using VietOCR.NET.Postprocessing;
using Microsoft.Win32;

namespace VietOCR.NET
{
    public partial class GUIWithBulkOCR : VietOCR.NET.GUIWithPostprocess
    {
        const string strInputFolder = "InputFolder";
        const string strBulkOutputFolder = "BulkOutputFolder";

        private string inputFolder;
        private string bulkOutputFolder;

        private BulkDialog bulkDialog;
        private StatusForm statusForm;

        public GUIWithBulkOCR()
        {
            InitializeComponent();
            statusForm = new StatusForm();
            statusForm.Text = Properties.Resources.BatchProcessStatus;
        }

        /// <summary>
        /// Changes localized text and messages
        /// </summary>
        /// <param name="locale"></param>
        /// <param name="firstTime"></param>
        protected override void ChangeUILanguage(string locale)
        {
            base.ChangeUILanguage(locale);

            statusForm.Text = Properties.Resources.BatchProcessStatus;
        }

        protected override void bulkOCRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (backgroundWorkerBulk != null && backgroundWorkerBulk.IsBusy)
            {
                backgroundWorkerBulk.CancelAsync();
                return;
            }
            if (bulkDialog == null)
            {
                bulkDialog = new BulkDialog();
            }

            bulkDialog.InputFolder = inputFolder;
            bulkDialog.OutputFolder = bulkOutputFolder;

            if (bulkDialog.ShowDialog() == DialogResult.OK)
            {
                inputFolder = bulkDialog.InputFolder;
                bulkOutputFolder = bulkDialog.OutputFolder;

                this.toolStripStatusLabel1.Text = Properties.Resources.OCRrunning;
                this.Cursor = Cursors.WaitCursor;
                this.pictureBox1.UseWaitCursor = true;
                this.textBox1.Cursor = Cursors.WaitCursor;
                this.toolStripProgressBar1.Enabled = true;
                this.toolStripProgressBar1.Visible = true;
                this.toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
                this.bulkOCRToolStripMenuItem.Text = "Cancel Bulk OCR";

                if (this.statusForm.IsDisposed)
                {
                    this.statusForm = new StatusForm();
                    statusForm.Text = Properties.Resources.BatchProcessStatus;
                }
                if (!this.statusForm.Visible)
                {
                    this.statusForm.Show();
                }
                else if (this.statusForm.WindowState == FormWindowState.Minimized)
                {
                    this.statusForm.WindowState = FormWindowState.Normal;
                }
                this.statusForm.BringToFront();
                this.statusForm.TextBox.AppendText("\t-- Beginning of task --" + Environment.NewLine);

                // start bulk OCR
                this.backgroundWorkerBulk.RunWorkerAsync();
            }
        }

        private void backgroundWorkerBulk_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;
            string imageFilters = "*.tif|*.tiff|*.jpg|*.jpeg|*.gif|*.png|*.bmp|*.pdf";
            List<string> files = new List<string>();
            foreach (string filter in imageFilters.Split('|'))
            {
                files.AddRange(Directory.GetFiles(inputFolder, filter));
            }

            for (int i = 0; i < files.Count; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                FileInfo imageFile = new FileInfo(files[i]);
                worker.ReportProgress(i, imageFile.FullName);
                IList<Image> imageList = ImageIOHelper.GetImageList(imageFile);
                OCR<Image> ocrEngine = new OCRImages();
                ocrEngine.PageSegMode = selectedPSM;
                string result = ocrEngine.RecognizeText(imageList, curLangCode);

                // postprocess to correct common OCR errors
                result = Processor.PostProcess(result, curLangCode);
                // correct common errors caused by OCR
                result = TextUtilities.CorrectOCRErrors(result);
                // correct letter cases
                result = TextUtilities.CorrectLetterCases(result);

                using (StreamWriter sw = new StreamWriter(Path.Combine(bulkOutputFolder, imageFile.Name + ".txt"), false, new System.Text.UTF8Encoding()))
                {
                    sw.Write(result);
                }
            }
        }

        private void backgroundWorkerBulk_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (this.statusForm.IsDisposed)
            {
                this.statusForm = new StatusForm();
                statusForm.Text = Properties.Resources.BatchProcessStatus;
            }
            if (!this.statusForm.Visible)
            {
                this.statusForm.Show();
            }

            this.statusForm.TextBox.AppendText((string)e.UserState + Environment.NewLine);
        }

        private void backgroundWorkerBulk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.toolStripProgressBar1.Enabled = false;
            this.toolStripProgressBar1.Visible = false;

            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                this.toolStripStatusLabel1.Text = String.Empty;
                this.statusForm.TextBox.AppendText(e.Error.Message + Environment.NewLine);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled the operation.
                // Note that due to a race condition in the DoWork event handler, the Cancelled
                // flag may not have been set, even though CancelAsync was called.
                this.toolStripStatusLabel1.Text = "OCR " + Properties.Resources.canceled;
                this.statusForm.TextBox.AppendText("*** " + this.toolStripStatusLabel1.Text + " ***");
            }
            else
            {
                // Finally, handle the case where the operation succeeded.
                this.toolStripStatusLabel1.Text = Properties.Resources.OCRcompleted;
                this.statusForm.TextBox.AppendText("\t-- End of task --" + Environment.NewLine);
            }

            this.Cursor = Cursors.Default;
            this.pictureBox1.UseWaitCursor = false;
            this.textBox1.Cursor = Cursors.Default;
            this.bulkOCRToolStripMenuItem.Text = "Bulk OCR...";
        }

        protected override void LoadRegistryInfo(RegistryKey regkey)
        {
            base.LoadRegistryInfo(regkey);
            inputFolder = (string)regkey.GetValue(strInputFolder, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            bulkOutputFolder = (string)regkey.GetValue(strBulkOutputFolder, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }

        protected override void SaveRegistryInfo(RegistryKey regkey)
        {
            base.SaveRegistryInfo(regkey);
            regkey.SetValue(strInputFolder, inputFolder);
            regkey.SetValue(strBulkOutputFolder, bulkOutputFolder);
        }
    }
}
