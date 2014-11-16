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
            Control.ControlCollection col = this.flowLayoutPanelThumbnail.Controls;
            col.Clear();
            group.Controls.Clear();

            int index = 0;

            foreach (Image image in imageList)
            {
                RadioButton rb = new RadioButton();
                rb.Appearance = Appearance.Button;
                rb.BackgroundImage = image.GetThumbnailImage(100, 100, null, IntPtr.Zero);
                rb.ImageAlign = ContentAlignment.MiddleCenter;
                int horizontalMargin = (this.flowLayoutPanelThumbnail.Width - rb.Width) / 2;
                rb.Margin = new Padding(horizontalMargin, 0, horizontalMargin, 0);
                rb.Height = 100;
                rb.ImageIndex = index++;
                rb.Click += new System.EventHandler(this.radioButton_Click);
                group.Controls.Add(rb);
                col.Add(rb);
                Label label = new Label();
                label.Text = rb.ImageIndex.ToString();
                label.Width = 20;
                horizontalMargin = (this.flowLayoutPanelThumbnail.Width - label.Width) / 2;
                label.Margin = new Padding(horizontalMargin, 0, horizontalMargin, 0);
                col.Add(label);
            }

            //loadImages.execute();
        }

        void radioButton_Click(object sender, EventArgs e)
        {
            int index = ((RadioButton)sender).ImageIndex;
            if (imageIndex == index)
            {
                return;
            }
            imageIndex = index;
            displayImage();
            clearStack();
            setButton();
        }
    }
}
