using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using VietOCR.NET.Utilities;

namespace VietOCR.NET
{
    public partial class OptionsDialog : Form
    {
        private string watchFolder;

        public string WatchFolder
        {
            get { return watchFolder; }
            set { watchFolder = value; }
        }
        private string outputFolder;

        public string OutputFolder
        {
            get { return outputFolder; }
            set { outputFolder = value; }
        }
        private bool watchEnabled;

        public bool WatchEnabled
        {
            get { return watchEnabled; }
            set { watchEnabled = value; }
        }

        private string dangAmbigsPath;

        public string DangAmbigsPath
        {
            get { return dangAmbigsPath; }
            set { dangAmbigsPath = value; }
        }

        private string curLangCode;

        public string CurLangCode
        {
            get { return curLangCode; }
            set { curLangCode = value; }
        }

        private bool dangAmbigsEnabled;

        public bool DangAmbigsEnabled
        {
            get { return dangAmbigsEnabled; }
            set { dangAmbigsEnabled = value; }
        }

        public string OutputFormat
        {
            get { return this.comboBoxOutputFormat.SelectedItem.ToString(); }
            set 
            {
                this.comboBoxOutputFormat.SelectedItem = value;
                if (this.comboBoxOutputFormat.SelectedIndex == -1) this.comboBoxOutputFormat.SelectedIndex = 0;
            }
        }

        public OptionsDialog()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            this.textBoxWatch.Text = watchFolder;
            this.textBoxOutput.Text = outputFolder;
            this.checkBoxWatch.Checked = watchEnabled;
            this.textBoxDangAmbigs.Text = dangAmbigsPath;
            this.checkBoxDangAmbigs.Checked = dangAmbigsEnabled;

            this.toolTip1.SetToolTip(this.btnWatch, Properties.Resources.Browse);
            this.toolTip1.SetToolTip(this.btnOutput, Properties.Resources.Browse);
            this.toolTip1.SetToolTip(this.btnDangAmbigs, Properties.Resources.Browse);
        }

        protected override void OnClosed(EventArgs ea)
        {
            base.OnClosed(ea);

            watchFolder = this.textBoxWatch.Text;
            outputFolder = this.textBoxOutput.Text;
            watchEnabled = this.checkBoxWatch.Checked;
            dangAmbigsPath = this.textBoxDangAmbigs.Text;
            dangAmbigsEnabled = this.checkBoxDangAmbigs.Checked;
        }

        private void btnWatch_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.Description = "Set Watch Folder.";
            this.folderBrowserDialog1.SelectedPath = watchFolder;

            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                watchFolder = this.folderBrowserDialog1.SelectedPath;
                this.textBoxWatch.Text = watchFolder;
            }
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.Description = "Set Output Folder.";
            this.folderBrowserDialog1.SelectedPath = outputFolder;

            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                outputFolder = this.folderBrowserDialog1.SelectedPath;
                this.textBoxOutput.Text = outputFolder;
            }
        }

        private void btnDangAmbigs_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Title = String.Format("Set Path to {0}.DangAmbigs.txt", curLangCode);
            this.openFileDialog1.InitialDirectory = dangAmbigsPath;

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dangAmbigsPath = System.IO.Path.GetDirectoryName(this.openFileDialog1.FileName);
                this.textBoxDangAmbigs.Text = dangAmbigsPath;
            }
        }

        /// <summary>
        /// Changes localized text and messages
        /// </summary>
        /// <param name="locale"></param>
        public virtual void ChangeUILanguage(string locale)
        {
            FormLocalizer localizer = new FormLocalizer(this, typeof(OptionsDialog));
            localizer.ApplyCulture(new CultureInfo(locale));
        }

        private void comboBoxOutputFormat_MouseHover(object sender, EventArgs e)
        {
            string val = this.comboBoxOutputFormat.SelectedItem.ToString();
            switch (val)
            {
                case "text+":
                    val = "Text with postprocessing";
                    break;
                case "text":
                    val = "Text with no postprocessing";
                    break;
                case "pdf":
                    val = "PDF";
                    break;
                case "hocr":
                    val = "hOCR";
                    break;
                default:
                    val = null;
                    break;
            }

            this.toolTip1.SetToolTip(this.comboBoxOutputFormat, val);
        }
    }
}
