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
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using Microsoft.Win32;
using System.Globalization;
using System.Threading;
using System.Drawing.Imaging;
using Net.SourceForge.Vietpad.InputMethod;
using VietOCR.NET.Utilities;
using Tesseract;

namespace VietOCR.NET
{
    public partial class GUI : GUIWithRegistry
    {
        public const string strProgName = "VietOCR.NET";
        public const string TO_BE_IMPLEMENTED = "To be implemented";

        protected string curLangCode;
        private string[] installedLanguageCodes;
        private string[] installedLanguages;

        public string[] InstalledLanguages
        {
            get { return installedLanguages; }
            set { installedLanguages = value; }
        }

        private Dictionary<string, string> lookupISO639;

        public Dictionary<string, string> LookupISO639
        {
            get { return lookupISO639; }
        }

        private Dictionary<string, string> lookupISO_3_1_Codes;

        public Dictionary<string, string> LookupISO_3_1_Codes
        {
            get { return lookupISO_3_1_Codes; }
        }

        protected int imageIndex;
        protected int imageTotal;
        protected IList<Image> imageList;
        protected string inputfilename;

        private Rectangle rect = Rectangle.Empty;
        private Rectangle box = Rectangle.Empty;

        protected float scaleX = 1f;
        protected float scaleY = 1f;

        protected string selectedUILanguage;
        protected string textFilename;
        protected bool textModified;

        const string strUILang = "UILanguage";
        const string strOcrLanguage = "OcrLanguage";
        const string strWordWrap = "WordWrap";
        const string strFontFace = "FontFace";
        const string strFontSize = "FontSize";
        const string strFontStyle = "FontStyle";
        const string strForeColor = "ForeColor";
        const string strBackColor = "BackColor";
        const string strSegmentedRegions = "SegmentedRegions";
        const string strSegmentedRegionsPara = "SegmentedRegionsPara";
        const string strSegmentedRegionsTextLine = "SegmentedRegionsTextLine";
        const string strSegmentedRegionsSymbol = "SegmentedRegionsSymbol";
        const string strSegmentedRegionsBlock = "SegmentedRegionsBlock";
        const string strSegmentedRegionsWord = "SegmentedRegionsWord";

        protected bool isFitImageSelected;
        protected Point curScrollPos;
        protected Point pointClicked;
        protected readonly string baseDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        protected System.CodeDom.Compiler.TempFileCollection tempFileCollection = new System.CodeDom.Compiler.TempFileCollection();

        public GUI()
        {
            // Access registry to determine which UI Language to be loaded.
            // The desired locale must be known before initializing visual components
            // with language text. Waiting until OnLoad would be too late.
            strRegKey += strProgName + "3";

            RegistryKey regkey = Registry.CurrentUser.OpenSubKey(strRegKey);

            if (regkey == null)
                regkey = Registry.CurrentUser.CreateSubKey(strRegKey);

            selectedUILanguage = (string)regkey.GetValue(strUILang, "en-US");
            regkey.Close();

            // Sets the UI culture to the selected language.
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedUILanguage);

            InitializeComponent();

            GetInstalledLanguagePacks();
            PopulateOCRLanguageBox();

            //rectNormal = DesktopBounds;

            //// Set system event.
            SystemEvents.SessionEnding += new SessionEndingEventHandler(OnSessionEnding);
        }

