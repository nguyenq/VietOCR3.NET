namespace VietOCR.NET
{
    partial class GUIWithThumbnail
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
            this.backgroundWorkerLoadThumbnail = new System.ComponentModel.BackgroundWorker();
            this.splitContainerImage.Panel2.SuspendLayout();
            this.splitContainerImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBox1.Font = new System.Drawing.Font("Arial", 18F);
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.textBox1.Size = new System.Drawing.Size(479, 556);
            this.textBox1.WordWrap = false;
            // 
            // splitContainerImage
            // 
            this.splitContainerImage.Size = new System.Drawing.Size(414, 556);
            // 
            // backgroundWorkerLoadThumbnail
            // 
            this.backgroundWorkerLoadThumbnail.WorkerReportsProgress = true;
            this.backgroundWorkerLoadThumbnail.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerLoadThumbnail_DoWork);
            this.backgroundWorkerLoadThumbnail.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerLoadThumbnail_ProgressChanged);
            // 
            // GUIWithThumbnail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 641);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "GUIWithThumbnail";
            this.splitContainerImage.Panel2.ResumeLayout(false);
            this.splitContainerImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorkerLoadThumbnail;
    }
}