using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace VietOCR.NET
{
    public partial class ImageInfoDialog : Form
    {
        Image image;
        bool isProgrammatic;

        public Image Image
        {
            get { return image; }
            set { image = value; }
        }

        public ImageInfoDialog()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            this.textBoxXRes.Text = Math.Round(this.image.HorizontalResolution).ToString();
            this.textBoxYRes.Text = Math.Round(this.image.VerticalResolution).ToString();
            this.textBoxWidth.Text = this.image.Width.ToString();
            this.textBoxHeight.Text = this.image.Height.ToString();
            this.textBoxBitDepth.Text = Bitmap.GetPixelFormatSize(this.image.PixelFormat).ToString();
            this.comboBox3.SelectedIndex = 0;
            this.comboBox4.SelectedIndex = 0;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isProgrammatic)
            {
                isProgrammatic = true;
                this.comboBox4.SelectedItem = this.comboBox3.SelectedItem;
                ConvertUnits(this.comboBox3.SelectedIndex);
                isProgrammatic = false;
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isProgrammatic)
            {
                isProgrammatic = true;
                this.comboBox3.SelectedItem = this.comboBox4.SelectedItem;
                ConvertUnits(this.comboBox4.SelectedIndex);
                isProgrammatic = false;
            }
        }

        private void ConvertUnits(int unit)
        {
            switch (unit)
            {
                case 1: // "inches"
                    this.textBoxWidth.Text = Math.Round(this.image.Width / this.image.HorizontalResolution, 1).ToString();
                    this.textBoxHeight.Text = Math.Round(this.image.Height / this.image.VerticalResolution, 1).ToString();
                    break;

                case 2: //"cm"
                    this.textBoxWidth.Text = Math.Round(this.image.Width / this.image.HorizontalResolution * 2.54, 2).ToString();
                    this.textBoxHeight.Text = Math.Round(this.image.Height / this.image.VerticalResolution * 2.54, 2).ToString();
                    break;

                default: // "pixel"
                    this.textBoxWidth.Text = this.image.Width.ToString();
                    this.textBoxHeight.Text = this.image.Height.ToString();
                    break;
            }
        }
    }
}
