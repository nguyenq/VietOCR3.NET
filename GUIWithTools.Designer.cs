namespace VietOCR.NET
{
    partial class GUIWithTools
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundWorkerSplitPdf = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerMergeTiff = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerMergePdf = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerSplitTiff = new System.ComponentModel.BackgroundWorker();
            this.splitContainerImage.Panel2.SuspendLayout();
            this.splitContainerImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.textBox1.Size = new System.Drawing.Size(396, 541);
            this.textBox1.WordWrap = false;
            // 
            // splitContainer2
            // 
            this.splitContainerImage.Size = new System.Drawing.Size(331, 541);
            // 
            // backgroundWorkerSplitPdf
            // 
            this.backgroundWorkerSplitPdf.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerSplitPdf_DoWork);
            this.backgroundWorkerSplitPdf.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerSplitPdf_ProgressChanged);
            this.backgroundWorkerSplitPdf.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerSplitPdf_RunWorkerCompleted);
            // 
            // backgroundWorkerMergeTiff
            // 
            this.backgroundWorkerMergeTiff.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerMergeTiff_DoWork);
            this.backgroundWorkerMergeTiff.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerMergeTiff_ProgressChanged);
            this.backgroundWorkerMergeTiff.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerMergeTiff_RunWorkerCompleted);
            // 
            // backgroundWorkerMergePdf
            // 
            this.backgroundWorkerMergePdf.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerMergePdf_DoWork);
            this.backgroundWorkerMergePdf.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerMergePdf_ProgressChanged);
            this.backgroundWorkerMergePdf.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerMergePdf_RunWorkerCompleted);
            // 
            // backgroundWorkerSplitTiff
            // 
            this.backgroundWorkerSplitTiff.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerSplitTiff_DoWork);
            this.backgroundWorkerSplitTiff.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerSplitTiff_ProgressChanged);
            this.backgroundWorkerSplitTiff.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerSplitTiff_RunWorkerCompleted);
            // 
            // GUIWithTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(774, 626);
            this.Name = "GUIWithTools";
            this.splitContainerImage.Panel2.ResumeLayout(false);
            this.splitContainerImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorkerSplitPdf;
        private System.ComponentModel.BackgroundWorker backgroundWorkerMergeTiff;
        private System.ComponentModel.BackgroundWorker backgroundWorkerMergePdf;
        private System.ComponentModel.BackgroundWorker backgroundWorkerSplitTiff;
    }
}
