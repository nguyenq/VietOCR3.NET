namespace VietOCR.NET
{
    partial class BulkDialog
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
            this.components = new System.ComponentModel.Container();
            this.labelOutput = new System.Windows.Forms.Label();
            this.labelFolder = new System.Windows.Forms.Label();
            this.btnOutput = new System.Windows.Forms.Button();
            this.btnFolder = new System.Windows.Forms.Button();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.textBoxFolder = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonRun = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // labelOutput
            // 
            this.labelOutput.AutoSize = true;
            this.labelOutput.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelOutput.Location = new System.Drawing.Point(11, 50);
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(74, 13);
            this.labelOutput.TabIndex = 19;
            this.labelOutput.Text = "Output Folder:";
            // 
            // labelFolder
            // 
            this.labelFolder.AutoSize = true;
            this.labelFolder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelFolder.Location = new System.Drawing.Point(11, 24);
            this.labelFolder.Name = "labelFolder";
            this.labelFolder.Size = new System.Drawing.Size(71, 13);
            this.labelFolder.TabIndex = 18;
            this.labelFolder.Text = "Image Folder:";
            // 
            // btnOutput
            // 
            this.btnOutput.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput.Location = new System.Drawing.Point(274, 45);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(30, 23);
            this.btnOutput.TabIndex = 17;
            this.btnOutput.Text = "...";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnFolder
            // 
            this.btnFolder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFolder.Location = new System.Drawing.Point(274, 19);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(30, 23);
            this.btnFolder.TabIndex = 16;
            this.btnFolder.Text = "...";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxOutput.Location = new System.Drawing.Point(86, 47);
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ReadOnly = true;
            this.textBoxOutput.Size = new System.Drawing.Size(182, 20);
            this.textBoxOutput.TabIndex = 15;
            // 
            // textBoxFolder
            // 
            this.textBoxFolder.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxFolder.Location = new System.Drawing.Point(86, 21);
            this.textBoxFolder.Name = "textBoxFolder";
            this.textBoxFolder.ReadOnly = true;
            this.textBoxFolder.Size = new System.Drawing.Size(182, 20);
            this.textBoxFolder.TabIndex = 14;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonCancel.Location = new System.Drawing.Point(229, 85);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 21;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonRun
            // 
            this.buttonRun.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonRun.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonRun.Location = new System.Drawing.Point(148, 85);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(75, 23);
            this.buttonRun.TabIndex = 20;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            // 
            // BulkDialog
            // 
            this.AcceptButton = this.buttonRun;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 126);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.labelOutput);
            this.Controls.Add(this.labelFolder);
            this.Controls.Add(this.btnOutput);
            this.Controls.Add(this.btnFolder);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.textBoxFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BulkDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bulk OCR";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelOutput;
        private System.Windows.Forms.Label labelFolder;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.TextBox textBoxFolder;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}