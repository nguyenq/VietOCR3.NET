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
using Microsoft.Win32;
using System.IO;
using System.Collections;
using VietOCR.NET.Utilities;

namespace VietOCR.NET
{
    public partial class GUIWithTools : VietOCR.NET.GUIWithSpellcheck
    {
        const string strImageFolder = "ImageFolder";

        string imageFolder;
        int filterIndex;

        public GUIWithTools()
        {
            InitializeComponent();
        }

        protected override void mergeTiffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = imageFolder;
            openFileDialog1.Title = Properties.Resources.Select_Input_Images;
            openFileDialog1.Filter = "Image Files (*.tif;*.tiff)|*.tif;*.tiff|Image Files (*.bmp)|*.bmp|Image Files (*.jpg;*.jpeg)|*.jpg;*.jpeg|Image Files (*.png)|*.png|All Image Files|*.tif;*.tiff;*.bmp;*.jpg;*.jpeg;*.png";
            openFileDialog1.FilterIndex = filterIndex;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filterIndex = openFileDialog1.FilterIndex;
                imageFolder = Path.GetDirectoryName(openFileDialog1.FileName);
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = imageFolder;
                saveFileDialog1.Title = Properties.Resources.Save_Multipage_TIFF_Image;
                saveFileDialog1.Filter = "Image Files (*.tif;*.tiff)|*.tif;*.tiff";
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    File.Delete(saveFileDialog1.FileName);

                    ArrayList args = new ArrayList();
                    args.Add(openFileDialog1.FileNames);
                    args.Add(saveFileDialog1.FileName);

                    this.Cursor = Cursors.WaitCursor;
                    this.toolStripStatusLabel1.Text = Properties.Resources.MergeTIFF_running;
                    //this.pictureBox1.UseWaitCursor = true;
                    this.textBox1.Cursor = Cursors.WaitCursor;
                    this.toolStripProgressBar1.Enabled = true;
                    this.toolStripProgressBar1.Visible = true;
                    this.toolStripProgressBar1.Style = ProgressBarStyle.Marquee;

