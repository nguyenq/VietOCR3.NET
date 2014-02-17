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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BulkDialog));
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
            this.comboBoxOutputFormat = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelOutput
            // 
            resources.ApplyResources(this.labelOutput, "labelOutput");
            this.labelOutput.Name = "labelOutput";
            // 
            // labelInput
            // 
            resources.ApplyResources(this.labelInput, "labelInput");
            this.labelInput.Name = "labelInput";
            // 
            // btnOutput
            // 
            resources.ApplyResources(this.btnOutput, "btnOutput");
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnInput
            // 
            resources.ApplyResources(this.btnInput, "btnInput");
            this.btnInput.Name = "btnInput";
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.BackColor = System.Drawing.SystemColors.ControlLightLight;
            resources.ApplyResources(this.textBoxOutput, "textBoxOutput");
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ReadOnly = true;
            // 
            // textBoxInput
            // 
            this.textBoxInput.BackColor = System.Drawing.SystemColors.ControlLightLight;
            resources.ApplyResources(this.textBoxInput, "textBoxInput");
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.ReadOnly = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonRun
            // 
            this.buttonRun.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.buttonRun, "buttonRun");
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.UseVisualStyleBackColor = true;
            // 
            // comboBoxOutputFormat
            // 
            this.comboBoxOutputFormat.FormattingEnabled = true;
            this.comboBoxOutputFormat.Items.AddRange(new object[] {
            resources.GetString("comboBoxOutputFormat.Items"),
            resources.GetString("comboBoxOutputFormat.Items1"),
            resources.GetString("comboBoxOutputFormat.Items2")});
            resources.ApplyResources(this.comboBoxOutputFormat, "comboBoxOutputFormat");
            this.comboBoxOutputFormat.Name = "comboBoxOutputFormat";
            // 
            // BulkDialog
            // 
            this.AcceptButton = this.buttonRun;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxOutputFormat);
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
        private System.Windows.Forms.ComboBox comboBoxOutputFormat;
    }
}