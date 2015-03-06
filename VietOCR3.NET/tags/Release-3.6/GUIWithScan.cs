using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using VietOCR.NET.WIA;

namespace VietOCR.NET
{
    public partial class GUIWithScan : VietOCR.NET.GUIWithThumbnail
    {
        public GUIWithScan()
        {
            InitializeComponent();
        }

        protected override void scanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scaleX = scaleY = 1f;
            performScan();
        }

        /// <summary>
        /// Access scanner and scan documents via WIA.
        /// </summary>
        void performScan()
        {
            try
            {
                this.toolStripStatusLabel1.Text = Properties.Resources.Scanning;
                this.Cursor = Cursors.WaitCursor;
                this.pictureBox1.UseWaitCursor = true;
                this.textBox1.Cursor = Cursors.WaitCursor;
                this.toolStripBtnScan.Enabled = false;
                this.scanToolStripMenuItem.Enabled = false;
                this.toolStripProgressBar1.Enabled = true;
                this.toolStripProgressBar1.Visible = true;
                this.toolStripProgressBar1.Style = ProgressBarStyle.Marquee;

                // Start the asynchronous operation.
                backgroundWorkerScan.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void backgroundWorkerScan_DoWork(object sender, DoWorkEventArgs e)
        {
            using (WiaScannerAdapter adapter = new WiaScannerAdapter())
            {
                try
                {
                    string tempFileName = Path.GetTempFileName();
                    File.Delete(tempFileName);
                    tempFileName = Path.ChangeExtension(tempFileName, ".png");
                    tempFileCollection.AddFile(tempFileName, false);
                    FileInfo imageFile = new FileInfo(tempFileName);
                    if (imageFile.Exists)
                    {
                        imageFile.Delete();
                    }
                    adapter.ScanImage(ImageFormat.Png, imageFile.FullName);
                    e.Result = tempFileName;
                }
                catch (WiaOperationException ex)
                {
                    throw new Exception(System.Text.RegularExpressions.Regex.Replace(ex.ErrorCode.ToString(), "(?=\\p{Lu}+)", " ").Trim() + ".");
                }
            }
        }

        private void backgroundWorkerScan_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.toolStripProgressBar1.Enabled = false;
            this.toolStripProgressBar1.Visible = false;

            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                this.toolStripStatusLabel1.Text = String.Empty;
                MessageBox.Show(e.Error.Message, Properties.Resources.ScanningOperation, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled the operation.
                // Note that due to a race condition in the DoWork event handler, the Cancelled
                // flag may not have been set, even though CancelAsync was called.
                this.toolStripStatusLabel1.Text = "Scanning " + Properties.Resources.canceled;
            }
            else
            {
                // Finally, handle the case where the operation succeeded.
                openFile(e.Result.ToString());
                this.toolStripStatusLabel1.Text = Properties.Resources.Scancompleted;
            }

            this.Cursor = Cursors.Default;
            this.pictureBox1.UseWaitCursor = false;
            this.textBox1.Cursor = Cursors.Default;
            this.toolStripBtnScan.Enabled = true;
            this.scanToolStripMenuItem.Enabled = true;
        }
    }
}
