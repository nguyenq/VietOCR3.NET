using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VietOCR.NET
{
    public partial class ChangeCaseDialog : Form
    {
        private string selectedCase;

        public event EventHandler CloseDlg;
        public event EventHandler ChangeCase;

        public string SelectedCase
        {
            set { selectedCase = value; }
            get { return selectedCase; }
        }

        public ChangeCaseDialog()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            foreach (RadioButton rb in this.groupBox1.Controls)
            {
                if (rb.Tag.ToString() == selectedCase)
                {
                    // Select Case last saved
                    rb.Checked = true;
                    break;
                }
            }
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            foreach (RadioButton rb in this.groupBox1.Controls)
            {
                if (rb.Checked)
                {
                    selectedCase = rb.Tag.ToString();
                    break;
                }
            }

            if (ChangeCase != null)
                ChangeCase(this, EventArgs.Empty);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (CloseDlg != null)
                CloseDlg(this, EventArgs.Empty);

            Close();
        }

    }
}
