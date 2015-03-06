namespace VietOCR.NET
{
    partial class ImageInfoDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageInfoDialog));
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelXRes = new System.Windows.Forms.Label();
            this.textBoxXRes = new System.Windows.Forms.TextBox();
            this.textBoxYRes = new System.Windows.Forms.TextBox();
            this.labelYRes = new System.Windows.Forms.Label();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.labelWidth = new System.Windows.Forms.Label();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.labelHeight = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.textBoxBitDepth = new System.Windows.Forms.TextBox();
            this.labelBitDepth = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // labelXRes
            // 
            resources.ApplyResources(this.labelXRes, "labelXRes");
            this.labelXRes.Name = "labelXRes";
            // 
            // textBoxXRes
            // 
            resources.ApplyResources(this.textBoxXRes, "textBoxXRes");
            this.textBoxXRes.Name = "textBoxXRes";
            this.textBoxXRes.ReadOnly = true;
            // 
            // textBoxYRes
            // 
            resources.ApplyResources(this.textBoxYRes, "textBoxYRes");
            this.textBoxYRes.Name = "textBoxYRes";
            this.textBoxYRes.ReadOnly = true;
            // 
            // labelYRes
            // 
            resources.ApplyResources(this.labelYRes, "labelYRes");
            this.labelYRes.Name = "labelYRes";
            // 
            // textBoxWidth
            // 
            resources.ApplyResources(this.textBoxWidth, "textBoxWidth");
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.ReadOnly = true;
            // 
            // labelWidth
            // 
            resources.ApplyResources(this.labelWidth, "labelWidth");
            this.labelWidth.Name = "labelWidth";
            // 
            // textBoxHeight
            // 
            resources.ApplyResources(this.textBoxHeight, "textBoxHeight");
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.ReadOnly = true;
            // 
            // labelHeight
            // 
            resources.ApplyResources(this.labelHeight, "labelHeight");
            this.labelHeight.Name = "labelHeight";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // comboBox4
            // 
            resources.ApplyResources(this.comboBox4, "comboBox4");
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            resources.GetString("comboBox4.Items"),
            resources.GetString("comboBox4.Items1"),
            resources.GetString("comboBox4.Items2")});
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.SelectedIndexChanged += new System.EventHandler(this.comboBox4_SelectedIndexChanged);
            // 
            // comboBox3
            // 
            resources.ApplyResources(this.comboBox3, "comboBox3");
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            resources.GetString("comboBox3.Items"),
            resources.GetString("comboBox3.Items1"),
            resources.GetString("comboBox3.Items2")});
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // textBoxBitDepth
            // 
            resources.ApplyResources(this.textBoxBitDepth, "textBoxBitDepth");
            this.textBoxBitDepth.Name = "textBoxBitDepth";
            this.textBoxBitDepth.ReadOnly = true;
            // 
            // labelBitDepth
            // 
            resources.ApplyResources(this.labelBitDepth, "labelBitDepth");
            this.labelBitDepth.Name = "labelBitDepth";
            // 
            // ImageInfoDialog
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxBitDepth);
            this.Controls.Add(this.labelBitDepth);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox4);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.textBoxHeight);
            this.Controls.Add(this.labelHeight);
            this.Controls.Add(this.textBoxWidth);
            this.Controls.Add(this.labelWidth);
            this.Controls.Add(this.textBoxYRes);
            this.Controls.Add(this.labelYRes);
            this.Controls.Add(this.textBoxXRes);
            this.Controls.Add(this.labelXRes);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImageInfoDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelXRes;
        private System.Windows.Forms.TextBox textBoxXRes;
        private System.Windows.Forms.TextBox textBoxYRes;
        private System.Windows.Forms.Label labelYRes;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.TextBox textBoxBitDepth;
        private System.Windows.Forms.Label labelBitDepth;
    }
}