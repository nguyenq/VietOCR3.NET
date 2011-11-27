namespace VietOCR.NET
{
    partial class ChangeCaseDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeCaseDialog));
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.buttonChange = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButton1
            // 
            this.radioButton1.AccessibleDescription = null;
            this.radioButton1.AccessibleName = null;
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.BackgroundImage = null;
            this.radioButton1.Font = null;
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.TabStop = true;
            this.radioButton1.Tag = "Sentence_case";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AccessibleDescription = null;
            this.radioButton2.AccessibleName = null;
            resources.ApplyResources(this.radioButton2, "radioButton2");
            this.radioButton2.BackgroundImage = null;
            this.radioButton2.Font = null;
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.TabStop = true;
            this.radioButton2.Tag = "lowercase";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AccessibleDescription = null;
            this.radioButton3.AccessibleName = null;
            resources.ApplyResources(this.radioButton3, "radioButton3");
            this.radioButton3.BackgroundImage = null;
            this.radioButton3.Font = null;
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.TabStop = true;
            this.radioButton3.Tag = "UPPERCASE";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AccessibleDescription = null;
            this.radioButton4.AccessibleName = null;
            resources.ApplyResources(this.radioButton4, "radioButton4");
            this.radioButton4.BackgroundImage = null;
            this.radioButton4.Font = null;
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.TabStop = true;
            this.radioButton4.Tag = "Title_Case";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // buttonChange
            // 
            this.buttonChange.AccessibleDescription = null;
            this.buttonChange.AccessibleName = null;
            resources.ApplyResources(this.buttonChange, "buttonChange");
            this.buttonChange.BackgroundImage = null;
            this.buttonChange.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonChange.Font = null;
            this.buttonChange.Name = "buttonChange";
            this.buttonChange.UseVisualStyleBackColor = true;
            this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.AccessibleDescription = null;
            this.buttonClose.AccessibleName = null;
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.BackgroundImage = null;
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Font = null;
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = null;
            this.groupBox1.AccessibleName = null;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackgroundImage = null;
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton4);
            this.groupBox1.Font = null;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // ChangeCaseDialog
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonChange);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeCaseDialog";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.Button buttonChange;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}