namespace VietOCR.NET
{
    partial class GUIWithPostprocess
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
            this.backgroundWorkerCorrect = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.textBox1.Size = new System.Drawing.Size(351, 427);
            // 
            // backgroundWorkerCorrect
            // 
            this.backgroundWorkerCorrect.WorkerReportsProgress = true;
            this.backgroundWorkerCorrect.WorkerSupportsCancellation = true;
            this.backgroundWorkerCorrect.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerCorrect_DoWork);
            this.backgroundWorkerCorrect.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerCorrect_RunWorkerCompleted);
            // 
            // GUIWithPostprocess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(687, 498);
            this.Name = "GUIWithPostprocess";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorkerCorrect;
    }
}
