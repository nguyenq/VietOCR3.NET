namespace VietOCR.NET.Controls
{
    partial class ScrollablePictureBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // ScrollablePictureBox
            // 
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ScrollablePictureBox_MouseMove);
            this.GotFocus += new System.EventHandler(this.ScrollablePictureBox_GotFocus);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ScrollablePictureBox_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ScrollablePictureBox_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ScrollablePictureBox_MouseUp);
            this.MouseEnter += new System.EventHandler(this.ScrollablePictureBox_MouseEnter);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
