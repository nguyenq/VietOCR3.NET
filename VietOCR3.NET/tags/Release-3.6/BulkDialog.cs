using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VietOCR.NET.Utilities;
using System.Globalization;

namespace VietOCR.NET
{
    public partial class BulkDialog : Form
    {
        private string inputFolder;

        public string InputFolder
        {
            get { return inputFolder; }
            set { inputFolder = value; }
        }
        private string outputFolder;

        public string OutputFolder
        {
            get { return outputFolder; }
            set { outputFolder = value; }
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

        public BulkDialog()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            this.textBoxInput.Text = inputFolder;
            this.textBoxOutput.Text = outputFolder;

            this.toolTip1.SetToolTip(this.btnInput, Properties.Resources.Browse);
            this.toolTip1.SetToolTip(this.btnOutput, Properties.Resources.Browse);
        }

        protected override void OnClosed(EventArgs ea)
        {
            base.OnClosed(ea);

            inputFolder = this.textBoxInput.Text;
            outputFolder = this.textBoxOutput.Text;
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.Description = "Set Input Image Folder.";
            this.folderBrowserDialog1.SelectedPath = inputFolder;

            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                inputFolder = this.folderBrowserDialog1.SelectedPath;
                this.textBoxInput.Text = inputFolder;
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

        /// <summary>
        /// Changes localized text and messages
        /// </summary>
        /// <param name="locale"></param>
        public virtual void ChangeUILanguage(string locale)
        {
            FormLocalizer localizer = new FormLocalizer(this, typeof(BulkDialog));
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
