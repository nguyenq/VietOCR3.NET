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
using System.Diagnostics;
using System.Threading;
using System.Globalization;

namespace VietOCR.NET
{
    public partial class GUIWithBulkOCR : VietOCR.NET.GUIWithPostprocess
    {
        const string strInputFolder = "InputFolder";
        const string strBulkOutputFolder = "BulkOutputFolder";
        const string strBulkOutputFormat = "BulkOutputFormat";

        private string inputFolder;
        private string outputFolder;
        private string outputFormat;

        private BulkDialog bulkDialog;
        private StatusForm statusForm;

        Stopwatch stopWatch = new Stopwatch();
        delegate void UpdateStatusEvent(string message);

        public GUIWithBulkOCR()
        {
            InitializeComponent();
            statusForm = new StatusForm();
            statusForm.Text = Properties.Resources.BulkProcessStatus;
        }

        /// <summary>
        /// Changes localized text and messages
        /// </summary>
        /// <param name="locale"></param>
        /// <param name="firstTime"></param>
        protected override void ChangeUILanguage(string locale)
        {
            base.ChangeUILanguage(locale);

            statusForm.Text = Properties.Resources.BulkProcessStatus;
            
            if (bulkDialog != null)
            {
                bulkDialog.ChangeUILanguage(locale);
            }
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
            bulkDialog.OutputFolder = outputFolder;
            bulkDialog.OutputFormat = outputFormat;

            if (bulkDialog.ShowDialog() == DialogResult.OK)
            {
                inputFolder = bulkDialog.InputFolder;
                outputFolder = bulkDialog.OutputFolder;
                outputFormat = bulkDialog.OutputFormat;

                this.toolStripStatusLabel1.Text = Properties.Resources.OCRrunning;
                this.Cursor = Cursors.WaitCursor;
                this.pictureBox1.UseWaitCursor = true;
                this.textBox1.Cursor = Cursors.WaitCursor;
                this.toolStripProgressBar1.Enabled = true;
                this.toolStripProgressBar1.Visible = true;
                this.toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
                this.bulkOCRToolStripMenuItem.Text = Properties.Resources.CancelBulkOCR;

                if (this.statusForm.IsDisposed)
                {
                    this.statusForm = new StatusForm();
                    statusForm.Text = Properties.Resources.BulkProcessStatus;
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
                this.statusForm.TextBox.AppendText("\t-- " + Properties.Resources.Beginning_of_task + " --" + Environment.NewLine);

                // start bulk OCR
                stopWatch.Start();
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
                files.AddRange(Directory.GetFiles(inputFolder, filter, SearchOption.AllDirectories));
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
                performOCR(imageFile);
            }
        }

        private void performOCR(FileInfo imageFile)
        {
            try
            {
                string outputFilename = imageFile.FullName.Substring(inputFolder.Length + 1);
                OCRHelper.PerformOCR(imageFile.FullName, Path.Combine(outputFolder, outputFilename), curLangCode, selectedPSM, outputFormat);
            }
            catch
            {
                // Sets the UI culture to the selected language.
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedUILanguage);

                this.statusForm.TextBox.BeginInvoke(new UpdateStatusEvent(this.WorkerUpdate), new Object[] { "\t** " + Properties.Resources.Cannotprocess + imageFile.Name + " **" });
            }
        }

        void WorkerUpdate(string message)
        {
            this.statusForm.TextBox.AppendText(message + Environment.NewLine);
        }

        private void backgroundWorkerBulk_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (this.statusForm.IsDisposed)
            {
                this.statusForm = new StatusForm();
                statusForm.Text = Properties.Resources.BulkProcessStatus;
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
                this.statusForm.TextBox.AppendText("\t-- " + Properties.Resources.Task_canceled + " --" + Environment.NewLine);
            }
            else
            {
                // Finally, handle the case where the operation succeeded.
                this.toolStripStatusLabel1.Text = Properties.Resources.OCRcompleted;
                this.statusForm.TextBox.AppendText("\t-- " + Properties.Resources.End_of_task + " --" + Environment.NewLine);
            }

            this.Cursor = Cursors.Default;
            this.pictureBox1.UseWaitCursor = false;
            this.textBox1.Cursor = Cursors.Default;
            this.bulkOCRToolStripMenuItem.Text = Properties.Resources.Bulk_OCR + "...";

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value. 
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
            this.statusForm.TextBox.AppendText("\t" + Properties.Resources.Elapsed_time + ": " + elapsedTime + Environment.NewLine);
        }

        protected override void LoadRegistryInfo(RegistryKey regkey)
        {
            base.LoadRegistryInfo(regkey);
            inputFolder = (string)regkey.GetValue(strInputFolder, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            outputFolder = (string)regkey.GetValue(strBulkOutputFolder, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            outputFormat = (string)regkey.GetValue(strBulkOutputFormat, "text");
        }

        protected override void SaveRegistryInfo(RegistryKey regkey)
        {
            base.SaveRegistryInfo(regkey);
            regkey.SetValue(strInputFolder, inputFolder);
            regkey.SetValue(strBulkOutputFolder, outputFolder);
            regkey.SetValue(strBulkOutputFormat, outputFormat);
        }
    }
}
