/**
 * Copyright @ 2016 Quan Nguyen
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
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Tesseract;
using VietOCR.NET.Utilities;

namespace VietOCR.NET
{
    public partial class GUIWithFile : VietOCR.NET.GUI
    {
        private int filterIndex;

        List<string> mruList = new List<string>();
        private string strClearRecentFiles;

        const string strFilterIndex = "FilterIndex";
        const string strMruList = "MruList";

        public GUIWithFile()
        {
            InitializeComponent();
        }

        // Event overrides
        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            updateMRUMenu();
        }
 
        protected override void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Title = Properties.Resources.OpenImageFile;
            openFileDialog1.Filter = "All Image Files|*.bmp;*.gif;*.jpg;*.jpeg;*.png;*.tif;*.tiff;*.pdf|Image Files (*.bmp)|*.bmp|Image Files (*.gif)|*.gif|Image Files (*.jpg;*.jpeg)|*.jpg;*.jpeg|Image Files (*.png)|*.png|Image Files (*.tif;*.tiff)|*.tif;*.tiff|PDF Files (*.pdf)|*.pdf|UTF-8 Text Files (*.txt)|*.txt";
            openFileDialog1.FilterIndex = filterIndex;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                openFile(openFileDialog1.FileName);
                filterIndex = openFileDialog1.FilterIndex;
            }
        }

        protected override void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAction();
        }

        protected void setButton()
        {
            if (imageIndex == 0)
            {
                this.toolStripBtnPrev.Enabled = false;
            }
            else
            {
                this.toolStripBtnPrev.Enabled = true;
            }

            if (imageIndex == imageList.Count - 1)
            {
                this.toolStripBtnNext.Enabled = false;
            }
            else
            {
                this.toolStripBtnNext.Enabled = true;
            }
        }

        protected override bool saveAction()
        {
            if (textFilename == null || textFilename.Length == 0)
            {
                return SaveFileDlg();
            }
            else
            {
                return SaveTextFile();
            }
        }

        protected override void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDlg();
        }

        bool SaveFileDlg()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = Properties.Resources.Save_As;
            saveFileDialog1.Filter = "UTF-8 Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            if (textFilename != null && textFilename.Length > 1)
            {
                saveFileDialog1.InitialDirectory = Path.GetDirectoryName(textFilename);
                saveFileDialog1.FileName = Path.GetFileName(textFilename);
            }

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textFilename = saveFileDialog1.FileName;
                return SaveTextFile();
            }
            else
            {
                return false;
            }
        }

        bool SaveTextFile()
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                using (StreamWriter sw = new StreamWriter(textFilename, false, new System.Text.UTF8Encoding()))
                {
                    sw.Write(this.textBox1.Text);
                    updateMRUList(textFilename);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, strProgName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            textModified = false;
            this.textBox1.Modified = false;
            this.Cursor = Cursors.Default;

            return true;
        }

        /// <summary>
        /// Opens image or text file.
        /// </summary>
        /// <param name="selectedFile"></param>
        protected override void openFile(string selectedFile)
        {
            // if text file, load it into textbox
            if (selectedFile.EndsWith(".txt"))
            {
                if (!OkToTrash())
                    return;

                try
                {
                    using (StreamReader sr = new StreamReader(selectedFile, Encoding.UTF8, true))
                    {
                        textModified = false;
                        this.textBox1.Text = sr.ReadToEnd();
                        updateMRUList(selectedFile);
                        textFilename = selectedFile;
                        this.textBox1.Modified = false;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(this, e.Message, strProgName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            this.toolStripStatusLabel1.Text = Properties.Resources.Loading_image;
            this.Cursor = Cursors.WaitCursor;
            this.pictureBox1.UseWaitCursor = true;
            this.textBox1.Cursor = Cursors.WaitCursor;
            this.toolStripBtnOCR.Enabled = false;
            this.oCRToolStripMenuItem.Enabled = false;
            this.oCRAllPagesToolStripMenuItem.Enabled = false;
            this.toolStripProgressBar1.Enabled = true;
            this.toolStripProgressBar1.Visible = true;
            this.toolStripProgressBar1.Style = ProgressBarStyle.Marquee;

            this.backgroundWorkerLoad.RunWorkerAsync(selectedFile);
            updateMRUList(selectedFile);
        }

        void loadImage(FileInfo imageFile)
        {
            if (imageList == null)
            {
                MessageBox.Show(this, Properties.Resources.Cannotloadimage, strProgName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            imageTotal = imageList.Count;
            imageIndex = 0;

            this.pictureBox1.Dock = DockStyle.None;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            scaleX = scaleY = 1f;
            isFitImageSelected = false;

            this.toolStripComboBoxPageNum.Enabled = true;
            this.toolStripComboBoxPageNum.Items.Clear();
            for (int i = 0; i < imageTotal; i++)
            {
                this.toolStripComboBoxPageNum.Items.Add(i + 1);
            }
            this.toolStripComboBoxPageNum.Enabled = false;
            this.toolStripComboBoxPageNum.SelectedIndex = 0;
            this.toolStripComboBoxPageNum.Enabled = true;

            this.toolStripLabelPageNum.Text = " / " + imageTotal.ToString();
            this.toolStripLabelPageNum.Enabled = true;

            displayImage();
            loadThumbnails();

            // clear undo buffer
            clearStack();

            this.Text = imageFile.Name + " - " + strProgName;
            //this.toolStripStatusLabel1.Text = null;
            this.pictureBox1.Deselect();

            this.toolStripBtnFitImage.Enabled = true;
            this.toolStripBtnActualSize.Enabled = false;
            this.toolStripBtnZoomIn.Enabled = true;
            this.toolStripBtnZoomOut.Enabled = true;
            this.toolStripBtnRotateCCW.Enabled = true;
            this.toolStripBtnRotateCW.Enabled = true;

            if (imageList.Count == 1)
            {
                this.toolStripBtnNext.Enabled = false;
                this.toolStripBtnPrev.Enabled = false;
            }
            else
            {
                this.toolStripBtnNext.Enabled = true;
                this.toolStripBtnPrev.Enabled = true;
            }

            setButton();
        }

        protected virtual void clearStack()
        {
            // to be implemented in subclass
        }

        protected void displayImage()
        {
            Image currentImage = imageList[imageIndex];
            this.pictureBox1.Image = new Bitmap(currentImage);
            this.pictureBox1.Size = this.pictureBox1.Image.Size;
            this.toolStripStatusLabelDimValue.Text = string.Format("{0} × {1}px  {2}bpp", currentImage.Width, currentImage.Height, Bitmap.GetPixelFormatSize(currentImage.PixelFormat).ToString());

            if (this.isFitImageSelected)
            {
                Size fitSize = fitImagetoContainer(this.pictureBox1.Image.Width, this.pictureBox1.Image.Height, this.splitContainerImage.Panel2.Width, this.splitContainerImage.Panel2.Height);
                this.pictureBox1.Width = fitSize.Width;
                this.pictureBox1.Height = fitSize.Height;
                setScale();
            }
            else if (this.scaleX != 1f)
            {
                this.pictureBox1.Width = Convert.ToInt32(this.pictureBox1.Width / scaleX);
                this.pictureBox1.Height = Convert.ToInt32(this.pictureBox1.Height / scaleY);
            }
            curScrollPos = Point.Empty;
            this.centerPicturebox();

            this.pictureBox1.Deselect();
            this.pictureBox1.SegmentedRegions = null;
            setSegmentedRegions();
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void backgroundWorkerLoad_DoWork(object sender, DoWorkEventArgs e)
        {
            inputfilename = (string)e.Argument;
            FileInfo imageFile = new FileInfo(inputfilename);
            imageList = ImageIOHelper.GetImageList(imageFile);
            e.Result = imageFile;
        }

        private void backgroundWorkerLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.toolStripProgressBar1.Enabled = false;
            this.toolStripProgressBar1.Visible = false;

            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                this.toolStripStatusLabel1.Text = String.Empty;
                MessageBox.Show(e.Error.Message, Properties.Resources.Load_image, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled the operation.
                // Note that due to a race condition in the DoWork event handler, the Cancelled
                // flag may not have been set, even though CancelAsync was called.
                this.toolStripStatusLabel1.Text = "Image loading " + Properties.Resources.canceled;
            }
            else
            {
                // Finally, handle the case where the operation succeeded.
                loadImage((FileInfo)e.Result);
                this.toolStripStatusLabel1.Text = Properties.Resources.Loading_completed;
            }

            this.Cursor = Cursors.Default;
            this.pictureBox1.UseWaitCursor = false;
            this.textBox1.Cursor = Cursors.Default;
            this.toolStripBtnOCR.Enabled = true;
            this.oCRToolStripMenuItem.Enabled = true;
            this.oCRAllPagesToolStripMenuItem.Enabled = true;
        }

        /// <summary>
        /// Update MRU List.
        /// </summary>
        /// <param name="fileName"></param>
        private void updateMRUList(String fileName)
        {
            if (mruList.Contains(fileName))
            {
                mruList.Remove(fileName);
            }

            mruList.Insert(0, fileName);

            if (mruList.Count > 10)
            {
                mruList.RemoveAt(10);
            }

            updateMRUMenu();
        }

        /// <summary>
        /// Update MRU Submenu.
        /// </summary>
        private void updateMRUMenu()
        {
            this.recentFilesToolStripMenuItem.DropDownItems.Clear();

            if (mruList.Count == 0)
            {
                this.recentFilesToolStripMenuItem.DropDownItems.Add(Properties.Resources.No_Recent_Files);
            }
            else
            {
                EventHandler eh = new EventHandler(MenuRecentFilesOnClick);

                foreach (string fileName in mruList)
                {
                    ToolStripItem item = this.recentFilesToolStripMenuItem.DropDownItems.Add(fileName);
                    item.Click += eh;
                }
                this.recentFilesToolStripMenuItem.DropDownItems.Add("-");
                strClearRecentFiles = Properties.Resources.Clear_Recent_Files;
                ToolStripItem clearItem = this.recentFilesToolStripMenuItem.DropDownItems.Add(strClearRecentFiles);
                clearItem.Click += eh;
            }
        }

        void MenuRecentFilesOnClick(object obj, EventArgs ea)
        {
            ToolStripDropDownItem item = (ToolStripDropDownItem)obj;
            string fileName = item.Text;

            if (fileName == strClearRecentFiles)
            {
                mruList.Clear();
                this.recentFilesToolStripMenuItem.DropDownItems.Clear();
                this.recentFilesToolStripMenuItem.DropDownItems.Add(Properties.Resources.No_Recent_Files);
            }
            else
            {
                openFile(fileName);
            }
        }

        protected override void toolStripComboBoxPageNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.toolStripComboBoxPageNum.Enabled) return;

            int pageNum = Int32.Parse(this.toolStripComboBoxPageNum.SelectedItem.ToString());
            this.pictureBox1.Deselect();
            imageIndex = pageNum - 1;
            this.toolStripStatusLabel1.Text = null;
            displayImage();
            clearStack();

            // recalculate scale factors if in Fit Image mode
            if (this.pictureBox1.SizeMode == PictureBoxSizeMode.Zoom)
            {
                scaleX = (float)this.pictureBox1.Image.Width / (float)this.pictureBox1.Width;
                scaleY = (float)this.pictureBox1.Image.Height / (float)this.pictureBox1.Height;
            }

            setButton();
        }

        protected override void LoadRegistryInfo(RegistryKey regkey)
        {
            base.LoadRegistryInfo(regkey);
            filterIndex = (int)regkey.GetValue(strFilterIndex, 1);

            string[] fileNames = ((string)regkey.GetValue(strMruList, String.Empty)).Split(new[] { Path.PathSeparator }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string fileName in fileNames)
            {
                mruList.Add(fileName);
            }
        }

        protected override void SaveRegistryInfo(RegistryKey regkey)
        {
            base.SaveRegistryInfo(regkey);
            regkey.SetValue(strFilterIndex, filterIndex);
            StringBuilder strB = new StringBuilder();
            foreach (string name in mruList)
            {
                strB.Append(name).Append(';');
            }
            regkey.SetValue(strMruList, strB.ToString());
        }
    }
}
