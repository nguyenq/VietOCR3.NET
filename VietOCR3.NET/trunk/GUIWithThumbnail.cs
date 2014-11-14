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
            Control.ControlCollection col = this.splitContainer2.Panel1.Controls;
            col.Clear();
            group.Controls.Clear();

            int y = 0;
            int index = 0;
            foreach (Image image in imageList)
            {
                RadioButton rb = new RadioButton();
                rb.Appearance = Appearance.Button;
                
                rb.Image = image.GetThumbnailImage(60, 80, null, IntPtr.Zero);
                rb.ImageAlign = ContentAlignment.MiddleCenter;
                rb.Location = new Point(0, y);
                y += 100;
                rb.ImageIndex = index++;
                rb.Click += new System.EventHandler(this.radioButton_Click);
                group.Controls.Add(rb);
                col.Add(rb);
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
