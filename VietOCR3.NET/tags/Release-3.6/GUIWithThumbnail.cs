using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VietOCR.NET
{
    public partial class GUIWithThumbnail : VietOCR.NET.GUI
    {
        GroupBox group = new GroupBox();

        public GUIWithThumbnail()
        {
            InitializeComponent();
        }

        protected override void loadThumbnails()
        {
            this.flowLayoutPanelThumbnail.Controls.Clear();
            group.Controls.Clear();

            this.backgroundWorkerLoadThumbnail.RunWorkerAsync();
        }

        private void backgroundWorkerLoadThumbnail_DoWork(object sender, DoWorkEventArgs e)
        {
            // Create thumbnails
            for (int i = 0; i < imageList.Count; i++)
            {
                Image thumbnail = imageList[i].GetThumbnailImage(85, 110, null, IntPtr.Zero);
                this.backgroundWorkerLoadThumbnail.ReportProgress(i, thumbnail);
            }
        }

        private void backgroundWorkerLoadThumbnail_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Load thumbnails & associated labels into panel
            Image thumbnail = (Image)e.UserState;
            RadioButton rb = new RadioButton();
            rb.Width = thumbnail.Width;
            rb.Appearance = Appearance.Button;
            rb.FlatStyle = FlatStyle.Flat;
            rb.FlatAppearance.BorderColor = Color.Gray;
            rb.BackgroundImage = thumbnail;
            rb.ImageAlign = ContentAlignment.MiddleCenter;
            int horizontalMargin = (this.flowLayoutPanelThumbnail.Width - rb.Width) / 2;
            rb.Margin = new Padding(horizontalMargin, 0, horizontalMargin, 2);
            rb.Height = thumbnail.Height;
            rb.ImageIndex = e.ProgressPercentage;
            rb.Click += new System.EventHandler(this.radioButton_Click);
            group.Controls.Add(rb);
            this.flowLayoutPanelThumbnail.Controls.Add(rb);

            Label label = new Label();
            label.Text = (rb.ImageIndex + 1).ToString();
            label.Width = 20;
            horizontalMargin = (this.flowLayoutPanelThumbnail.Width - label.Width) / 2;
            label.Margin = new Padding(horizontalMargin, 0, horizontalMargin, 0);
            this.flowLayoutPanelThumbnail.Controls.Add(label);
        }

        private void radioButton_Click(object sender, EventArgs e)
        {
            int index = ((RadioButton)sender).ImageIndex;
            if (imageIndex == index)
            {
                return;
            }
            imageIndex = index;
            this.toolStripComboBoxPageNum.SelectedItem = (imageIndex + 1);
        }

        protected override void splitContainerImage_SplitterMoved(object sender, SplitterEventArgs e)
        {
            foreach (Control con in this.flowLayoutPanelThumbnail.Controls)
            {
                int horizontalMargin = (this.flowLayoutPanelThumbnail.Width - con.Width) / 2;
                con.Margin = new Padding(horizontalMargin, 0, horizontalMargin, 2);
            }
        }
    }
}
