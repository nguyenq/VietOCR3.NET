using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

///
/// Creating clonable toolstrip menu items:
/// http://blogs.msdn.com/jfoscoding/articles/475177.aspx
///
namespace Vietpad.NET.Controls
{
    public class ClonableToolStripMenuItem : ToolStripMenuItem
    {
        public ClonableToolStripMenuItem()
        {
        }
        public ClonableToolStripMenuItem(string text):base(text)
        {
        }
        internal ToolStripMenuItem Clone()
        {

            // dirt simple clone - just properties, no subitems

            ClonableToolStripMenuItem menuItem = new ClonableToolStripMenuItem();
            menuItem.Events.AddHandlers(this.Events);

            menuItem.AccessibleName = this.AccessibleName;
            menuItem.AccessibleRole = this.AccessibleRole;
            menuItem.Alignment = this.Alignment;
            menuItem.AllowDrop = this.AllowDrop;
            menuItem.Anchor = this.Anchor;
            menuItem.AutoSize = this.AutoSize;
            menuItem.AutoToolTip = this.AutoToolTip;
            menuItem.BackColor = this.BackColor;
            menuItem.BackgroundImage = this.BackgroundImage;
            menuItem.BackgroundImageLayout = this.BackgroundImageLayout;
            menuItem.Checked = this.Checked;
            menuItem.CheckOnClick = this.CheckOnClick;
            menuItem.CheckState = this.CheckState;
            menuItem.DisplayStyle = this.DisplayStyle;
            menuItem.Dock = this.Dock;
            menuItem.DoubleClickEnabled = this.DoubleClickEnabled;
            menuItem.Enabled = this.Enabled;
            menuItem.Font = this.Font;
            menuItem.ForeColor = this.ForeColor;
            menuItem.Image = this.Image;
            menuItem.ImageAlign = this.ImageAlign;
            menuItem.ImageScaling = this.ImageScaling;
            menuItem.ImageTransparentColor = this.ImageTransparentColor;
            menuItem.Margin = this.Margin;
            menuItem.MergeAction = this.MergeAction;
            menuItem.MergeIndex = this.MergeIndex;
            menuItem.Name = this.Name;
            menuItem.Overflow = this.Overflow;
            menuItem.Padding = this.Padding;
            menuItem.RightToLeft = this.RightToLeft;

            menuItem.ShortcutKeys = this.ShortcutKeys;
            menuItem.ShowShortcutKeys = this.ShowShortcutKeys;
            menuItem.Tag = this.Tag;
            menuItem.Text = this.Text;
            menuItem.TextAlign = this.TextAlign;
            menuItem.TextDirection = this.TextDirection;
            menuItem.TextImageRelation = this.TextImageRelation;
            menuItem.ToolTipText = this.ToolTipText;

            menuItem.Available = this.Available;

            if (!AutoSize)
            {
                menuItem.Size = this.Size;
            }
            return menuItem;
        }

    }
}
