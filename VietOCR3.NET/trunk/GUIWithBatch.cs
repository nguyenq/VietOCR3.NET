using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VietOCR.NET
{
    public partial class GUIWithBatch : VietOCR.NET.GUIWithSettings
    {
        const string strWatchFolder = "ImageFolder";
        const string strOutputFolder = "BulkOutputFolder";

        private string imageFolder;
        private string bulkOutputFolder;

        private BulkDialog bulkDialog;

        public GUIWithBatch()
        {
            InitializeComponent();
        }

        protected override void bulkOCRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bulkDialog == null)
            {
                bulkDialog = new BulkDialog();
            }

            bulkDialog.WatchFolder = imageFolder;
            bulkDialog.OutputFolder = bulkOutputFolder;

            if (bulkDialog.ShowDialog() == DialogResult.OK)
            {
                imageFolder = bulkDialog.WatchFolder;
                bulkOutputFolder = bulkDialog.OutputFolder;
            }
        }
    }
}
