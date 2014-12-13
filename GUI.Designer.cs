using VietOCR.NET.Controls;
namespace VietOCR.NET
{
    partial class GUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.splitContainerImage = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanelThumbnail = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox1 = new VietOCR.NET.Controls.ScrollablePictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelImage = new System.Windows.Forms.Panel();
            this.panelArrow = new System.Windows.Forms.Panel();
            this.buttonCollapseExpand = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripBtnOpen = new System.Windows.Forms.ToolStripButton();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripBtnScan = new System.Windows.Forms.ToolStripButton();
            this.scanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonPasteImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnPrev = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxPageNum = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabelPageNum = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnFitImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnActualSize = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnRotateCCW = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnRotateCW = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnOCR = new System.Windows.Forms.ToolStripButton();
            this.oCRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonCancelOCR = new System.Windows.Forms.ToolStripButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new VietOCR.NET.Controls.TextBoxContextMenuStrip(this.components);
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSpellCheck = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPostProcess = new System.Windows.Forms.ToolStripButton();
            this.postprocessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonRemoveLineBreaks = new System.Windows.Forms.ToolStripButton();
            this.removeLineBreaksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnSave = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripBtnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripCbLang = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabelLanguage = new System.Windows.Forms.ToolStripLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.recentFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oCRAllPagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.bulkOCRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripSeparator();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metadataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brightenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contrastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grayscaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monochromeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sharpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smoothToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deskewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autocropToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripSeparator();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.screenshotModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wordWrapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.changeCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vietInputMethodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemInputMethod = new System.Windows.Forms.ToolStripSeparator();
            this.uiLanguageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.downloadLangDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.psmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeTiffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitTiffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripSeparator();
            this.mergePdfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitPdfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelDim = new VietOCR.NET.Controls.NonblinkingToolStripStatusLabel(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStripStatusLabelDimValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelSpring = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelSM = new VietOCR.NET.Controls.NonblinkingToolStripStatusLabel(this.components);
            this.toolStripStatusLabelSMvalue = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelPSM = new VietOCR.NET.Controls.NonblinkingToolStripStatusLabel(this.components);
            this.toolStripStatusLabelPSMvalue = new System.Windows.Forms.ToolStripStatusLabel();
            this.backgroundWorkerLoad = new System.ComponentModel.BackgroundWorker();
            this.splitContainerImage.Panel1.SuspendLayout();
            this.splitContainerImage.Panel2.SuspendLayout();
            this.splitContainerImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelImage.SuspendLayout();
            this.panelArrow.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerImage
            // 
            this.splitContainerImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.splitContainerImage, "splitContainerImage");
            this.splitContainerImage.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerImage.Name = "splitContainerImage";
            // 
            // splitContainerImage.Panel1
            // 
            resources.ApplyResources(this.splitContainerImage.Panel1, "splitContainerImage.Panel1");
            this.splitContainerImage.Panel1.Controls.Add(this.flowLayoutPanelThumbnail);
            this.splitContainerImage.Panel1Collapsed = true;
            // 
            // splitContainerImage.Panel2
            // 
            this.splitContainerImage.Panel2.AllowDrop = true;
            resources.ApplyResources(this.splitContainerImage.Panel2, "splitContainerImage.Panel2");
            this.splitContainerImage.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainerImage.Panel2.DragDrop += new System.Windows.Forms.DragEventHandler(this.splitContainer2_Panel2_DragDrop);
            this.splitContainerImage.Panel2.DragOver += new System.Windows.Forms.DragEventHandler(this.splitContainer2_Panel2_DragOver);
            this.splitContainerImage.TabStop = false;
            this.splitContainerImage.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainerImage_SplitterMoved);
            // 
            // flowLayoutPanelThumbnail
            // 
            resources.ApplyResources(this.flowLayoutPanelThumbnail, "flowLayoutPanelThumbnail");
            this.flowLayoutPanelThumbnail.Name = "flowLayoutPanelThumbnail";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelImage);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer1.TabStop = false;
            // 
            // panelImage
            // 
            this.panelImage.Controls.Add(this.splitContainerImage);
            this.panelImage.Controls.Add(this.panelArrow);
            resources.ApplyResources(this.panelImage, "panelImage");
            this.panelImage.Name = "panelImage";
            // 
            // panelArrow
            // 
            this.panelArrow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelArrow.Controls.Add(this.buttonCollapseExpand);
            resources.ApplyResources(this.panelArrow, "panelArrow");
            this.panelArrow.Name = "panelArrow";
            // 
            // buttonCollapseExpand
            // 
            this.buttonCollapseExpand.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.buttonCollapseExpand, "buttonCollapseExpand");
            this.buttonCollapseExpand.Name = "buttonCollapseExpand";
            this.toolTip1.SetToolTip(this.buttonCollapseExpand, resources.GetString("buttonCollapseExpand.ToolTip"));
            this.buttonCollapseExpand.UseVisualStyleBackColor = true;
            this.buttonCollapseExpand.Click += new System.EventHandler(this.buttonCollapseExpand_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtnOpen,
            this.toolStripBtnScan,
            this.toolStripButtonPasteImage,
            this.toolStripSeparator4,
            this.toolStripBtnPrev,
            this.toolStripBtnNext,
            this.toolStripComboBoxPageNum,
            this.toolStripLabelPageNum,
            this.toolStripSeparator1,
            this.toolStripBtnFitImage,
            this.toolStripBtnActualSize,
            this.toolStripSeparator2,
            this.toolStripBtnZoomIn,
            this.toolStripBtnZoomOut,
            this.toolStripSeparator3,
            this.toolStripBtnRotateCCW,
            this.toolStripBtnRotateCW,
            this.toolStripSeparator6,
            this.toolStripBtnOCR,
            this.toolStripButtonCancelOCR});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // toolStripBtnOpen
            // 
            this.toolStripBtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripBtnOpen, "toolStripBtnOpen");
            this.toolStripBtnOpen.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripBtnOpen.Name = "toolStripBtnOpen";
            this.toolStripBtnOpen.Tag = this.openToolStripMenuItem;
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripBtnScan
            // 
            this.toolStripBtnScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripBtnScan, "toolStripBtnScan");
            this.toolStripBtnScan.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripBtnScan.Name = "toolStripBtnScan";
            this.toolStripBtnScan.Tag = this.scanToolStripMenuItem;
            // 
            // scanToolStripMenuItem
            // 
            this.scanToolStripMenuItem.Name = "scanToolStripMenuItem";
            resources.ApplyResources(this.scanToolStripMenuItem, "scanToolStripMenuItem");
            this.scanToolStripMenuItem.Click += new System.EventHandler(this.scanToolStripMenuItem_Click);
            // 
            // toolStripButtonPasteImage
            // 
            this.toolStripButtonPasteImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonPasteImage, "toolStripButtonPasteImage");
            this.toolStripButtonPasteImage.Name = "toolStripButtonPasteImage";
            this.toolStripButtonPasteImage.Click += new System.EventHandler(this.toolStripButtonPasteImage_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // toolStripBtnPrev
            // 
            this.toolStripBtnPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripBtnPrev, "toolStripBtnPrev");
            this.toolStripBtnPrev.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripBtnPrev.Name = "toolStripBtnPrev";
            this.toolStripBtnPrev.Click += new System.EventHandler(this.toolStripBtnPrev_Click);
            // 
            // toolStripBtnNext
            // 
            this.toolStripBtnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripBtnNext, "toolStripBtnNext");
            this.toolStripBtnNext.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripBtnNext.Name = "toolStripBtnNext";
            this.toolStripBtnNext.Click += new System.EventHandler(this.toolStripBtnNext_Click);
            // 
            // toolStripComboBoxPageNum
            // 
            resources.ApplyResources(this.toolStripComboBoxPageNum, "toolStripComboBoxPageNum");
            this.toolStripComboBoxPageNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxPageNum.DropDownWidth = 40;
            this.toolStripComboBoxPageNum.Name = "toolStripComboBoxPageNum";
            this.toolStripComboBoxPageNum.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxPageNum_SelectedIndexChanged);
            // 
            // toolStripLabelPageNum
            // 
            resources.ApplyResources(this.toolStripLabelPageNum, "toolStripLabelPageNum");
            this.toolStripLabelPageNum.Name = "toolStripLabelPageNum";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripBtnFitImage
            // 
            this.toolStripBtnFitImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripBtnFitImage, "toolStripBtnFitImage");
            this.toolStripBtnFitImage.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripBtnFitImage.Name = "toolStripBtnFitImage";
            this.toolStripBtnFitImage.Click += new System.EventHandler(this.toolStripBtnFitImage_Click);
            // 
            // toolStripBtnActualSize
            // 
            this.toolStripBtnActualSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripBtnActualSize, "toolStripBtnActualSize");
            this.toolStripBtnActualSize.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripBtnActualSize.Name = "toolStripBtnActualSize";
            this.toolStripBtnActualSize.Click += new System.EventHandler(this.toolStripBtnActualSize_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolStripBtnZoomIn
            // 
            this.toolStripBtnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripBtnZoomIn, "toolStripBtnZoomIn");
            this.toolStripBtnZoomIn.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripBtnZoomIn.Name = "toolStripBtnZoomIn";
            this.toolStripBtnZoomIn.Click += new System.EventHandler(this.toolStripBtnZoomIn_Click);
            // 
            // toolStripBtnZoomOut
            // 
            this.toolStripBtnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripBtnZoomOut, "toolStripBtnZoomOut");
            this.toolStripBtnZoomOut.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripBtnZoomOut.Name = "toolStripBtnZoomOut";
            this.toolStripBtnZoomOut.Click += new System.EventHandler(this.toolStripBtnZoomOut_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // toolStripBtnRotateCCW
            // 
            this.toolStripBtnRotateCCW.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripBtnRotateCCW, "toolStripBtnRotateCCW");
            this.toolStripBtnRotateCCW.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripBtnRotateCCW.Name = "toolStripBtnRotateCCW";
            this.toolStripBtnRotateCCW.Click += new System.EventHandler(this.toolStripBtnRotateCCW_Click);
            // 
            // toolStripBtnRotateCW
            // 
            this.toolStripBtnRotateCW.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripBtnRotateCW, "toolStripBtnRotateCW");
            this.toolStripBtnRotateCW.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripBtnRotateCW.Name = "toolStripBtnRotateCW";
            this.toolStripBtnRotateCW.Click += new System.EventHandler(this.toolStripBtnRotateCW_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // toolStripBtnOCR
            // 
            this.toolStripBtnOCR.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripBtnOCR, "toolStripBtnOCR");
            this.toolStripBtnOCR.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripBtnOCR.Name = "toolStripBtnOCR";
            this.toolStripBtnOCR.Tag = this.oCRToolStripMenuItem;
            // 
            // oCRToolStripMenuItem
            // 
            this.oCRToolStripMenuItem.Name = "oCRToolStripMenuItem";
            resources.ApplyResources(this.oCRToolStripMenuItem, "oCRToolStripMenuItem");
            this.oCRToolStripMenuItem.Click += new System.EventHandler(this.ocrToolStripMenuItem_Click);
            // 
            // toolStripButtonCancelOCR
            // 
            this.toolStripButtonCancelOCR.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonCancelOCR, "toolStripButtonCancelOCR");
            this.toolStripButtonCancelOCR.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripButtonCancelOCR.Name = "toolStripButtonCancelOCR";
            this.toolStripButtonCancelOCR.Click += new System.EventHandler(this.toolStripButtonCancelOCR_Click);
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.ModifiedChanged += new System.EventHandler(this.textBox1_ModifiedChanged);
            this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.splitContainer2_Panel2_DragDrop);
            this.textBox1.DragOver += new System.Windows.Forms.DragEventHandler(this.splitContainer2_Panel2_DragOver);
            this.textBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseDown);
            this.textBox1.MouseEnter += new System.EventHandler(this.textBox1_MouseEnter);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "standardTextBoxContextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSpellCheck,
            this.toolStripButtonPostProcess,
            this.toolStripButtonRemoveLineBreaks,
            this.toolStripSeparator5,
            this.toolStripBtnSave,
            this.toolStripBtnClear,
            this.toolStripSeparator7,
            this.toolStripCbLang,
            this.toolStripLabelLanguage});
            resources.ApplyResources(this.toolStrip2, "toolStrip2");
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip2_ItemClicked);
            // 
            // toolStripButtonSpellCheck
            // 
            this.toolStripButtonSpellCheck.CheckOnClick = true;
            this.toolStripButtonSpellCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonSpellCheck, "toolStripButtonSpellCheck");
            this.toolStripButtonSpellCheck.Margin = new System.Windows.Forms.Padding(0, 1, 1, 2);
            this.toolStripButtonSpellCheck.Name = "toolStripButtonSpellCheck";
            this.toolStripButtonSpellCheck.Click += new System.EventHandler(this.toolStripButtonSpellCheck_Click);
            // 
            // toolStripButtonPostProcess
            // 
            this.toolStripButtonPostProcess.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonPostProcess, "toolStripButtonPostProcess");
            this.toolStripButtonPostProcess.Name = "toolStripButtonPostProcess";
            this.toolStripButtonPostProcess.Tag = this.postprocessToolStripMenuItem;
            // 
            // postprocessToolStripMenuItem
            // 
            this.postprocessToolStripMenuItem.Name = "postprocessToolStripMenuItem";
            resources.ApplyResources(this.postprocessToolStripMenuItem, "postprocessToolStripMenuItem");
            this.postprocessToolStripMenuItem.Click += new System.EventHandler(this.postprocessToolStripMenuItem_Click);
            // 
            // toolStripButtonRemoveLineBreaks
            // 
            this.toolStripButtonRemoveLineBreaks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonRemoveLineBreaks, "toolStripButtonRemoveLineBreaks");
            this.toolStripButtonRemoveLineBreaks.Name = "toolStripButtonRemoveLineBreaks";
            this.toolStripButtonRemoveLineBreaks.Tag = this.removeLineBreaksToolStripMenuItem;
            // 
            // removeLineBreaksToolStripMenuItem
            // 
            this.removeLineBreaksToolStripMenuItem.Name = "removeLineBreaksToolStripMenuItem";
            resources.ApplyResources(this.removeLineBreaksToolStripMenuItem, "removeLineBreaksToolStripMenuItem");
            this.removeLineBreaksToolStripMenuItem.Click += new System.EventHandler(this.removeLineBreaksToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // toolStripBtnSave
            // 
            this.toolStripBtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripBtnSave, "toolStripBtnSave");
            this.toolStripBtnSave.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripBtnSave.Name = "toolStripBtnSave";
            this.toolStripBtnSave.Tag = this.saveToolStripMenuItem;
            // 
            // saveToolStripMenuItem
            // 
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripBtnClear
            // 
            this.toolStripBtnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripBtnClear, "toolStripBtnClear");
            this.toolStripBtnClear.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.toolStripBtnClear.Name = "toolStripBtnClear";
            this.toolStripBtnClear.Click += new System.EventHandler(this.toolStripBtnClear_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            // 
            // toolStripCbLang
            // 
            this.toolStripCbLang.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            resources.ApplyResources(this.toolStripCbLang, "toolStripCbLang");
            this.toolStripCbLang.Name = "toolStripCbLang";
            this.toolStripCbLang.SelectedIndexChanged += new System.EventHandler(this.toolStripCbLang_SelectedIndexChanged);
            this.toolStripCbLang.TextUpdate += new System.EventHandler(this.toolStripCbLang_TextUpdate);
            // 
            // toolStripLabelLanguage
            // 
            this.toolStripLabelLanguage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabelLanguage.Name = "toolStripLabelLanguage";
            resources.ApplyResources(this.toolStripLabelLanguage, "toolStripLabelLanguage");
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.commandToolStripMenuItem,
            this.imageToolStripMenuItem,
            this.formatToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.scanToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem4,
            this.recentFilesToolStripMenuItem,
            this.toolStripMenuItem1,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            resources.ApplyResources(this.saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            // 
            // recentFilesToolStripMenuItem
            // 
            this.recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
            resources.ApplyResources(this.recentFilesToolStripMenuItem, "recentFilesToolStripMenuItem");
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            resources.ApplyResources(this.quitToolStripMenuItem, "quitToolStripMenuItem");
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // commandToolStripMenuItem
            // 
            this.commandToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oCRToolStripMenuItem,
            this.oCRAllPagesToolStripMenuItem,
            this.toolStripMenuItem2,
            this.bulkOCRToolStripMenuItem,
            this.toolStripMenuItem11,
            this.postprocessToolStripMenuItem});
            this.commandToolStripMenuItem.Name = "commandToolStripMenuItem";
            resources.ApplyResources(this.commandToolStripMenuItem, "commandToolStripMenuItem");
            // 
            // oCRAllPagesToolStripMenuItem
            // 
            this.oCRAllPagesToolStripMenuItem.Name = "oCRAllPagesToolStripMenuItem";
            resources.ApplyResources(this.oCRAllPagesToolStripMenuItem, "oCRAllPagesToolStripMenuItem");
            this.oCRAllPagesToolStripMenuItem.Click += new System.EventHandler(this.ocrAllPagesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // bulkOCRToolStripMenuItem
            // 
            this.bulkOCRToolStripMenuItem.Name = "bulkOCRToolStripMenuItem";
            resources.ApplyResources(this.bulkOCRToolStripMenuItem, "bulkOCRToolStripMenuItem");
            this.bulkOCRToolStripMenuItem.Click += new System.EventHandler(this.bulkOCRToolStripMenuItem_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            resources.ApplyResources(this.toolStripMenuItem11, "toolStripMenuItem11");
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.metadataToolStripMenuItem,
            this.toolStripMenuItem6,
            this.filterToolStripMenuItem,
            this.deskewToolStripMenuItem,
            this.autocropToolStripMenuItem,
            this.toolStripMenuItem12,
            this.undoToolStripMenuItem,
            this.toolStripMenuItem8,
            this.screenshotModeToolStripMenuItem});
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            resources.ApplyResources(this.imageToolStripMenuItem, "imageToolStripMenuItem");
            // 
            // metadataToolStripMenuItem
            // 
            this.metadataToolStripMenuItem.Name = "metadataToolStripMenuItem";
            resources.ApplyResources(this.metadataToolStripMenuItem, "metadataToolStripMenuItem");
            this.metadataToolStripMenuItem.Click += new System.EventHandler(this.metadataToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            resources.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.brightenToolStripMenuItem,
            this.contrastToolStripMenuItem,
            this.grayscaleToolStripMenuItem,
            this.monochromeToolStripMenuItem,
            this.invertToolStripMenuItem,
            this.sharpenToolStripMenuItem,
            this.smoothToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            resources.ApplyResources(this.filterToolStripMenuItem, "filterToolStripMenuItem");
            // 
            // brightenToolStripMenuItem
            // 
            this.brightenToolStripMenuItem.Name = "brightenToolStripMenuItem";
            resources.ApplyResources(this.brightenToolStripMenuItem, "brightenToolStripMenuItem");
            this.brightenToolStripMenuItem.Click += new System.EventHandler(this.brightenToolStripMenuItem_Click);
            // 
            // contrastToolStripMenuItem
            // 
            this.contrastToolStripMenuItem.Name = "contrastToolStripMenuItem";
            resources.ApplyResources(this.contrastToolStripMenuItem, "contrastToolStripMenuItem");
            this.contrastToolStripMenuItem.Click += new System.EventHandler(this.contrastToolStripMenuItem_Click);
            // 
            // grayscaleToolStripMenuItem
            // 
            this.grayscaleToolStripMenuItem.Name = "grayscaleToolStripMenuItem";
            resources.ApplyResources(this.grayscaleToolStripMenuItem, "grayscaleToolStripMenuItem");
            this.grayscaleToolStripMenuItem.Click += new System.EventHandler(this.grayscaleToolStripMenuItem_Click);
            // 
            // monochromeToolStripMenuItem
            // 
            this.monochromeToolStripMenuItem.Name = "monochromeToolStripMenuItem";
            resources.ApplyResources(this.monochromeToolStripMenuItem, "monochromeToolStripMenuItem");
            this.monochromeToolStripMenuItem.Click += new System.EventHandler(this.monochromeToolStripMenuItem_Click);
            // 
            // invertToolStripMenuItem
            // 
            this.invertToolStripMenuItem.Name = "invertToolStripMenuItem";
            resources.ApplyResources(this.invertToolStripMenuItem, "invertToolStripMenuItem");
            this.invertToolStripMenuItem.Click += new System.EventHandler(this.invertToolStripMenuItem_Click);
            // 
            // sharpenToolStripMenuItem
            // 
            this.sharpenToolStripMenuItem.Name = "sharpenToolStripMenuItem";
            resources.ApplyResources(this.sharpenToolStripMenuItem, "sharpenToolStripMenuItem");
            this.sharpenToolStripMenuItem.Click += new System.EventHandler(this.sharpenToolStripMenuItem_Click);
            // 
            // smoothToolStripMenuItem
            // 
            this.smoothToolStripMenuItem.Name = "smoothToolStripMenuItem";
            resources.ApplyResources(this.smoothToolStripMenuItem, "smoothToolStripMenuItem");
            this.smoothToolStripMenuItem.Click += new System.EventHandler(this.smoothToolStripMenuItem_Click);
            // 
            // deskewToolStripMenuItem
            // 
            this.deskewToolStripMenuItem.Name = "deskewToolStripMenuItem";
            resources.ApplyResources(this.deskewToolStripMenuItem, "deskewToolStripMenuItem");
            this.deskewToolStripMenuItem.Click += new System.EventHandler(this.deskewToolStripMenuItem_Click);
            // 
            // autocropToolStripMenuItem
            // 
            this.autocropToolStripMenuItem.Name = "autocropToolStripMenuItem";
            resources.ApplyResources(this.autocropToolStripMenuItem, "autocropToolStripMenuItem");
            this.autocropToolStripMenuItem.Click += new System.EventHandler(this.autocropToolStripMenuItem_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            resources.ApplyResources(this.toolStripMenuItem12, "toolStripMenuItem12");
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            resources.ApplyResources(this.undoToolStripMenuItem, "undoToolStripMenuItem");
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            resources.ApplyResources(this.toolStripMenuItem8, "toolStripMenuItem8");
            // 
            // screenshotModeToolStripMenuItem
            // 
            this.screenshotModeToolStripMenuItem.Name = "screenshotModeToolStripMenuItem";
            resources.ApplyResources(this.screenshotModeToolStripMenuItem, "screenshotModeToolStripMenuItem");
            this.screenshotModeToolStripMenuItem.Click += new System.EventHandler(this.screenshotModeToolStripMenuItem_Click);
            // 
            // formatToolStripMenuItem
            // 
            this.formatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wordWrapToolStripMenuItem,
            this.fontToolStripMenuItem,
            this.toolStripMenuItem5,
            this.changeCaseToolStripMenuItem,
            this.removeLineBreaksToolStripMenuItem});
            this.formatToolStripMenuItem.Name = "formatToolStripMenuItem";
            resources.ApplyResources(this.formatToolStripMenuItem, "formatToolStripMenuItem");
            this.formatToolStripMenuItem.DropDownOpening += new System.EventHandler(this.formatToolStripMenuItem_DropDownOpening);
            // 
            // wordWrapToolStripMenuItem
            // 
            this.wordWrapToolStripMenuItem.Name = "wordWrapToolStripMenuItem";
            resources.ApplyResources(this.wordWrapToolStripMenuItem, "wordWrapToolStripMenuItem");
            this.wordWrapToolStripMenuItem.Click += new System.EventHandler(this.wordWrapToolStripMenuItem_Click);
            // 
            // fontToolStripMenuItem
            // 
            this.fontToolStripMenuItem.Name = "fontToolStripMenuItem";
            resources.ApplyResources(this.fontToolStripMenuItem, "fontToolStripMenuItem");
            this.fontToolStripMenuItem.Click += new System.EventHandler(this.fontToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
            // 
            // changeCaseToolStripMenuItem
            // 
            this.changeCaseToolStripMenuItem.Name = "changeCaseToolStripMenuItem";
            resources.ApplyResources(this.changeCaseToolStripMenuItem, "changeCaseToolStripMenuItem");
            this.changeCaseToolStripMenuItem.Click += new System.EventHandler(this.changeCaseToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vietInputMethodToolStripMenuItem,
            this.toolStripMenuItemInputMethod,
            this.uiLanguageToolStripMenuItem,
            this.toolStripMenuItem9,
            this.downloadLangDataToolStripMenuItem,
            this.toolStripMenuItem10,
            this.psmToolStripMenuItem,
            this.toolStripMenuItem7,
            this.optionsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
            // 
            // vietInputMethodToolStripMenuItem
            // 
            this.vietInputMethodToolStripMenuItem.Name = "vietInputMethodToolStripMenuItem";
            resources.ApplyResources(this.vietInputMethodToolStripMenuItem, "vietInputMethodToolStripMenuItem");
            // 
            // toolStripMenuItemInputMethod
            // 
            this.toolStripMenuItemInputMethod.Name = "toolStripMenuItemInputMethod";
            resources.ApplyResources(this.toolStripMenuItemInputMethod, "toolStripMenuItemInputMethod");
            // 
            // uiLanguageToolStripMenuItem
            // 
            this.uiLanguageToolStripMenuItem.Name = "uiLanguageToolStripMenuItem";
            resources.ApplyResources(this.uiLanguageToolStripMenuItem, "uiLanguageToolStripMenuItem");
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            resources.ApplyResources(this.toolStripMenuItem9, "toolStripMenuItem9");
            // 
            // downloadLangDataToolStripMenuItem
            // 
            this.downloadLangDataToolStripMenuItem.Name = "downloadLangDataToolStripMenuItem";
            resources.ApplyResources(this.downloadLangDataToolStripMenuItem, "downloadLangDataToolStripMenuItem");
            this.downloadLangDataToolStripMenuItem.Click += new System.EventHandler(this.downloadLangDataToolStripMenuItem_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            resources.ApplyResources(this.toolStripMenuItem10, "toolStripMenuItem10");
            // 
            // psmToolStripMenuItem
            // 
            this.psmToolStripMenuItem.Name = "psmToolStripMenuItem";
            resources.ApplyResources(this.psmToolStripMenuItem, "psmToolStripMenuItem");
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            resources.ApplyResources(this.toolStripMenuItem7, "toolStripMenuItem7");
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mergeTiffToolStripMenuItem,
            this.splitTiffToolStripMenuItem,
            this.toolStripMenuItem13,
            this.mergePdfToolStripMenuItem,
            this.splitPdfToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
            // 
            // mergeTiffToolStripMenuItem
            // 
            this.mergeTiffToolStripMenuItem.Name = "mergeTiffToolStripMenuItem";
            resources.ApplyResources(this.mergeTiffToolStripMenuItem, "mergeTiffToolStripMenuItem");
            this.mergeTiffToolStripMenuItem.Click += new System.EventHandler(this.mergeTiffToolStripMenuItem_Click);
            // 
            // splitTiffToolStripMenuItem
            // 
            this.splitTiffToolStripMenuItem.Name = "splitTiffToolStripMenuItem";
            resources.ApplyResources(this.splitTiffToolStripMenuItem, "splitTiffToolStripMenuItem");
            this.splitTiffToolStripMenuItem.Click += new System.EventHandler(this.splitTiffToolStripMenuItem_Click);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            resources.ApplyResources(this.toolStripMenuItem13, "toolStripMenuItem13");
            // 
            // mergePdfToolStripMenuItem
            // 
            this.mergePdfToolStripMenuItem.Name = "mergePdfToolStripMenuItem";
            resources.ApplyResources(this.mergePdfToolStripMenuItem, "mergePdfToolStripMenuItem");
            this.mergePdfToolStripMenuItem.Click += new System.EventHandler(this.mergePdfToolStripMenuItem_Click);
            // 
            // splitPdfToolStripMenuItem
            // 
            this.splitPdfToolStripMenuItem.Name = "splitPdfToolStripMenuItem";
            resources.ApplyResources(this.splitPdfToolStripMenuItem, "splitPdfToolStripMenuItem");
            this.splitPdfToolStripMenuItem.Click += new System.EventHandler(this.splitPdfToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.toolStripMenuItem3,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            resources.ApplyResources(this.helpToolStripMenuItem1, "helpToolStripMenuItem1");
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem2_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabelDim,
            this.toolStripStatusLabelDimValue,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabelSpring,
            this.toolStripStatusLabelSM,
            this.toolStripStatusLabelSMvalue,
            this.toolStripStatusLabelPSM,
            this.toolStripStatusLabelPSMvalue});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.ShowItemToolTips = true;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            resources.ApplyResources(this.toolStripProgressBar1, "toolStripProgressBar1");
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            resources.ApplyResources(this.toolStripStatusLabel2, "toolStripStatusLabel2");
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripStatusLabelDim
            // 
            this.toolStripStatusLabelDim.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabelDim.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabelDim.Name = "toolStripStatusLabelDim";
            resources.ApplyResources(this.toolStripStatusLabelDim, "toolStripStatusLabelDim");
            this.toolStripStatusLabelDim.ToolTip = this.toolTip1;
            // 
            // toolStripStatusLabelDimValue
            // 
            this.toolStripStatusLabelDimValue.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelDimValue.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabelDimValue.Name = "toolStripStatusLabelDimValue";
            resources.ApplyResources(this.toolStripStatusLabelDimValue, "toolStripStatusLabelDimValue");
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            resources.ApplyResources(this.toolStripStatusLabel3, "toolStripStatusLabel3");
            this.toolStripStatusLabel3.Spring = true;
            // 
            // toolStripStatusLabelSpring
            // 
            this.toolStripStatusLabelSpring.Name = "toolStripStatusLabelSpring";
            resources.ApplyResources(this.toolStripStatusLabelSpring, "toolStripStatusLabelSpring");
            this.toolStripStatusLabelSpring.Spring = true;
            // 
            // toolStripStatusLabelSM
            // 
            this.toolStripStatusLabelSM.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabelSM.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabelSM.Name = "toolStripStatusLabelSM";
            resources.ApplyResources(this.toolStripStatusLabelSM, "toolStripStatusLabelSM");
            this.toolStripStatusLabelSM.ToolTip = this.toolTip1;
            // 
            // toolStripStatusLabelSMvalue
            // 
            this.toolStripStatusLabelSMvalue.Name = "toolStripStatusLabelSMvalue";
            resources.ApplyResources(this.toolStripStatusLabelSMvalue, "toolStripStatusLabelSMvalue");
            // 
            // toolStripStatusLabelPSM
            // 
            this.toolStripStatusLabelPSM.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabelPSM.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabelPSM.Name = "toolStripStatusLabelPSM";
            resources.ApplyResources(this.toolStripStatusLabelPSM, "toolStripStatusLabelPSM");
            this.toolStripStatusLabelPSM.ToolTip = this.toolTip1;
            // 
            // toolStripStatusLabelPSMvalue
            // 
            this.toolStripStatusLabelPSMvalue.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelPSMvalue.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabelPSMvalue.Name = "toolStripStatusLabelPSMvalue";
            resources.ApplyResources(this.toolStripStatusLabelPSMvalue, "toolStripStatusLabelPSMvalue");
            // 
            // backgroundWorkerLoad
            // 
            this.backgroundWorkerLoad.WorkerReportsProgress = true;
            this.backgroundWorkerLoad.WorkerSupportsCancellation = true;
            this.backgroundWorkerLoad.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerLoad_DoWork);
            this.backgroundWorkerLoad.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerLoad_RunWorkerCompleted);
            // 
            // GUI
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GUI";
            this.Activated += new System.EventHandler(this.GUI_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GUI_KeyDown);
            this.splitContainerImage.Panel1.ResumeLayout(false);
            this.splitContainerImage.Panel2.ResumeLayout(false);
            this.splitContainerImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.panelImage.ResumeLayout(false);
            this.panelArrow.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripBtnOpen;
        protected System.Windows.Forms.ToolStripButton toolStripBtnOCR;
        private System.Windows.Forms.ToolStripButton toolStripBtnClear;
        private System.Windows.Forms.SplitContainer splitContainer1;
        protected System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem commandToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem oCRToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem oCRAllPagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        protected System.Windows.Forms.ToolStripMenuItem postprocessToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabelLanguage;
        protected System.Windows.Forms.ToolStripComboBox toolStripCbLang;
        protected System.Windows.Forms.ToolStripButton toolStripBtnPrev;
        protected System.Windows.Forms.ToolStripButton toolStripBtnNext;
        protected System.Windows.Forms.ToolStripButton toolStripBtnFitImage;
        protected System.Windows.Forms.ToolStripButton toolStripBtnActualSize;
        protected System.Windows.Forms.ToolStripButton toolStripBtnZoomIn;
        protected System.Windows.Forms.ToolStripButton toolStripBtnZoomOut;
        protected System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        protected System.Windows.Forms.SplitContainer splitContainerImage;
        private System.Windows.Forms.Panel panelImage;
        protected System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        protected VietOCR.NET.Controls.ScrollablePictureBox pictureBox1;
        private System.Windows.Forms.ToolStripButton toolStripBtnRotateCCW;
        private System.Windows.Forms.ToolStripButton toolStripBtnRotateCW;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wordWrapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fontToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem changeCaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeLineBreaksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeTiffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem splitPdfToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorkerLoad;
        protected System.Windows.Forms.ToolStripButton toolStripButtonCancelOCR;
        private System.Windows.Forms.ToolStripMenuItem mergePdfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem recentFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripBtnSave;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem metadataToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        protected System.Windows.Forms.ToolStripMenuItem screenshotModeToolStripMenuItem;
        protected System.Windows.Forms.ToolStripButton toolStripButtonSpellCheck;
        protected VietOCR.NET.Controls.TextBoxContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem downloadLangDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        protected System.Windows.Forms.ToolStripMenuItem scanToolStripMenuItem;
        protected System.Windows.Forms.ToolStripButton toolStripBtnScan;
        protected System.Windows.Forms.ToolStripMenuItem vietInputMethodToolStripMenuItem;
        protected System.Windows.Forms.ToolStripSeparator toolStripMenuItemInputMethod;
        protected System.Windows.Forms.ToolStripMenuItem uiLanguageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem deskewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        protected System.Windows.Forms.ToolStripMenuItem psmToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem11;
        protected System.Windows.Forms.ToolStripMenuItem bulkOCRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem brightenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contrastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grayscaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monochromeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sharpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smoothToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autocropToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem splitTiffToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem13;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabelPageNum;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.Panel panelArrow;
        private System.Windows.Forms.Button buttonCollapseExpand;
        protected System.Windows.Forms.FlowLayoutPanel flowLayoutPanelThumbnail;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSpring;
        protected System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPSMvalue;
        protected System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSMvalue;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDimValue;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private NonblinkingToolStripStatusLabel toolStripStatusLabelPSM;
        private NonblinkingToolStripStatusLabel toolStripStatusLabelSM;
        private NonblinkingToolStripStatusLabel toolStripStatusLabelDim;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButtonPasteImage;
        private System.Windows.Forms.ToolStripButton toolStripButtonPostProcess;
        private System.Windows.Forms.ToolStripButton toolStripButtonRemoveLineBreaks;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxPageNum;
    }
}