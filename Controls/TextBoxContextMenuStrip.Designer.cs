//
// Adapted from VietPad.NET and http://bytes.com/topic/c-sharp/answers/265836-custom-paste-context-menu-fvor-text-boxes
//

using System.Windows.Forms;

namespace VietOCR.NET.Controls
{
    /// <summary>
    /// 
    /// </summary>
    partial class TextBoxContextMenuStrip
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextBoxContextMenuStrip));
            this.miUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.miSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.miCut = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.miPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.miSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.miSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.SuspendLayout();
            // 
            // miUndo
            // 
            this.miUndo.AccessibleDescription = null;
            this.miUndo.AccessibleName = null;
            resources.ApplyResources(this.miUndo, "miUndo");
            this.miUndo.BackgroundImage = null;
            this.miUndo.Name = "miUndo";
            this.miUndo.ShortcutKeyDisplayString = null;
            this.miUndo.Click += new System.EventHandler(this.miUndo_Click);
            // 
            // miSeparator
            // 
            this.miSeparator.AccessibleDescription = null;
            this.miSeparator.AccessibleName = null;
            resources.ApplyResources(this.miSeparator, "miSeparator");
            this.miSeparator.Name = "miSeparator";
            // 
            // miCut
            // 
            this.miCut.AccessibleDescription = null;
            this.miCut.AccessibleName = null;
            resources.ApplyResources(this.miCut, "miCut");
            this.miCut.BackgroundImage = null;
            this.miCut.Name = "miCut";
            this.miCut.ShortcutKeyDisplayString = null;
            this.miCut.Click += new System.EventHandler(this.miCut_Click);
            // 
            // miCopy
            // 
            this.miCopy.AccessibleDescription = null;
            this.miCopy.AccessibleName = null;
            resources.ApplyResources(this.miCopy, "miCopy");
            this.miCopy.BackgroundImage = null;
            this.miCopy.Name = "miCopy";
            this.miCopy.ShortcutKeyDisplayString = null;
            this.miCopy.Click += new System.EventHandler(this.miCopy_Click);
            // 
            // miPaste
            // 
            this.miPaste.AccessibleDescription = null;
            this.miPaste.AccessibleName = null;
            resources.ApplyResources(this.miPaste, "miPaste");
            this.miPaste.BackgroundImage = null;
            this.miPaste.Name = "miPaste";
            this.miPaste.ShortcutKeyDisplayString = null;
            this.miPaste.Click += new System.EventHandler(this.miPaste_Click);
            // 
            // miDelete
            // 
            this.miDelete.AccessibleDescription = null;
            this.miDelete.AccessibleName = null;
            resources.ApplyResources(this.miDelete, "miDelete");
            this.miDelete.BackgroundImage = null;
            this.miDelete.Name = "miDelete";
            this.miDelete.ShortcutKeyDisplayString = null;
            this.miDelete.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // miSeparator2
            // 
            this.miSeparator2.AccessibleDescription = null;
            this.miSeparator2.AccessibleName = null;
            resources.ApplyResources(this.miSeparator2, "miSeparator2");
            this.miSeparator2.Name = "miSeparator2";
            // 
            // miSelectAll
            // 
            this.miSelectAll.AccessibleDescription = null;
            this.miSelectAll.AccessibleName = null;
            resources.ApplyResources(this.miSelectAll, "miSelectAll");
            this.miSelectAll.BackgroundImage = null;
            this.miSelectAll.Name = "miSelectAll";
            this.miSelectAll.ShortcutKeyDisplayString = null;
            this.miSelectAll.Click += new System.EventHandler(this.miSelectAll_Click);
            // 
            // TextBoxContextMenuStrip
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackgroundImage = null;
            this.Font = null;
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miUndo,
            this.miSeparator,
            this.miCut,
            this.miCopy,
            this.miPaste,
            this.miDelete,
            this.miSeparator2,
            this.miSelectAll});
            this.Opening += new System.ComponentModel.CancelEventHandler(this.TextBoxContextMenuStrip_Opening);
            this.ResumeLayout(false);

        }

        public void RepopulateContextMenu()
        {
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.miUndo,
                this.miSeparator,
                this.miCut,
                this.miCopy,
                this.miPaste,
                this.miDelete,
                this.miSeparator2,
                this.miSelectAll
            });
        }

        public void ChangeUILanguage()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextBoxContextMenuStrip));
            resources.ApplyResources(this.miUndo, "miUndo");
            resources.ApplyResources(this.miCut, "miCut");
            resources.ApplyResources(this.miCopy, "miCopy");
            resources.ApplyResources(this.miPaste, "miPaste");
            resources.ApplyResources(this.miDelete, "miDelete");
            resources.ApplyResources(this.miSelectAll, "miSelectAll");
        }

        void TextBoxContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Get the text box that the context menu was popped on
            if (this.SourceControl is TextBox)
            {
                TextBox clickedBox = (TextBox)this.SourceControl;

                // Enable and disable standard menu items as necessary
                bool isSelection = clickedBox.SelectionLength > 0;
                IDataObject clipObject = Clipboard.GetDataObject();
                bool textOnClipboard = clipObject.GetDataPresent(DataFormats.Text);

                this.miUndo.Enabled = clickedBox.CanUndo;
                this.miCut.Enabled = isSelection;
                this.miCopy.Enabled = isSelection;
                this.miPaste.Enabled = textOnClipboard;
                this.miDelete.Enabled = isSelection;
            }
        }

        private void miUndo_Click(object sender, System.EventArgs e)
        {
            // Get the text box that the context menu was popped on
            if (this.SourceControl is TextBox)
            {
                TextBox clickedBox = (TextBox)this.SourceControl;

                if (clickedBox.CanUndo)
                {
                    clickedBox.Undo();
                }
            }
        }

        private void miCut_Click(object sender, System.EventArgs e)
        {
            // Get the text box that the context menu was popped on
            if (this.SourceControl is TextBox)
            {
                TextBox clickedBox = (TextBox)this.SourceControl;

                if (clickedBox.SelectionLength > 0)
                {
                    clickedBox.Cut();
                }
            }
        }

        private void miCopy_Click(object sender, System.EventArgs e)
        {
            // Get the text box that the context menu was popped on
            if (this.SourceControl is TextBox)
            {
                TextBox clickedBox = (TextBox)this.SourceControl;

                if (clickedBox.SelectionLength > 0)
                {
                    clickedBox.Copy();
                }
            }
        }

        private void miPaste_Click(object sender, System.EventArgs e)
        {
            // Get the text box that the context menu was popped on
            if (this.SourceControl is TextBox)
            {
                TextBox clickedBox = (TextBox)this.SourceControl;
                clickedBox.Paste();
            }
        }

        private void miDelete_Click(object sender, System.EventArgs e)
        {
            // Get the text box that the context menu was popped on
            if (this.SourceControl is TextBox)
            {
                TextBox clickedBox = (TextBox)this.SourceControl;

                if (clickedBox.SelectionLength > 0)
                {
                    clickedBox.SelectedText = string.Empty;
                }
            }
        }

        private void miSelectAll_Click(object sender, System.EventArgs e)
        {
            // Get the text box that the context menu was popped on
            if (this.SourceControl is TextBox)
            {
                TextBox clickedBox = (TextBox)this.SourceControl;
                clickedBox.SelectAll();
            }
        }
        #endregion

        private System.Windows.Forms.ToolStripMenuItem miUndo;
        private System.Windows.Forms.ToolStripMenuItem miCut;
        private System.Windows.Forms.ToolStripMenuItem miCopy;
        private System.Windows.Forms.ToolStripMenuItem miPaste;
        private System.Windows.Forms.ToolStripMenuItem miDelete;
        private System.Windows.Forms.ToolStripMenuItem miSelectAll;
        private System.Windows.Forms.ToolStripSeparator miSeparator;
        private System.Windows.Forms.ToolStripSeparator miSeparator2;
    }
}
