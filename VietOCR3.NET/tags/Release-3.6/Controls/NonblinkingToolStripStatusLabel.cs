using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace VietOCR.NET.Controls
{
    /// <summary>
    /// Works around a blinking tooltip issue when window maximized.
    /// https://social.msdn.microsoft.com/Forums/en-US/c48fc24e-9bd6-4879-a992-507b8e008b52/blinking-tooltip-of-toolstripstatuslabel
    /// </summary>
    public partial class NonblinkingToolStripStatusLabel : ToolStripStatusLabel
    {
        public NonblinkingToolStripStatusLabel()
        {
            InitializeComponent();
        }

        public NonblinkingToolStripStatusLabel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public ToolTip ToolTip
        {
            set;
            get;
        }

        protected override void OnMouseHover(EventArgs e)
        {
            if (ToolTip != null)
            {
                Point loc = new Point(Control.MousePosition.X, Control.MousePosition.Y - 30);
                loc = this.Parent.PointToClient(loc);
                ToolTip.Show(this.ToolTipText, this.Parent, loc);
            }
            else
            {
                base.OnMouseHover(e);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (ToolTip != null)
            {
                ToolTip.Hide(this.Parent);
            }
            base.OnMouseLeave(e);
        }
    }
}
