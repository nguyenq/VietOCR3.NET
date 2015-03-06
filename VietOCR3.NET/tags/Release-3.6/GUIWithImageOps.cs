using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VietOCR.NET
{
    public partial class GUIWithImageOps : VietOCR.NET.GUIWithScan
    {
        //private bool isFitForZoomIn = false;
        private const float ZOOM_FACTOR = 1.25f;

        public GUIWithImageOps()
        {
            InitializeComponent();
        }

        protected override void toolStripBtnPrev_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Deselect();
            imageIndex--;
            if (imageIndex < 0)
            {
                imageIndex = 0;
            }
            else
            {
                this.toolStripStatusLabel1.Text = null;
                this.toolStripComboBoxPageNum.SelectedItem = (imageIndex + 1);
            }
        }

        protected override void toolStripBtnNext_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Deselect();
            imageIndex++;
            if (imageIndex > imageTotal - 1)
            {
                imageIndex = imageTotal - 1;
            }
            else
            {
                this.toolStripStatusLabel1.Text = null;
                this.toolStripComboBoxPageNum.SelectedItem = (imageIndex + 1);
            }
        }
        protected override void toolStripBtnFitImage_Click(object sender, EventArgs e)
        {
            this.toolStripBtnFitImage.Enabled = false;
            this.toolStripBtnActualSize.Enabled = true;
            this.toolStripBtnZoomIn.Enabled = false;
            this.toolStripBtnZoomOut.Enabled = false;
            curScrollPos = this.splitContainerImage.Panel2.AutoScrollPosition;
            this.splitContainerImage.Panel2.AutoScrollPosition = Point.Empty;
            this.pictureBox1.Deselect();

            this.pictureBox1.Dock = DockStyle.None;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            Size fitSize = fitImagetoContainer(this.pictureBox1.Image.Width, this.pictureBox1.Image.Height, this.splitContainerImage.Panel2.Width, this.splitContainerImage.Panel2.Height);
            this.pictureBox1.Width = fitSize.Width;
            this.pictureBox1.Height = fitSize.Height;
            setScale();
            this.centerPicturebox();
            isFitImageSelected = true;
        }  

        protected override void toolStripBtnActualSize_Click(object sender, EventArgs e)
        {
            this.toolStripBtnFitImage.Enabled = true;
            this.toolStripBtnActualSize.Enabled = false;
            this.toolStripBtnZoomIn.Enabled = true;
            this.toolStripBtnZoomOut.Enabled = true;

            this.pictureBox1.Deselect();
            this.pictureBox1.Size = this.pictureBox1.Image.Size;
            this.pictureBox1.Dock = DockStyle.None;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            scaleX = scaleY = 1f;
            this.centerPicturebox();
            this.splitContainerImage.Panel2.AutoScrollPosition = new Point(Math.Abs(curScrollPos.X), Math.Abs(curScrollPos.Y));
            isFitImageSelected = false;
        }

        protected override void toolStripBtnRotateCCW_Click(object sender, EventArgs e)
        {
            this.centerPicturebox();
            this.pictureBox1.Deselect();
            // Rotating 270 degrees is equivalent to rotating -90 degrees.
            imageList[imageIndex].RotateFlip(RotateFlipType.Rotate270FlipNone);
            this.pictureBox1.Image = new Bitmap(imageList[imageIndex]);
            adjustPictureBoxAfterFlip();
            clearStack();
        }

        protected override void toolStripBtnRotateCW_Click(object sender, EventArgs e)
        {
            this.centerPicturebox();
            this.pictureBox1.Deselect();
            imageList[imageIndex].RotateFlip(RotateFlipType.Rotate90FlipNone);
            this.pictureBox1.Image = new Bitmap(imageList[imageIndex]);
            adjustPictureBoxAfterFlip();
            clearStack();
        }

        private void adjustPictureBoxAfterFlip()
        {
            this.pictureBox1.Size = new Size(this.pictureBox1.Height, this.pictureBox1.Width);
            this.pictureBox1.Refresh();
            // recalculate scale factors if in Fit Image mode
            if (this.isFitImageSelected)
            {
                scaleX = (float)this.pictureBox1.Image.Width / (float)this.pictureBox1.Width;
                scaleY = (float)this.pictureBox1.Image.Height / (float)this.pictureBox1.Height;
            }
            this.centerPicturebox();
        }

        protected override void toolStripBtnZoomIn_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Deselect();
            //isFitForZoomIn = true;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            // Zoom works best if you first fit the image according to its true aspect ratio.
            Fit();
            // Make the PictureBox dimensions larger by 25% to effect the Zoom.
            this.pictureBox1.Width = Convert.ToInt32(this.pictureBox1.Width * ZOOM_FACTOR);
            this.pictureBox1.Height = Convert.ToInt32(this.pictureBox1.Height * ZOOM_FACTOR);
            scaleX = (float)this.pictureBox1.Image.Width / (float)this.pictureBox1.Width;
            scaleY = (float)this.pictureBox1.Image.Height / (float)this.pictureBox1.Height;
            this.centerPicturebox();
            isFitImageSelected = false;
            this.toolStripBtnActualSize.Enabled = true;
        }

        protected override void toolStripBtnZoomOut_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Deselect();
            //isFitForZoomIn = false;
            // StretchImage SizeMode works best for zooming.
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            // Zoom works best if you first fit the image according to its true aspect ratio.
            Fit();
            // Make the PictureBox dimensions smaller by 25% to effect the Zoom.
            this.pictureBox1.Width = Convert.ToInt32(this.pictureBox1.Width / ZOOM_FACTOR);
            this.pictureBox1.Height = Convert.ToInt32(this.pictureBox1.Height / ZOOM_FACTOR);
            scaleX = (float)this.pictureBox1.Image.Width / (float)this.pictureBox1.Width;
            scaleY = (float)this.pictureBox1.Image.Height / (float)this.pictureBox1.Height;
            this.centerPicturebox();
            isFitImageSelected = false;
            this.toolStripBtnActualSize.Enabled = true;
        }

        // This method makes the image fit properly in the PictureBox. You might think 
        // that the AutoSize SizeMode enum would make the image appear in the PictureBox 
        // according to its true aspect ratio within the fixed bounds of the PictureBox.
        // However, it merely expands or shrinks the PictureBox.
        private void Fit()
        {
            // if Fit was called by the Zoom In button, then center the image. This is
            // only needed when working with images that are smaller than the PictureBox.
            // Feel free to uncomment the line that sets the SizeMode and then see how
            // it causes Zoom In for small images to show unexpected behavior.

            //if ((this.pictureBox1.Image.Width < this.pictureBox1.Width) &&
            //    (this.pictureBox1.Image.Height < this.pictureBox1.Height))
            //{
            //    if (!isFitForZoomIn)
            //    {
            //        this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            //    }
            //}
            //CalculateAspectRatioAndSetDimensions();
        }

        //// Calculates and returns the image's aspect ratio, and sets 
        //// its proper dimensions. This is used for Fit() and for saving thumbnails
        //// of images.
        //private double CalculateAspectRatioAndSetDimensions()
        //{
        //    // Calculate the proper aspect ratio and set the image's dimensions.
        //    double ratio;

        //    if (this.pictureBox1.Image.Width > this.pictureBox1.Image.Height)
        //    {
        //        ratio = this.pictureBox1.Image.Width / this.pictureBox1.Image.Height;
        //        this.pictureBox1.Height = Convert.ToInt32(Convert.ToDouble(this.pictureBox1.Width) / ratio);
        //    }
        //    else
        //    {
        //        ratio = this.pictureBox1.Image.Height / this.pictureBox1.Image.Width;
        //        this.pictureBox1.Width = Convert.ToInt32(Convert.ToDouble(this.pictureBox1.Height) / ratio);
        //    }
        //    return ratio;
        //}
    }
}
