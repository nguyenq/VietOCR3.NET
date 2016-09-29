namespace VietOCR.NET
{
    partial class GUIWithFile
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
            this.backgroundWorkerLoad = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerImage)).BeginInit();
            this.splitContainerImage.Panel2.SuspendLayout();
            this.splitContainerImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBox1.Font = new System.Drawing.Font("Courier New", 14.25F);
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.textBox1.Size = new System.Drawing.Size(339, 289);
            this.textBox1.WordWrap = false;
            // 
            // splitContainerImage
            // 
            this.splitContainerImage.Size = new System.Drawing.Size(308, 289);
            // 
            // backgroundWorkerLoad
            // 
            this.backgroundWorkerLoad.WorkerReportsProgress = true;
            this.backgroundWorkerLoad.WorkerSupportsCancellation = true;
            this.backgroundWorkerLoad.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerLoad_DoWork);
            this.backgroundWorkerLoad.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerLoad_RunWorkerCompleted);
            // 
            // GUIWithFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(687, 385);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "GUIWithFile";
            this.splitContainerImage.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerImage)).EndInit();
            this.splitContainerImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorkerLoad;
    }
}
