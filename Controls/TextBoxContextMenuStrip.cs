using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace VietOCR.NET.Controls
{
    public partial class TextBoxContextMenuStrip : ContextMenuStrip
    {
        public TextBoxContextMenuStrip()
        {
            InitializeComponent();
        }

        public TextBoxContextMenuStrip(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
