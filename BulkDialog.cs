using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VietOCR.NET
{
    public partial class BulkDialog : Form
    {
        private string imageFolder;

        public string ImageFolder
        {
            get { return imageFolder; }
            set { imageFolder = value; }
        }
        private string outputFolder;

        public string OutputFolder
        {
            get { return outputFolder; }
            set { outputFolder = value; }
        }

        public BulkDialog()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            this.textBoxFolder.Text = imageFolder;
            this.textBoxOutput.Text = outputFolder;

            this.toolTip1.SetToolTip(this.btnFolder, Properties.Resources.Browse);
            this.toolTip1.SetToolTip(this.btnOutput, Properties.Resources.Browse);
        }

        protected override void OnClosed(EventArgs ea)
        {
            base.OnClosed(ea);

            //watchFolder = this.textBoxWatch.Text;
            //outputFolder = this.textBoxOutput.Text;
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.Description = "Set Image Folder.";
            this.folderBrowserDialog1.SelectedPath = imageFolder;

            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                imageFolder = this.folderBrowserDialog1.SelectedPath;
                this.textBoxFolder.Text = imageFolder;
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
    }
}
