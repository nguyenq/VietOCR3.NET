using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

namespace VietOCR.NET
{
    public partial class TrackbarDialog : Form
    {
        public class ValueChangedEventArgs : EventArgs
        {
            public float NewValue;

            public ValueChangedEventArgs(float value)
                : base()
            {
                this.NewValue = value;
            }
        }

        public string LabelText
        {
            set
            {
                this.label1.Text = value;
            }
        }

        public delegate void HandleValueChange(object sender, ValueChangedEventArgs e);
        public event HandleValueChange ValueUpdated;

        public TrackbarDialog()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Dispose();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Dispose();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueUpdated != null)
            {
                ValueChangedEventArgs args = new ValueChangedEventArgs(this.trackBar1.Value);
                this.ValueUpdated(this, args);
            }
        }

        public void SetForContrast()
        {
            this.trackBar1.Minimum = 0;
            this.trackBar1.Value = 25;
            this.trackBar1.TickFrequency = 10;
        }
    }
}
