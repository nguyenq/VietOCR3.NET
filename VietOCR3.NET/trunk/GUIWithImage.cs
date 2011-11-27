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

namespace VietOCR.NET
{
    public partial class GUIWithImage : VietOCR.NET.GUIWithPostprocess
    {
        const string strScreenshotMode = "ScreenshotMode";
        const double MINIMUM_DESKEW_THRESHOLD = 0.05d;

        public GUIWithImage()
        {
            InitializeComponent();
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
                imageList[imageIndex] = ImageHelper.Rotate((Bitmap)imageList[imageIndex], -imageSkewAngle);
                this.pictureBox1.Image = new Bitmap(imageList[imageIndex]);
            }
            this.Cursor = Cursors.Default;
        }

        protected override void screenshotModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            mi.Checked ^= true;
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
