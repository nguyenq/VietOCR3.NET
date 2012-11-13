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

        public BulkDialog()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            //this.textBoxWatch.Text = watchFolder;
            //this.textBoxOutput.Text = outputFolder;
            
            //this.toolTip1.SetToolTip(this.btnWatch, Properties.Resources.Browse);
            //this.toolTip1.SetToolTip(this.btnOutput, Properties.Resources.Browse);
        }

        protected override void OnClosed(EventArgs ea)
        {
            base.OnClosed(ea);

            //watchFolder = this.textBoxWatch.Text;
            //outputFolder = this.textBoxOutput.Text;
        }
    }
}
