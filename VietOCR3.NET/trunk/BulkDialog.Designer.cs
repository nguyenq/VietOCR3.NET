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
            this.labelInput = new System.Windows.Forms.Label();
            this.btnOutput = new System.Windows.Forms.Button();
            this.btnInput = new System.Windows.Forms.Button();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.textBoxInput = new System.Windows.Forms.TextBox();
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
            // labelInput
            // 
            this.labelInput.AutoSize = true;
            this.labelInput.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelInput.Location = new System.Drawing.Point(11, 24);
            this.labelInput.Name = "labelInput";
            this.labelInput.Size = new System.Drawing.Size(66, 13);
            this.labelInput.TabIndex = 18;
            this.labelInput.Text = "Input Folder:";
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
            // btnInput
            // 
            this.btnInput.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput.Location = new System.Drawing.Point(274, 19);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(30, 23);
            this.btnInput.TabIndex = 16;
            this.btnInput.Text = "...";
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
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
            this.textBoxInput.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxInput.Location = new System.Drawing.Point(86, 21);
            this.textBoxInput.Name = "textBoxFolder";
            this.textBoxInput.ReadOnly = true;
            this.textBoxInput.Size = new System.Drawing.Size(182, 20);
            this.textBoxInput.TabIndex = 14;
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
            this.Controls.Add(this.labelInput);
            this.Controls.Add(this.btnOutput);
            this.Controls.Add(this.btnInput);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.textBoxInput);
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
        private System.Windows.Forms.Label labelInput;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.Button btnInput;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}