        // Event overrides
        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            // Work around a bug which causes Modified property
            // to be true when system default locale is vi-VN.
            this.textBox1.Modified = false;
        }


        protected override void OnClosing(CancelEventArgs cea)
        {
            base.OnClosing(cea);

            cea.Cancel = !OkToTrash();
        }

        // Event handlers
        void OnSessionEnding(object obj, SessionEndingEventArgs seea)
        {
            seea.Cancel = !OkToTrash();
        }

        /// <summary>
        /// Gets Tesseract's installed language data packs.
        /// </summary>
        void GetInstalledLanguagePacks()
        {
            lookupISO639 = new Dictionary<string, string>();
            lookupISO_3_1_Codes = new Dictionary<string, string>();

            try
            {
                string tessdataDir = Path.Combine(baseDir, "tessdata");
                installedLanguageCodes = Directory.GetFiles(tessdataDir, "*.traineddata");
                //installedLanguageCodes = installedLanguageCodes.Where(val => val != "osd.traineddata").ToArray(); // LINQ
                string xmlFilePath = Path.Combine(baseDir, "Data/ISO639-3.xml");
                Utilities.Utilities.LoadFromXML(lookupISO639, xmlFilePath);
                xmlFilePath = Path.Combine(baseDir, "Data/ISO639-1.xml");
                Utilities.Utilities.LoadFromXML(lookupISO_3_1_Codes, xmlFilePath);
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, strProgName);
                // this also applies to missing language data files in tessdata directory
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                if (installedLanguageCodes == null)
                {
                    installedLanguages = new String[0];
                }
                else
                {
                    installedLanguages = new String[installedLanguageCodes.Length];
                }

                for (int i = 0; i < installedLanguages.Length; i++)
                {
                    installedLanguageCodes[i] = Path.GetFileNameWithoutExtension(installedLanguageCodes[i]);
                    // translate ISO codes to full English names for user-friendliness
                    if (lookupISO639.ContainsKey(installedLanguageCodes[i]))
                    {
                        installedLanguages[i] = lookupISO639[installedLanguageCodes[i]];
                    }
                    else
                    {
                        installedLanguages[i] = installedLanguageCodes[i];
                    }
                }
            }
        }

        /// <summary>
        /// Populates OCR Language box.
        /// </summary>
        void PopulateOCRLanguageBox()
        {
            this.toolStripCbLang.Items.AddRange(installedLanguages);
        }

        protected virtual void ocrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void ocrAllPagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void toolStripButtonCancelOCR_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void postprocessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OwnedForms.Length > 0)
            {
                foreach (Form form in this.OwnedForms)
                {
                    HtmlHelpForm helpForm1 = form as HtmlHelpForm;
                    if (helpForm1 != null)
                    {
                        helpForm1.Show();
                        helpForm1.WindowState = FormWindowState.Normal;
                        return;
                    }
                }
            }
            HtmlHelpForm helpForm = new HtmlHelpForm(Properties.Resources.readme, ((ToolStripMenuItem)sender).Text.Replace("&", string.Empty));
            helpForm.Owner = this;
            helpForm.Show();
        }

        private void aboutToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string releaseDate = System.Configuration.ConfigurationManager.AppSettings["ReleaseDate"];
            string version = System.Configuration.ConfigurationManager.AppSettings["Version"];

            MessageBox.Show(this, strProgName + " " + version + " © 2008\n" +
                Properties.Resources.Program_desc + "\n" +
                DateTime.Parse(releaseDate).ToString("D", System.Threading.Thread.CurrentThread.CurrentUICulture).Normalize() + "\n" +
                "http://vietocr.sourceforge.net",
                ((ToolStripMenuItem)sender).Text.Replace("&", string.Empty), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected virtual void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        protected virtual void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        protected virtual void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }


        private void toolStripCbLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            curLangCode = installedLanguageCodes[this.toolStripCbLang.SelectedIndex];

            // Hide Viet Input Method submenu if selected OCR Language is not Vietnamese
            bool vie = curLangCode.Contains("vie");
            VietKeyHandler.VietModeEnabled = vie;
            this.vietInputMethodToolStripMenuItem.Visible = vie;
            this.toolStripMenuItemInputMethod.Visible = vie;

            if (this.toolStripButtonSpellCheck.Checked)
            {
                this.toolStripButtonSpellCheck.PerformClick();
                this.toolStripButtonSpellCheck.PerformClick();
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (!(e.ClickedItem is ToolStripButton)) return;

            ToolStripButton tsb = (ToolStripButton)e.ClickedItem;
            ToolStripMenuItem mi = (ToolStripMenuItem)tsb.Tag;
            if (mi != null)
                mi.PerformClick();
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (!(e.ClickedItem is ToolStripButton)) return;

            ToolStripButton tsb = (ToolStripButton)e.ClickedItem;
            ToolStripMenuItem mi = (ToolStripMenuItem)tsb.Tag;
            if (mi != null)
                mi.PerformClick();
        }

        private void toolStripBtnClear_Click(object sender, EventArgs e)
        {
            if (textFilename == null || OkToTrash())
            {
                //this.textBox1.Clear(); // cannot undo a clear action
                this.textBox1.SelectAll();
                this.textBox1.Cut();
                //this.textBox1.ClearUndo();
                textModified = false;
                this.textBox1.Modified = false;
                textFilename = null;
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OkToTrash())
            {
                Application.Exit();
            }
        }

        protected bool OkToTrash()
        {
            if (!textModified)
            {
                return true;
            }

            DialogResult dr =
                MessageBox.Show(Properties.Resources.Do_you_want_to_save_the_changes_to_ + FileTitle() + "?",
                strProgName,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Exclamation);
            switch (dr)
            {
                case DialogResult.Yes:
                    return saveAction();
                case DialogResult.No:
                    return true;
                case DialogResult.Cancel:
                    return false;
            }
            return false;
        }

        protected string FileTitle()
        {
            return (this.textFilename != null && this.textFilename.Length > 1) ?
                Path.GetFileName(this.textFilename) : Properties.Resources.Untitled;
        }

        protected virtual void toolStripBtnPrev_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }
        protected virtual void toolStripBtnNext_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }
        protected virtual void toolStripBtnFitImage_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }
        protected virtual void toolStripBtnActualSize_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }
        protected virtual void toolStripBtnZoomIn_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }
        protected virtual void toolStripBtnZoomOut_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }
        protected virtual void toolStripBtnRotateCCW_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }
        protected virtual void toolStripBtnRotateCW_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected void centerPicturebox()
        {
            this.splitContainerImage.Panel2.AutoScrollPosition = Point.Empty;
            int x = 0;
            int y = 0;

            if (this.pictureBox1.Width < this.splitContainerImage.Panel2.Width)
            {
                x = (this.splitContainerImage.Panel2.Width - this.pictureBox1.Width) / 2;
            }

            if (this.pictureBox1.Height < this.splitContainerImage.Panel2.Height)
            {
                y = (this.splitContainerImage.Panel2.Height - this.pictureBox1.Height) / 2;
            }

            this.pictureBox1.Location = new Point(x, y);
            this.pictureBox1.Invalidate();
        }

        protected virtual void scanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // To be implemented in subclass.
        }

        private void splitContainer2_Panel2_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if ((e.AllowedEffect & DragDropEffects.Move) != 0)
                    e.Effect = DragDropEffects.Move;

                if (((e.AllowedEffect & DragDropEffects.Copy) != 0) &&
                    ((e.KeyState & 0x08) != 0))    // Ctrl key
                    e.Effect = DragDropEffects.Copy;
            }
            //else if (e.Data.GetDataPresent(DataFormats.Bitmap))
            //{
            //    e.Effect = DragDropEffects.Copy;
            //}
        }

        private void splitContainer2_Panel2_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] astr = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (System.IO.File.Exists(astr[0]))
                {
                    openFile(astr[0]);
                }
            }
        }

        private void splitContainer2_Panel2_MouseWheel(object sender, MouseEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                if (e.Delta <= 0)
                {
                    // set minimum size to zoom
                    if (this.pictureBox1.Width < 100)
                        return;
                }
                else
                {
                    // set maximum size to zoom
                    if (this.pictureBox1.Width > 10000)
                        return;
                }

                this.pictureBox1.Deselect();
                this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                this.pictureBox1.Width += this.pictureBox1.Width * e.Delta / 1000;
                this.pictureBox1.Height += this.pictureBox1.Height * e.Delta / 1000;
                scaleX = (float)this.pictureBox1.Image.Width / (float)this.pictureBox1.Width;
                scaleY = (float)this.pictureBox1.Image.Height / (float)this.pictureBox1.Height;
                this.centerPicturebox();
                isFitImageSelected = false;
                this.toolStripBtnActualSize.Enabled = true;
            }
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            if (!this.textBox1.Focused && this.ContainsFocus)
            {
                textBox1.HideSelection = false;

                bool isTextSelected = false;
                if (this.textBox1.SelectionLength > 0)
                {
                    isTextSelected = true;
                }

                this.textBox1.Focus();

                if (!isTextSelected)
                {
                    this.textBox1.SelectionLength = 0;
                }
            }
        }

        /// <summary>
        /// Changes localized text and messages
        /// </summary>
        /// <param name="locale"></param>
        /// <param name="firstTime"></param>
        protected virtual void ChangeUILanguage(string locale)
        {
            string imageProperties = this.toolStripStatusLabelDimValue.Text; // retain values of image properties in statusbar
            string totalPage = this.toolStripLabelPageNum.Text;

            FormLocalizer localizer = new FormLocalizer(this, typeof(GUI));
            localizer.ApplyCulture(new CultureInfo(locale));

            this.toolStripStatusLabel1.Text = null;

            foreach (Form form in this.OwnedForms)
            {
                HtmlHelpForm helpForm = form as HtmlHelpForm;
                if (helpForm != null)
                {
                    helpForm.Text = Properties.Resources.VietOCR_Help;
                }
            }

            this.contextMenuStrip1.ChangeUILanguage();

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.toolTip1.SetToolTip(this.buttonCollapseExpand, resources.GetString("buttonCollapseExpand.ToolTipText"));
            this.toolStripStatusLabelDimValue.Text = imageProperties; // restore prior values
            this.toolStripLabelPageNum.Text = totalPage;
        }

        protected override void LoadRegistryInfo(RegistryKey regkey)
        {
            base.LoadRegistryInfo(regkey);

            this.toolStripCbLang.Text = (string)regkey.GetValue(strOcrLanguage, null);
            if (curLangCode == null)
            {
                curLangCode = this.toolStripCbLang.Text;
            }
            this.textBox1.WordWrap = Convert.ToBoolean(
                (int)regkey.GetValue(strWordWrap, Convert.ToInt32(true)));
            this.textBox1.Font = new Font((string)regkey.GetValue(strFontFace, "Microsoft Sans Serif"),
                float.Parse((string)regkey.GetValue(strFontSize, "10")),
                (FontStyle)regkey.GetValue(strFontStyle, FontStyle.Regular));
            this.textBox1.ForeColor = Color.FromArgb(
                (int)regkey.GetValue(strForeColor, Color.FromKnownColor(KnownColor.Black).ToArgb()));
            this.textBox1.BackColor = Color.FromArgb(
                (int)regkey.GetValue(strBackColor, Color.FromKnownColor(KnownColor.White).ToArgb()));

            selectedUILanguage = (string)regkey.GetValue(strUILang, "en-US");


            this.segmentedRegionsToolStripMenuItem.Checked = Convert.ToBoolean((int)regkey.GetValue(strSegmentedRegions, Convert.ToInt32(false)));
            this.toolStripDropDownButtonSegmentedRegions.Visible = this.segmentedRegionsToolStripMenuItem.Checked;
            this.toolStripMenuItemPara.Checked = Convert.ToBoolean((int)regkey.GetValue(strSegmentedRegionsPara, Convert.ToInt32(false)));
            this.toolStripMenuItemTextLine.Checked = Convert.ToBoolean((int)regkey.GetValue(strSegmentedRegionsTextLine, Convert.ToInt32(false)));
            this.toolStripMenuItemSymbol.Checked = Convert.ToBoolean((int)regkey.GetValue(strSegmentedRegionsSymbol, Convert.ToInt32(false)));
            this.toolStripMenuItemBlock.Checked = Convert.ToBoolean((int)regkey.GetValue(strSegmentedRegionsBlock, Convert.ToInt32(false)));
            this.toolStripMenuItemWord.Checked = Convert.ToBoolean((int)regkey.GetValue(strSegmentedRegionsWord, Convert.ToInt32(false)));
        }

        protected override void SaveRegistryInfo(RegistryKey regkey)
        {
            base.SaveRegistryInfo(regkey);

            regkey.SetValue(strOcrLanguage, this.toolStripCbLang.Text);

            regkey.SetValue(strWordWrap, Convert.ToInt32(this.textBox1.WordWrap));
            regkey.SetValue(strFontFace, this.textBox1.Font.Name);
            regkey.SetValue(strFontSize, this.textBox1.Font.SizeInPoints.ToString());
            regkey.SetValue(strFontStyle, (int)this.textBox1.Font.Style);
            regkey.SetValue(strForeColor, this.textBox1.ForeColor.ToArgb());
            regkey.SetValue(strBackColor, this.textBox1.BackColor.ToArgb());
            regkey.SetValue(strUILang, selectedUILanguage);
            regkey.SetValue(strSegmentedRegions, Convert.ToInt32(this.segmentedRegionsToolStripMenuItem.Checked));
            regkey.SetValue(strSegmentedRegionsPara, Convert.ToInt32(this.toolStripMenuItemPara.Checked));
            regkey.SetValue(strSegmentedRegionsTextLine, Convert.ToInt32(this.toolStripMenuItemTextLine.Checked));
            regkey.SetValue(strSegmentedRegionsSymbol, Convert.ToInt32(this.toolStripMenuItemSymbol.Checked));
            regkey.SetValue(strSegmentedRegionsBlock, Convert.ToInt32(this.toolStripMenuItemBlock.Checked));
            regkey.SetValue(strSegmentedRegionsWord, Convert.ToInt32(this.toolStripMenuItemWord.Checked));
        }

        private void formatToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            this.wordWrapToolStripMenuItem.Checked = this.textBox1.WordWrap;
        }

        protected virtual void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void changeCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void removeLineBreaksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void mergeTiffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void splitTiffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void splitPdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void mergePdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                PasteImage();
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }

        void PasteImage()
        {
            Image image = ImageHelper.GetClipboardImage();
            if (image != null)
            {
                string tempFileName = Path.GetTempFileName();
                File.Delete(tempFileName);
                tempFileName = Path.ChangeExtension(tempFileName, ".png");
                tempFileCollection.AddFile(tempFileName, false);
                image.Save(tempFileName, System.Drawing.Imaging.ImageFormat.Png);
                openFile(tempFileName);
            }
        }

        protected virtual void openFile(string fileName)
        {

        }

        protected virtual bool saveAction()
        {
            return true;
        }

        protected override void OnResize(EventArgs e)
        {
            bool isCentered = this.splitContainerImage.Panel2.AutoScrollPosition == Point.Empty;
            base.OnResize(e);
            if (this.pictureBox1.Image != null)
            {
                this.pictureBox1.Deselect();
                if (this.isFitImageSelected)
                {
                    int w = this.splitContainerImage.Panel2.Width;
                    int h = this.splitContainerImage.Panel2.Height;
                    Size fitSize = fitImagetoContainer(this.pictureBox1.Image.Width, this.pictureBox1.Image.Height, w, h);
                    this.pictureBox1.Width = fitSize.Width;
                    this.pictureBox1.Height = fitSize.Height;
                    setScale();
                    this.centerPicturebox();
                }
                else
                {
                    scaleX = (float)this.pictureBox1.Image.Width / (float)this.pictureBox1.Width;
                    scaleY = (float)this.pictureBox1.Image.Height / (float)this.pictureBox1.Height;
                }
                if (isCentered)
                {
                    this.centerPicturebox();
                }
            }
        }

        protected void setScale()
        {
            scaleX = (float)this.pictureBox1.Image.Width / (float)this.pictureBox1.Width;
            scaleY = (float)this.pictureBox1.Image.Height / (float)this.pictureBox1.Height;
            if (scaleX > scaleY)
            {
                scaleY = scaleX;
            }
            else
            {
                scaleX = scaleY;
            }
        }

        protected Size fitImagetoContainer(int w, int h, int maxWidth, int maxHeight)
        {
            float ratio = (float)w / h;

            w = maxWidth;
            h = (int)Math.Floor(maxWidth / ratio);

            if (h > maxHeight)
            {
                h = maxHeight;
                w = (int)Math.Floor(maxHeight * ratio);
            }

            return new Size(w, h);
        }

        private void textBox1_ModifiedChanged(object sender, EventArgs e)
        {
            if (textModified && !this.textBox1.Modified)
            {
                this.textBox1.Modified = textModified;
            }
            textModified = this.textBox1.Modified;
            this.toolStripBtnSave.Enabled = this.textBox1.Modified;
            this.saveToolStripMenuItem.Enabled = this.textBox1.Modified;
        }

        protected virtual void loadThumbnails()
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void metadataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void screenshotModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void toolStripButtonSpellCheck_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                pointClicked = e.Location;
            }
        }

        protected virtual void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            // To be implemented in subclass.
        }

        private void GUI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                this.toolStripButtonSpellCheck.PerformClick();
            }
            else if (e.Control && e.Shift && (e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.Add))
            {
                this.toolStripBtnRotateCW.PerformClick();
            }
            else if (e.Control && e.Shift && (e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Subtract))
            {
                this.toolStripBtnRotateCCW.PerformClick();
            }
            else if (e.Control && (e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.Add))
            {
                this.toolStripBtnZoomIn.PerformClick();
            }
            else if (e.Control && (e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Subtract))
            {
                this.toolStripBtnZoomOut.PerformClick();
            }
            else if (e.Control && (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1))
            {
                this.toolStripBtnActualSize.PerformClick();
            }
            else if (e.Control && (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2))
            {
                this.toolStripBtnFitImage.PerformClick();
            }
        }

        /// <summary>
        /// This method is for Left and Right arrows used in navigating the image pages.
        /// The GUI_KeyDown method seems to skip these keys on first key entries.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!this.textBox1.Focused && keyData == (Keys.Control | Keys.Left))
            {
                this.toolStripBtnPrev.PerformClick();
            }
            else if (!this.textBox1.Focused && keyData == (Keys.Control | Keys.Right))
            {
                this.toolStripBtnNext.PerformClick();
            }

            if (this.splitContainerImage.Focused)
            {
                if (keyData == Keys.Left)
                {
                    curScrollPos = this.splitContainerImage.Panel2.AutoScrollPosition;
                    this.splitContainerImage.Panel2.AutoScrollPosition = new Point(Math.Abs(curScrollPos.X) - 10, Math.Abs(curScrollPos.Y));
                }
                else if (keyData == Keys.Right)
                {
                    curScrollPos = this.splitContainerImage.Panel2.AutoScrollPosition;
                    this.splitContainerImage.Panel2.AutoScrollPosition = new Point(Math.Abs(curScrollPos.X) + 10, Math.Abs(curScrollPos.Y));
                }
                else if (keyData == Keys.Up)
                {
                    curScrollPos = this.splitContainerImage.Panel2.AutoScrollPosition;
                    this.splitContainerImage.Panel2.AutoScrollPosition = new Point(Math.Abs(curScrollPos.X), Math.Abs(curScrollPos.Y) - 10);
                }
                else if (keyData == Keys.Down)
                {
                    curScrollPos = this.splitContainerImage.Panel2.AutoScrollPosition;
                    this.splitContainerImage.Panel2.AutoScrollPosition = new Point(Math.Abs(curScrollPos.X), Math.Abs(curScrollPos.Y) + 10);
                }
                else if (keyData == (Keys.Control | Keys.Home))
                {
                    this.splitContainerImage.Panel2.AutoScrollPosition = Point.Empty;
                }
                else if (keyData == (Keys.Control | Keys.End))
                {
                    this.splitContainerImage.Panel2.AutoScrollPosition = new Point(Math.Abs(this.splitContainerImage.Panel2.HorizontalScroll.Maximum), Math.Abs(this.splitContainerImage.Panel2.VerticalScroll.Maximum));
                }
                else if (keyData == Keys.PageUp)
                {
                    curScrollPos = this.splitContainerImage.Panel2.AutoScrollPosition;
                    this.splitContainerImage.Panel2.AutoScrollPosition = new Point(Math.Abs(curScrollPos.X), Math.Abs(curScrollPos.Y) - this.splitContainerImage.Panel2.VerticalScroll.LargeChange);
                }
                else if (keyData == Keys.PageDown)
                {
                    curScrollPos = this.splitContainerImage.Panel2.AutoScrollPosition;
                    this.splitContainerImage.Panel2.AutoScrollPosition = new Point(Math.Abs(curScrollPos.X), Math.Abs(curScrollPos.Y) + this.splitContainerImage.Panel2.VerticalScroll.LargeChange);
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected virtual void downloadLangDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void deskewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        private void toolStripCbLang_TextUpdate(object sender, EventArgs e)
        {
            curLangCode = this.toolStripCbLang.Text;

            // Hide Viet Input Method submenu if selected OCR Language is not Vietnamese
            bool vie = curLangCode.Contains("vie");
            VietKeyHandler.VietModeEnabled = vie;
            this.vietInputMethodToolStripMenuItem.Visible = vie;
            this.toolStripMenuItemInputMethod.Visible = vie;
        }

        protected virtual void bulkOCRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void brightenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void autocropToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void contrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void monochromeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void sharpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void smoothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        protected virtual void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if (!this.pictureBox1.Focused && this.FindForm().ContainsFocus)
            {
                this.pictureBox1.Focus();
            }
        }

        private void buttonCollapseExpand_Click(object sender, EventArgs e)
        {
            this.buttonCollapseExpand.Text = this.buttonCollapseExpand.Text == "»" ? "«" : "»";
            this.splitContainerImage.Panel1Collapsed ^= true;
        }

        protected virtual void splitContainerImage_SplitterMoved(object sender, SplitterEventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        private void toolStripButtonPasteImage_Click(object sender, EventArgs e)
        {
            PasteImage();
        }

        private void GUI_Activated(object sender, EventArgs e)
        {
            this.toolStripButtonPasteImage.Enabled = ImageHelper.GetClipboardImage() != null;
        }

        protected virtual void toolStripComboBoxPageNum_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected virtual void removeLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }

        private void segmentedRegionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.toolStripDropDownButtonSegmentedRegions.Visible = this.segmentedRegionsToolStripMenuItem.Checked;
            setSegmentedRegions();
        }


        private void toolStripMenuItemSymbol_Click(object sender, EventArgs e)
        {
            setSegmentedRegions();
        }

        private void toolStripMenuItemWord_Click(object sender, EventArgs e)
        {
            setSegmentedRegions();
        }

        private void toolStripMenuItemTextLine_Click(object sender, EventArgs e)
        {
            setSegmentedRegions();
        }

        private void toolStripMenuItemPara_Click(object sender, EventArgs e)
        {
            setSegmentedRegions();
        }

        private void toolStripMenuItemBlock_Click(object sender, EventArgs e)
        {
            setSegmentedRegions();
        }

        protected void setSegmentedRegions()
        {
            if (!this.segmentedRegionsToolStripMenuItem.Checked || imageList == null || this.toolStripBtnActualSize.Enabled)
            {
                pictureBox1.SegmentedRegions = null;
                pictureBox1.Refresh();
                return;
            }

            OCR<Image> ocrEngine = new OCRImages();
            Dictionary<Color, List<Rectangle>> map = pictureBox1.SegmentedRegions;
            if (map == null)
            {
                map = new Dictionary<Color, List<Rectangle>>();
            }

            Bitmap image = (Bitmap)imageList[imageIndex];

            List<Rectangle> regions = new List<Rectangle>();

            if (toolStripMenuItemBlock.Checked)
            {
                if (!map.ContainsKey(Color.Gray))
                {
                    regions = ocrEngine.GetSegmentedRegions(image, PageIteratorLevel.Block);
                    map.Add(Color.Gray, regions);
                }
            }
            else
            {
                map.Remove(Color.Gray);
            }

            if (toolStripMenuItemPara.Checked)
            {
                if (!map.ContainsKey(Color.Green))
                {
                    regions = ocrEngine.GetSegmentedRegions(image, PageIteratorLevel.Para);
                    map.Add(Color.Green, regions);
                }
            }
            else
            {
                map.Remove(Color.Green);
            }

            if (toolStripMenuItemTextLine.Checked)
            {
                if (!map.ContainsKey(Color.Red))
                {
                    regions = ocrEngine.GetSegmentedRegions(image, PageIteratorLevel.TextLine);
                    map.Add(Color.Red, regions);
                }
            }
            else
            {
                map.Remove(Color.Red);
            }

            if (toolStripMenuItemWord.Checked)
            {
                if (!map.ContainsKey(Color.Blue))
                {
                    regions = ocrEngine.GetSegmentedRegions(image, PageIteratorLevel.Word);
                    map.Add(Color.Blue, regions);
                }
            }
            else
            {
                map.Remove(Color.Blue);
            }

            if (toolStripMenuItemSymbol.Checked)
            {
                if (!map.ContainsKey(Color.Magenta))
                {
                    regions = ocrEngine.GetSegmentedRegions(image, PageIteratorLevel.Symbol);
                    map.Add(Color.Magenta, regions);
                }
            }
            else
            {
                map.Remove(Color.Magenta);
            }

            pictureBox1.SegmentedRegions = map;
            pictureBox1.Refresh();
            //pictureBox1.Update();
        }

        protected virtual void cropToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TO_BE_IMPLEMENTED, strProgName);
        }
    }
}