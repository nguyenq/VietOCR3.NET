namespace VietOCR.NET
{
    partial class OptionsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsDialog));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.labelOutputFormat = new System.Windows.Forms.Label();
            this.comboBoxOutputFormat = new System.Windows.Forms.ComboBox();
            this.checkBoxWatch = new System.Windows.Forms.CheckBox();
            this.labelOutput = new System.Windows.Forms.Label();
            this.labelWatch = new System.Windows.Forms.Label();
            this.btnOutput = new System.Windows.Forms.Button();
            this.btnWatch = new System.Windows.Forms.Button();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.textBoxWatch = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBoxDangAmbigs = new System.Windows.Forms.CheckBox();
            this.labelPath = new System.Windows.Forms.Label();
            this.btnDangAmbigs = new System.Windows.Forms.Button();
            this.textBoxDangAmbigs = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelOutputFormat);
            this.tabPage1.Controls.Add(this.comboBoxOutputFormat);
            this.tabPage1.Controls.Add(this.checkBoxWatch);
            this.tabPage1.Controls.Add(this.labelOutput);
            this.tabPage1.Controls.Add(this.labelWatch);
            this.tabPage1.Controls.Add(this.btnOutput);
            this.tabPage1.Controls.Add(this.btnWatch);
            this.tabPage1.Controls.Add(this.textBoxOutput);
            this.tabPage1.Controls.Add(this.textBoxWatch);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // labelOutputFormat
            // 
            resources.ApplyResources(this.labelOutputFormat, "labelOutputFormat");
            this.labelOutputFormat.Name = "labelOutputFormat";
            // 
            // comboBoxOutputFormat
            // 
            this.comboBoxOutputFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOutputFormat.FormattingEnabled = true;
            this.comboBoxOutputFormat.Items.AddRange(new object[] {
            resources.GetString("comboBoxOutputFormat.Items"),
            resources.GetString("comboBoxOutputFormat.Items1"),
            resources.GetString("comboBoxOutputFormat.Items2"),
            resources.GetString("comboBoxOutputFormat.Items3")});
            resources.ApplyResources(this.comboBoxOutputFormat, "comboBoxOutputFormat");
            this.comboBoxOutputFormat.Name = "comboBoxOutputFormat";
            this.comboBoxOutputFormat.MouseHover += new System.EventHandler(this.comboBoxOutputFormat_MouseHover);
            // 
            // checkBoxWatch
            // 
            resources.ApplyResources(this.checkBoxWatch, "checkBoxWatch");
            this.checkBoxWatch.Name = "checkBoxWatch";
            this.checkBoxWatch.UseVisualStyleBackColor = true;
            // 
            // labelOutput
            // 
            resources.ApplyResources(this.labelOutput, "labelOutput");
            this.labelOutput.Name = "labelOutput";
            // 
            // labelWatch
            // 
            resources.ApplyResources(this.labelWatch, "labelWatch");
            this.labelWatch.Name = "labelWatch";
            // 
            // btnOutput
            // 
            resources.ApplyResources(this.btnOutput, "btnOutput");
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnWatch
            // 
            resources.ApplyResources(this.btnWatch, "btnWatch");
            this.btnWatch.Name = "btnWatch";
            this.btnWatch.UseVisualStyleBackColor = true;
            this.btnWatch.Click += new System.EventHandler(this.btnWatch_Click);
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.BackColor = System.Drawing.SystemColors.ControlLightLight;
            resources.ApplyResources(this.textBoxOutput, "textBoxOutput");
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ReadOnly = true;
            // 
            // textBoxWatch
            // 
            this.textBoxWatch.BackColor = System.Drawing.SystemColors.ControlLightLight;
            resources.ApplyResources(this.textBoxWatch, "textBoxWatch");
            this.textBoxWatch.Name = "textBoxWatch";
            this.textBoxWatch.ReadOnly = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkBoxDangAmbigs);
            this.tabPage2.Controls.Add(this.labelPath);
            this.tabPage2.Controls.Add(this.btnDangAmbigs);
            this.tabPage2.Controls.Add(this.textBoxDangAmbigs);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBoxDangAmbigs
            // 
            resources.ApplyResources(this.checkBoxDangAmbigs, "checkBoxDangAmbigs");
            this.checkBoxDangAmbigs.Name = "checkBoxDangAmbigs";
            this.checkBoxDangAmbigs.UseVisualStyleBackColor = true;
            // 
            // labelPath
            // 
            resources.ApplyResources(this.labelPath, "labelPath");
            this.labelPath.Name = "labelPath";
            // 
            // btnDangAmbigs
            // 
            resources.ApplyResources(this.btnDangAmbigs, "btnDangAmbigs");
            this.btnDangAmbigs.Name = "btnDangAmbigs";
            this.btnDangAmbigs.UseVisualStyleBackColor = true;
            this.btnDangAmbigs.Click += new System.EventHandler(this.btnDangAmbigs_Click);
            // 
            // textBoxDangAmbigs
            // 
            this.textBoxDangAmbigs.BackColor = System.Drawing.SystemColors.ControlLightLight;
            resources.ApplyResources(this.textBoxDangAmbigs, "textBoxDangAmbigs");
            this.textBoxDangAmbigs.Name = "textBoxDangAmbigs";
            this.textBoxDangAmbigs.ReadOnly = true;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "txt";
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // OptionsDialog
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsDialog";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelOutput;
        private System.Windows.Forms.Label labelWatch;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.Button btnWatch;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.TextBox textBoxWatch;
        private System.Windows.Forms.CheckBox checkBoxWatch;
        private System.Windows.Forms.CheckBox checkBoxDangAmbigs;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Button btnDangAmbigs;
        private System.Windows.Forms.TextBox textBoxDangAmbigs;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox comboBoxOutputFormat;
        private System.Windows.Forms.Label labelOutputFormat;
    }
}