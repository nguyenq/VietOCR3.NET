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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using VietOCR.NET.Utilities;
using System.Drawing.Imaging;

namespace VietOCR.NET
{
    public partial class GUIWithImage : VietOCR.NET.GUIWithBulkOCR
    {
        const string strScreenshotMode = "ScreenshotMode";
        const double MINIMUM_DESKEW_THRESHOLD = 0.05d;
        FixedSizeStack<Image> stack = new FixedSizeStack<Image>(10);
        Image originalImage;

        public GUIWithImage()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);
            toolStripStatusLabelSMvalue.Text = this.screenshotModeToolStripMenuItem.Checked ? "On" : "Off";
        }

        protected override void metadataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imageList == null)
            {
                MessageBox.Show(this, Properties.Resources.LoadImage, strProgName);
                return;
            }

            ImageInfoDialog dialog = new ImageInfoDialog();
            dialog.Image = imageList[imageIndex];

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Do nothing for now. 
                // Initial plan was to implement various image manipulation operations 
                // (rotate, flip, sharpen, brighten, threshold, clean up,...) here.
            }
        }

        protected override void brightenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imageList == null)
            {
                MessageBox.Show(this, Properties.Resources.LoadImage, strProgName);
                return;
            }
            TrackbarDialog dialog = new TrackbarDialog();
            dialog.LabelText = Properties.Resources.Brightness;
            dialog.ValueUpdated += new TrackbarDialog.HandleValueChange(UpdatedBrightness);

            originalImage = imageList[imageIndex];
            stack.Push(originalImage);
            if (dialog.ShowDialog() == DialogResult.Cancel)
            {
                // restore original image
                imageList[imageIndex] = originalImage;
                this.pictureBox1.Image = new Bitmap(originalImage);
            }
        }

        protected override void contrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imageList == null)
            {
                MessageBox.Show(this, Properties.Resources.LoadImage, strProgName);
                return;
            }
            TrackbarDialog dialog = new TrackbarDialog();
            dialog.LabelText = Properties.Resources.Contrast;
            dialog.SetForContrast();
            dialog.ValueUpdated += new TrackbarDialog.HandleValueChange(UpdatedContrast);

            originalImage = imageList[imageIndex];
            stack.Push(originalImage);
            if (dialog.ShowDialog() == DialogResult.Cancel)
            {
                // restore original image
                imageList[imageIndex] = originalImage;
                this.pictureBox1.Image = new Bitmap(originalImage);
            }
        }

        private void UpdatedBrightness(object sender, TrackbarDialog.ValueChangedEventArgs e)
        {
            Image image = ImageHelper.Brighten(originalImage, e.NewValue * 0.005f);
            if (image != null)
            {
                imageList[imageIndex] = image;
                this.pictureBox1.Image = new Bitmap(image);
            }
        }

        private void UpdatedContrast(object sender, TrackbarDialog.ValueChangedEventArgs e)
        {
            Image image = ImageHelper.Contrast(originalImage, e.NewValue * 0.04f);
            if (image != null)
            {
                imageList[imageIndex] = image;
                this.pictureBox1.Image = new Bitmap(image);
            }
        }

        protected override void deskewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imageList == null)
            {
                MessageBox.Show(this, Properties.Resources.LoadImage, strProgName);
                return;
            }
            this.pictureBox1.Deselect();
            this.Cursor = Cursors.WaitCursor;

            gmseDeskew deskew = new gmseDeskew((Bitmap)this.pictureBox1.Image);
            double imageSkewAngle = deskew.GetSkewAngle();

            if ((imageSkewAngle > MINIMUM_DESKEW_THRESHOLD || imageSkewAngle < -(MINIMUM_DESKEW_THRESHOLD)))
            {
                originalImage = imageList[imageIndex];
                stack.Push(originalImage);
                imageList[imageIndex] = ImageHelper.Rotate((Bitmap)originalImage, -imageSkewAngle);
                this.pictureBox1.Image = new Bitmap(imageList[imageIndex]);
            }
            this.Cursor = Cursors.Default;
        }

        protected override void autocropToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imageList == null)
            {
                MessageBox.Show(this, Properties.Resources.LoadImage, strProgName);
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            originalImage = imageList[imageIndex];
            imageList[imageIndex] = ImageHelper.AutoCrop((Bitmap)originalImage, 0.1);

            // if same image, skip
            if (originalImage != imageList[imageIndex])
            {
                stack.Push(originalImage);
                displayImage();
            }

            this.Cursor = Cursors.Default;
        }

        protected override void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imageList == null)
            {
                MessageBox.Show(this, Properties.Resources.LoadImage, strProgName);
                return;
            }
            originalImage = imageList[imageIndex];
            stack.Push(originalImage);
            imageList[imageIndex] = ImageHelper.ConvertGrayscale((Bitmap)originalImage);
            this.pictureBox1.Image = new Bitmap(imageList[imageIndex]);
        }

        protected override void monochromeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imageList == null)
            {
                MessageBox.Show(this, Properties.Resources.LoadImage, strProgName);
                return;
            }
            originalImage = imageList[imageIndex];
            stack.Push(originalImage);
            imageList[imageIndex] = ImageHelper.ConvertMonochrome((Bitmap)originalImage);
            this.pictureBox1.Image = new Bitmap(imageList[imageIndex]);
        }

        protected override void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imageList == null)
            {
                MessageBox.Show(this, Properties.Resources.LoadImage, strProgName);
                return;
            }
            originalImage = imageList[imageIndex];
            stack.Push(originalImage);
            imageList[imageIndex] = ImageHelper.InvertColor((Bitmap)originalImage);
            this.pictureBox1.Image = new Bitmap(imageList[imageIndex]);
        }

        protected override void sharpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imageList == null)
            {
                MessageBox.Show(this, Properties.Resources.LoadImage, strProgName);
                return;
            }
            originalImage = imageList[imageIndex];
            stack.Push(originalImage);
            imageList[imageIndex] = ImageHelper.Sharpen((Bitmap)originalImage);
            this.pictureBox1.Image = new Bitmap(imageList[imageIndex]);
        }

        protected override void smoothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imageList == null)
            {
                MessageBox.Show(this, Properties.Resources.LoadImage, strProgName);
                return;
            }
            originalImage = imageList[imageIndex];
            stack.Push(originalImage);
            imageList[imageIndex] = ImageHelper.GaussianBlur((Bitmap)originalImage);
            this.pictureBox1.Image = new Bitmap(imageList[imageIndex]);
        }

        protected override void screenshotModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            mi.Checked ^= true;
            toolStripStatusLabelSMvalue.Text = mi.Checked ? "On" : "Off";
        }

        protected override void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (stack.Count == 0)
            {
                return;
            }

            Image image = stack.Pop();
            imageList[imageIndex] = image;
            displayImage();
        }

        protected override void clearStack()
        {
            stack.Clear();
        }

        protected override void LoadRegistryInfo(RegistryKey regkey)
        {
            base.LoadRegistryInfo(regkey);

            this.screenshotModeToolStripMenuItem.Checked = Convert.ToBoolean(
                (int)regkey.GetValue(strScreenshotMode, Convert.ToInt32(false)));
        }

        protected override void SaveRegistryInfo(RegistryKey regkey)
        {
            base.SaveRegistryInfo(regkey);

            regkey.SetValue(strScreenshotMode, Convert.ToInt32(this.screenshotModeToolStripMenuItem.Checked));
        }
    }
}
