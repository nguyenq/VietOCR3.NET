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
            this.labelOutputFormat = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelOutput
            // 
            resources.ApplyResources(this.labelOutput, "labelOutput");
            this.labelOutput.Name = "labelOutput";
            this.toolTip1.SetToolTip(this.labelOutput, resources.GetString("labelOutput.ToolTip"));
            // 
            // labelInput
            // 
            resources.ApplyResources(this.labelInput, "labelInput");
            this.labelInput.Name = "labelInput";
            this.toolTip1.SetToolTip(this.labelInput, resources.GetString("labelInput.ToolTip"));
            // 
            // btnOutput
            // 
            resources.ApplyResources(this.btnOutput, "btnOutput");
            this.btnOutput.Name = "btnOutput";
            this.toolTip1.SetToolTip(this.btnOutput, resources.GetString("btnOutput.ToolTip"));
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnInput
            // 
            resources.ApplyResources(this.btnInput, "btnInput");
            this.btnInput.Name = "btnInput";
            this.toolTip1.SetToolTip(this.btnInput, resources.GetString("btnInput.ToolTip"));
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // textBoxOutput
            // 
            resources.ApplyResources(this.textBoxOutput, "textBoxOutput");
            this.textBoxOutput.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ReadOnly = true;
            this.toolTip1.SetToolTip(this.textBoxOutput, resources.GetString("textBoxOutput.ToolTip"));
            // 
            // textBoxInput
            // 
            resources.ApplyResources(this.textBoxInput, "textBoxInput");
            this.textBoxInput.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.ReadOnly = true;
            this.toolTip1.SetToolTip(this.textBoxInput, resources.GetString("textBoxInput.ToolTip"));
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.toolTip1.SetToolTip(this.buttonCancel, resources.GetString("buttonCancel.ToolTip"));
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonRun
            // 
            resources.ApplyResources(this.buttonRun, "buttonRun");
            this.buttonRun.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonRun.Name = "buttonRun";
            this.toolTip1.SetToolTip(this.buttonRun, resources.GetString("buttonRun.ToolTip"));
            this.buttonRun.UseVisualStyleBackColor = true;
            // 
            // folderBrowserDialog1
            // 
            resources.ApplyResources(this.folderBrowserDialog1, "folderBrowserDialog1");
            // 
            // comboBoxOutputFormat
            // 
            resources.ApplyResources(this.comboBoxOutputFormat, "comboBoxOutputFormat");
            this.comboBoxOutputFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOutputFormat.FormattingEnabled = true;
            this.comboBoxOutputFormat.Items.AddRange(new object[] {
            resources.GetString("comboBoxOutputFormat.Items"),
            resources.GetString("comboBoxOutputFormat.Items1"),
            resources.GetString("comboBoxOutputFormat.Items2")});
            this.comboBoxOutputFormat.Name = "comboBoxOutputFormat";
            this.toolTip1.SetToolTip(this.comboBoxOutputFormat, resources.GetString("comboBoxOutputFormat.ToolTip"));
            this.comboBoxOutputFormat.MouseHover += new System.EventHandler(this.comboBoxOutputFormat_MouseHover);
            // 
            // labelOutputFormat
            // 
            resources.ApplyResources(this.labelOutputFormat, "labelOutputFormat");
            this.labelOutputFormat.Name = "labelOutputFormat";
            this.toolTip1.SetToolTip(this.labelOutputFormat, resources.GetString("labelOutputFormat.ToolTip"));
            // 
            // BulkDialog
            // 
            this.AcceptButton = this.buttonRun;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelOutputFormat);
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
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
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
        private System.Windows.Forms.Label labelOutputFormat;
    }
}