                    // Start the asynchronous operation.
                    backgroundWorkerMergeTiff.RunWorkerAsync(args);
                }
            }
        }

        private void backgroundWorkerMergeTiff_DoWork(object sender, DoWorkEventArgs e)
        {
            ArrayList args = (ArrayList)e.Argument;
            string[] inputFiles = (string[])args[0];
            string outputFile = (string)args[1];
            ImageIOHelper.MergeTiff(inputFiles, outputFile);
            e.Result = outputFile;
        }

        private void backgroundWorkerMergeTiff_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void backgroundWorkerMergeTiff_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.toolStripProgressBar1.Enabled = false;
            this.toolStripProgressBar1.Visible = false;

            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                this.toolStripStatusLabel1.Text = String.Empty;
                MessageBox.Show(this, e.Error.Message, strProgName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled the operation.
                // Note that due to a race condition in the DoWork event handler, the Cancelled
                // flag may not have been set, even though CancelAsync was called.
                this.toolStripStatusLabel1.Text = Properties.Resources.canceled;
            }
            else
            {
                // Finally, handle the case where the operation succeeded.
                this.toolStripStatusLabel1.Text = Properties.Resources.MergeTIFFcompleted;
                MessageBox.Show(this, Properties.Resources.MergeTIFFcompleted + Path.GetFileName(e.Result.ToString()) + Properties.Resources.created, strProgName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.toolStripStatusLabel1.Text = String.Empty;
            this.textBox1.Cursor = Cursors.Default;
            this.Cursor = Cursors.Default;
        }

        protected override void splitTiffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = imageFolder;
            openFileDialog1.Title = Properties.Resources.Select_Input_TIFF;
            openFileDialog1.Filter = "Image Files (*.tif;*.tiff)|*.tif;*.tiff";
            //openFileDialog1.FilterIndex = filterIndex;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //filterIndex = openFileDialog1.FilterIndex;
                imageFolder = Path.GetDirectoryName(openFileDialog1.FileName);

                this.Cursor = Cursors.WaitCursor;
                this.toolStripStatusLabel1.Text = Properties.Resources.SplitTIFF_running;
                //this.pictureBox1.UseWaitCursor = true;
                this.textBox1.Cursor = Cursors.WaitCursor;
                this.toolStripProgressBar1.Enabled = true;
                this.toolStripProgressBar1.Visible = true;
                this.toolStripProgressBar1.Style = ProgressBarStyle.Marquee;

                // Start the asynchronous operation.
                backgroundWorkerSplitTiff.RunWorkerAsync(openFileDialog1.FileName);
            }
        }

        private void backgroundWorkerSplitTiff_DoWork(object sender, DoWorkEventArgs e)
        {
            string infilename = (string) e.Argument;
            string basefilename = Path.Combine(Path.GetDirectoryName(infilename), Path.GetFileNameWithoutExtension(infilename));

            IList<string> filenames = ImageIOHelper.SplitMultipageTiff(new FileInfo(infilename));
            
            // move temp TIFF files to selected folder
            for (int i = 0; i < filenames.Count; i++)
            {
                string outfilename = String.Format("{0}-{1:000}.tif", basefilename, i + 1);
                File.Delete(outfilename);
                File.Move(filenames[i], outfilename);
            }
            //e.Result = filenames;
        }

        private void backgroundWorkerSplitTiff_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void backgroundWorkerSplitTiff_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.toolStripProgressBar1.Enabled = false;
            this.toolStripProgressBar1.Visible = false;

            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                this.toolStripStatusLabel1.Text = String.Empty;
                MessageBox.Show(this, e.Error.Message, strProgName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled the operation.
                // Note that due to a race condition in the DoWork event handler, the Cancelled
                // flag may not have been set, even though CancelAsync was called.
                this.toolStripStatusLabel1.Text = Properties.Resources.canceled;
            }
            else
            {
                // Finally, handle the case where the operation succeeded.
                this.toolStripStatusLabel1.Text = Properties.Resources.SplitTIFFcompleted;
                MessageBox.Show(this, Properties.Resources.SplitTIFFcompleted, strProgName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.toolStripStatusLabel1.Text = String.Empty;
            this.textBox1.Cursor = Cursors.Default;
            this.Cursor = Cursors.Default;
        }

        protected override void splitPdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SplitPdfDialog dialog = new SplitPdfDialog();
            dialog.Owner = this;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                this.toolStripStatusLabel1.Text = Properties.Resources.SplitPDF_running;
                //this.pictureBox1.UseWaitCursor = true;
                this.textBox1.Cursor = Cursors.WaitCursor;
                this.toolStripProgressBar1.Enabled = true;
                this.toolStripProgressBar1.Visible = true;
                this.toolStripProgressBar1.Style = ProgressBarStyle.Marquee;

                // Start the asynchronous operation.
                backgroundWorkerSplitPdf.RunWorkerAsync(dialog.Args);
            }
        }

        private void backgroundWorkerSplitPdf_DoWork(object sender, DoWorkEventArgs e)
        {
            SplitPdfArgs args = (SplitPdfArgs)e.Argument;

            if (args.Pages)
            {
                PdfUtilities.SplitPdf(args.InputFilename, args.OutputFilename, args.FromPage, args.ToPage);
            }
            else
            {
                string outputFilename = String.Empty;

                if (args.OutputFilename.EndsWith(".pdf"))
                {
                    outputFilename = args.OutputFilename.Substring(0, args.OutputFilename.LastIndexOf(".pdf"));
                }

                int pageCount = PdfUtilities.GetPdfPageCount(args.InputFilename);
                if (pageCount == 0)
                {
                    throw new ApplicationException("Split PDF failed.");
                }

                int pageRange = Int32.Parse(args.NumOfPages);
                int startPage = 1;

                while (startPage <= pageCount)
                {
                    int endPage = startPage + pageRange - 1;
                    string outputFile = outputFilename + startPage + ".pdf";
                    PdfUtilities.SplitPdf(args.InputFilename, outputFile, startPage.ToString(), endPage.ToString());
                    startPage = endPage + 1;
                }
            }

            e.Result = args.OutputFilename;
        }
        private void backgroundWorkerSplitPdf_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }
        private void backgroundWorkerSplitPdf_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.toolStripProgressBar1.Enabled = false;
            this.toolStripProgressBar1.Visible = false;
            
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                this.toolStripStatusLabel1.Text = String.Empty;
                MessageBox.Show(this, e.Error.Message, strProgName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled the operation.
                // Note that due to a race condition in the DoWork event handler, the Cancelled
                // flag may not have been set, even though CancelAsync was called.
                this.toolStripStatusLabel1.Text = Properties.Resources.canceled;
            }
            else
            {
                // Finally, handle the case where the operation succeeded.
                this.toolStripStatusLabel1.Text = Properties.Resources.SplitPDF_completed;
                MessageBox.Show(this, Properties.Resources.SplitPDF_completed + "\n" + Properties.Resources.check_output_in + Path.GetDirectoryName(e.Result.ToString()), strProgName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.toolStripStatusLabel1.Text = String.Empty;
            this.textBox1.Cursor = Cursors.Default;
            this.Cursor = Cursors.Default;
        }

        protected override void mergePdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = imageFolder;
            openFileDialog1.Title = Properties.Resources.Select_Input_PDFs;
            openFileDialog1.Filter = "PDF Files (*.pdf)|*.pdf";
            openFileDialog1.FilterIndex = filterIndex;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filterIndex = openFileDialog1.FilterIndex;
                imageFolder = Path.GetDirectoryName(openFileDialog1.FileName);
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = imageFolder;
                saveFileDialog1.Title = Properties.Resources.Save_Merged_PDF;
                saveFileDialog1.Filter = "PDF Files (*.pdf)|*.pdf";
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ArrayList args = new ArrayList();
                    args.Add(openFileDialog1.FileNames);
                    args.Add(saveFileDialog1.FileName);

                    this.Cursor = Cursors.WaitCursor;
                    this.toolStripStatusLabel1.Text = Properties.Resources.MergePDF_running;
                    //this.pictureBox1.UseWaitCursor = true;
                    this.textBox1.Cursor = Cursors.WaitCursor;
                    this.toolStripProgressBar1.Enabled = true;
                    this.toolStripProgressBar1.Visible = true;
                    this.toolStripProgressBar1.Style = ProgressBarStyle.Marquee;

                    // Start the asynchronous operation.
                    backgroundWorkerMergePdf.RunWorkerAsync(args);
                }
            }
        }

        private void backgroundWorkerMergePdf_DoWork(object sender, DoWorkEventArgs e)
        {
            ArrayList args = (ArrayList)e.Argument;
            string[] inputFiles = (string[])args[0];
            string outputFile = (string)args[1];
            PdfUtilities.MergePdf(inputFiles, outputFile);
            e.Result = outputFile;
        }

        private void backgroundWorkerMergePdf_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorkerMergePdf_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.toolStripProgressBar1.Enabled = false;
            this.toolStripProgressBar1.Visible = false;

            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                this.toolStripStatusLabel1.Text = String.Empty;
                MessageBox.Show(this, e.Error.Message, strProgName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled the operation.
                // Note that due to a race condition in the DoWork event handler, the Cancelled
                // flag may not have been set, even though CancelAsync was called.
                this.toolStripStatusLabel1.Text = Properties.Resources.canceled;
            }
            else
            {
                // Finally, handle the case where the operation succeeded.
                this.toolStripStatusLabel1.Text = Properties.Resources.MergePDFcompleted;
                MessageBox.Show(this, Properties.Resources.MergePDFcompleted + Path.GetFileName(e.Result.ToString()) + Properties.Resources.created, strProgName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.toolStripStatusLabel1.Text = String.Empty;
            this.textBox1.Cursor = Cursors.Default;
            this.Cursor = Cursors.Default;
        }        
        
        protected override void LoadRegistryInfo(RegistryKey regkey)
        {
            base.LoadRegistryInfo(regkey);
            imageFolder = (string)regkey.GetValue(strImageFolder, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }

        protected override void SaveRegistryInfo(RegistryKey regkey)
        {
            base.SaveRegistryInfo(regkey);
            regkey.SetValue(strImageFolder, imageFolder);
        }
    }
}
