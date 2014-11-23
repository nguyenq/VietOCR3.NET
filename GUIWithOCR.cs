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
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using VietOCR.NET.Controls;

namespace VietOCR.NET
{
    public partial class GUIWithOCR : VietOCR.NET.GUIWithImageOps
    {
        protected string selectedPSM = "Auto"; // 3 - Fully automatic page segmentation, but no OSD (default)

        public GUIWithOCR()
        {
            InitializeComponent();
        }

        protected override void ocrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.pictureBox1.Image == null)
            {
                MessageBox.Show(this, Properties.Resources.LoadImage, strProgName);
                return;
            }

            Rectangle rect = ((ScrollablePictureBox)this.pictureBox1).GetRect();

            if (rect != Rectangle.Empty)
            {
                try
                {
                    rect = new Rectangle((int)(rect.X * scaleX), (int)(rect.Y * scaleY), (int)(rect.Width * scaleX), (int)(rect.Height * scaleY));
                    performOCR(imageList, imageIndex, rect);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
            else
            {
                performOCR(imageList, imageIndex, Rectangle.Empty);
            }
        }

        protected override void ocrAllPagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.pictureBox1.Image == null)
            {
                MessageBox.Show(this, Properties.Resources.LoadImage, strProgName);
                return;
            }

            this.toolStripBtnOCR.Visible = false;
            this.toolStripButtonCancelOCR.Visible = true;
            this.toolStripButtonCancelOCR.Enabled = true;
            performOCR(imageList, -1, Rectangle.Empty);
        }

        /// <summary>
        /// Perform OCR on pages of image.
        /// </summary>
        /// <param name="imageList"></param>
        /// <param name="index">-1 for all pages</param>
        /// <param name="rect">selection rectangle</param>
        void performOCR(IList<Image> imageList, int index, Rectangle rect)
        {
            try
            {
                if (curLangCode.Trim().Length == 0)
                {
                    MessageBox.Show(this, Properties.Resources.selectLanguage, strProgName);
                    return;
                }

                this.toolStripStatusLabel1.Text = Properties.Resources.OCRrunning;
                this.Cursor = Cursors.WaitCursor;
                this.pictureBox1.UseWaitCursor = true;
                this.textBox1.Cursor = Cursors.WaitCursor;
                this.toolStripBtnOCR.Enabled = false;
                this.oCRToolStripMenuItem.Enabled = false;
                this.oCRAllPagesToolStripMenuItem.Enabled = false;
                this.toolStripProgressBar1.Enabled = true;
                this.toolStripProgressBar1.Visible = true;
                this.toolStripProgressBar1.Style = ProgressBarStyle.Marquee;

                OCRImageEntity entity = new OCRImageEntity(imageList, index, rect, curLangCode);
                entity.ScreenshotMode = this.screenshotModeToolStripMenuItem.Checked;

                // Start the asynchronous operation.
                backgroundWorkerOcr.RunWorkerAsync(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void toolStripButtonCancelOCR_Click(object sender, EventArgs e)
        {
            backgroundWorkerOcr.CancelAsync();
            this.toolStripButtonCancelOCR.Enabled = false;
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void backgroundWorkerOcr_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            OCRImageEntity entity = (OCRImageEntity)e.Argument;
            OCR<Image> ocrEngine = new OCRImages();
            ocrEngine.PageSegMode = selectedPSM;
            ocrEngine.Language = entity.Language;

            // Assign the result of the computation to the Result property of the DoWorkEventArgs
            // object. This is will be available to the RunWorkerCompleted eventhandler.
            //e.Result = ocrEngine.RecognizeText(entity.ClonedImages, entity.Lang, entity.Rect, worker, e);
            IList<Image> images = entity.ClonedImages;

            for (int i = 0; i < images.Count; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                string result = ocrEngine.RecognizeText(((List<Image>)images).GetRange(i, 1), entity.Rect, worker, e);
                worker.ReportProgress(i, result); // i is not really percentage
            }
        }

        //[System.Diagnostics.DebuggerNonUserCodeAttribute()]
        //private void backgroundWorkerOcr_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    // Get the BackgroundWorker that raised this event.
        //    BackgroundWorker worker = sender as BackgroundWorker;

        //    OCRImageEntity entity = (OCRImageEntity)e.Argument;
        //    OCR<string> ocrEngine = new OCRFiles();
        //    ocrEngine.PageSegMode = selectedPSM;
        //    ocrEngine.Language = entity.Language;

        //    IList<string> workingImageFiles = entity.ImageFiles;

        //    foreach (string workingImageFile in workingImageFiles)
        //    {
        //        tempFileCollection.AddFile(workingImageFile, false); // to be deleted when program exits
        //    }

        //    for (int i = 0; i < workingImageFiles.Count; i++)
        //    {
        //        if (worker.CancellationPending)
        //        {
        //            e.Cancel = true;
        //            break;
        //        }

        //        string result = ocrEngine.RecognizeText(((List<string>)workingImageFiles).GetRange(i, 1), worker, e);
        //        worker.ReportProgress(i, result); // i is not really percentage
        //    }
        //}

        private void backgroundWorkerOcr_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //this.toolStripProgressBar1.Value = e.ProgressPercentage;
            this.textBox1.AppendText((string)e.UserState);
        }

        private void backgroundWorkerOcr_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.toolStripProgressBar1.Enabled = false;
            this.toolStripProgressBar1.Visible = false;

            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                this.toolStripStatusLabel1.Text = String.Empty;
                MessageBox.Show(e.Error.Message, Properties.Resources.OCROperation, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled the operation.
                // Note that due to a race condition in the DoWork event handler, the Cancelled
                // flag may not have been set, even though CancelAsync was called.
                this.toolStripStatusLabel1.Text = "OCR " + Properties.Resources.canceled;
            }
            else
            {
                // Finally, handle the case where the operation succeeded.
                this.toolStripStatusLabel1.Text = Properties.Resources.OCRcompleted;
                //this.textBox1.AppendText(e.Result.ToString());
            }

            this.Cursor = Cursors.Default;
            this.pictureBox1.UseWaitCursor = false;
            this.textBox1.Cursor = Cursors.Default;
            this.toolStripBtnOCR.Visible = true;
            this.toolStripBtnOCR.Enabled = true;
            this.oCRToolStripMenuItem.Enabled = true;
            this.oCRAllPagesToolStripMenuItem.Enabled = true;
            this.toolStripButtonCancelOCR.Visible = false;
        }
    }
}
