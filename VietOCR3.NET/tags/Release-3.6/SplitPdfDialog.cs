using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using VietOCR.NET.Utilities;

namespace VietOCR.NET
{
    public partial class SplitPdfDialog : Form
    {
        SplitPdfArgs args;
        string pdfFolder = null;

        internal SplitPdfArgs Args
        {
            get { return args; }
        }

        public SplitPdfDialog()
        {
            InitializeComponent();
            disableBoxes(!this.radioButtonPages.Checked);
            this.toolTip1.SetToolTip(this.buttonBrowseInput, Properties.Resources.Browse);
            this.toolTip1.SetToolTip(this.buttonBrowseOutput, Properties.Resources.Browse);
        }

        private void radioButtonPages_CheckedChanged(object sender, EventArgs e)
        {
            disableBoxes(false);
        }

        private void radioButtonFiles_CheckedChanged(object sender, EventArgs e)
        {
            disableBoxes(true);
        }

        void disableBoxes(bool enabled)
        {
            this.textBoxNumOfPages.Enabled = enabled;
            this.textBoxFrom.Enabled = !enabled;
            this.textBoxTo.Enabled = !enabled;
        }

        private void buttonBrowseInput_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = Properties.Resources.Open;
            dialog.InitialDirectory = pdfFolder;
            dialog.Filter = "PDF (*.pdf)|*.pdf";
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxInput.Text = dialog.FileName;
                pdfFolder = Path.GetDirectoryName(dialog.FileName);
            }
        }

        private void buttonBrowseOutput_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = Properties.Resources.Save;
            dialog.InitialDirectory = pdfFolder;
            dialog.Filter = "PDF (*.pdf)|*.pdf";
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxOutput.Text = dialog.FileName;

                if (!this.textBoxOutput.Text.EndsWith(".pdf") )
                {
                    this.textBoxOutput.Text += ".pdf"; // seems not needed
                }
            }
        }
       
        private void buttonSplit_Click(object sender, EventArgs e)
        {
            SplitPdfArgs args = new SplitPdfArgs();
            args.InputFilename = this.textBoxInput.Text;
            args.OutputFilename = this.textBoxOutput.Text;
            args.FromPage = this.textBoxFrom.Text;
            args.ToPage = this.textBoxTo.Text;
            args.NumOfPages = this.textBoxNumOfPages.Text;
            args.Pages = this.radioButtonPages.Checked;

            if (args.InputFilename.Length > 0 && args.OutputFilename.Length > 0 && 
                ((this.radioButtonPages.Checked && args.FromPage.Length > 0) || 
                (this.radioButtonFiles.Checked && args.NumOfPages.Length > 0)))
            {
                Regex regexNums = new Regex(@"^\d+$");

                if ((this.radioButtonPages.Checked && regexNums.IsMatch(args.FromPage) && (args.ToPage.Length > 0? regexNums.IsMatch(args.ToPage) : true)) || (this.radioButtonFiles.Checked && regexNums.IsMatch(args.NumOfPages)))
                {
                    this.args = args;
                }
                else
                {
                    MessageBox.Show(this, "Input invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.None;       
                }
            }
            else
            {
                MessageBox.Show(this, "Input incomplete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
            }
        }

        /// <summary>
        /// Changes localized text and messages
        /// </summary>
        /// <param name="locale"></param>
        public virtual void ChangeUILanguage(string locale)
        {
            FormLocalizer localizer = new FormLocalizer(this, typeof(SplitPdfDialog));
            localizer.ApplyCulture(new CultureInfo(locale));
        }

        class NumericTextBox : TextBox
        {
            protected override void OnKeyPress(KeyPressEventArgs e)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                base.OnKeyPress(e);
            }
        }
    }
}
