///
/// http://www.codeproject.com/KB/list/ListBox.aspx
///

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Security;
using System.Runtime.InteropServices;

namespace Netdev.Windows.Forms
{
    public class ListBox : System.Windows.Forms.ListBox
    {
        public event EventHandler<IndexEventArgs> DisabledItemSelected;
        protected virtual void OnDisabledItemSelected(object sender, IndexEventArgs e)
        {
            if (DisabledItemSelected != null)
            {
                DisabledItemSelected(sender, e);
            }
        }
        public ListBox()
        {
            DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            disabledIndices = new DisabledIndexCollection(this);
        }

        private int originalHeight = 0;
        private bool fontChanged = false;

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            fontChanged = true;
            this.ItemHeight = FontHeight;
            this.Height = GetPreferredHeight();
            fontChanged = false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (!fontChanged)
                this.originalHeight = this.Height;
        }

        public void DisableItem(int index)
        {
            disabledIndices.Add(index);
        }

        public void EnableItem(int index)
        {
            disabledIndices.Remove(index);
        }

        private int GetPreferredHeight()
        {
            if (!IntegralHeight)
                return this.Height;

            int currentHeight = this.originalHeight;
            int preferredHeight = PreferredHeight;
            if (currentHeight < preferredHeight)
            {
                // Calculate how many items currentheigh can hold.
                int number = currentHeight / ItemHeight;

                if (number < Items.Count)
                {
                    preferredHeight = number * ItemHeight;
                    int delta = currentHeight - preferredHeight;
                    if (ItemHeight / 2 <= delta)
                    {
                        preferredHeight += ItemHeight;
                    }
                    preferredHeight += (SystemInformation.BorderSize.Height * 4) + 3;
                }
                else
                {
                    preferredHeight = currentHeight;
                }
            }
            else
                preferredHeight = currentHeight;

            return preferredHeight;
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            int currentSelectedIndex = SelectedIndex;
            List<int> selectedDisabledIndices = new List<int>();

            for (int i = 0; i < SelectedIndices.Count; i++)
            {
                if (disabledIndices.Contains(SelectedIndices[i]))
                {
                    selectedDisabledIndices.Add(SelectedIndices[i]);
                    SelectedIndices.Remove(SelectedIndices[i]);
                }
            }
            foreach (int index in selectedDisabledIndices)
            {
                IndexEventArgs args = new IndexEventArgs(index);
                OnDisabledItemSelected(this, args);
            }
            if (currentSelectedIndex == SelectedIndex)
                base.OnSelectedIndexChanged(e);
        }

        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            if (DesignMode && Items.Count == 0)
            {
                return;
            }

            if (e.Index != ListBox.NoMatches)
            {
                object item = this.Items[e.Index];
                if (disabledIndices.Contains(e.Index))
                {
                    e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds); // SystemBrushes.InactiveBorder
                    if (item != null)
                    {
                        
                        e.Graphics.DrawString(item.ToString(), e.Font, SystemBrushes.GrayText, e.Bounds);
                    }
                }
                else
                {
                    if (SelectionMode == System.Windows.Forms.SelectionMode.None)
                    {
                        e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
                        if (item != null)
                        {
                            e.Graphics.DrawString(item.ToString(), e.Font, SystemBrushes.WindowText, e.Bounds);
                        }
                    }
                    else
                    {
                        if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                        {
                            e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                            e.DrawFocusRectangle();
                            if (item != null)
                            {
                                e.Graphics.DrawString(item.ToString(), e.Font, SystemBrushes.HighlightText, e.Bounds);
                            }
                        }
                        else
                        {
                            e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
                            if (item != null)
                            {
                                e.Graphics.DrawString(item.ToString(), e.Font, SystemBrushes.WindowText, e.Bounds);
                            }
                        }
                    }
                }
            }
        }

        private DisabledIndexCollection disabledIndices;

        public DisabledIndexCollection DisabledIndices
        {
            get { return disabledIndices; }
        }

        public class DisabledIndexCollection : IList, ICollection, IEnumerable
        {
            // Fields
            private ListBox owner;
            private List<int> innerList = new List<int>();

            // Methods
            public DisabledIndexCollection(ListBox owner)
            {
                this.owner = owner;
            }

            public void Add(int index)
            {
                if (((this.owner != null) && (this.owner.Items != null)) && ((index != -1) && !this.Contains(index)))
                {
                    innerList.Add(index);
                    this.owner.SetSelected(index, false);
                }
            }

            public void Clear()
            {
                if (this.owner != null)
                {
                    innerList.Clear();
                }
            }

            public bool Contains(int selectedIndex)
            {
                return (this.IndexOf(selectedIndex) != -1);
            }

            public void CopyTo(Array destination, int index)
            {
                int count = this.Count;
                for (int i = 0; i < count; i++)
                {
                    destination.SetValue(this[i], (int)(i + index));
                }
            }

            public IEnumerator GetEnumerator()
            {
                return new SelectedIndexEnumerator(this);
            }

            public int IndexOf(int selectedIndex)
            {
                if ((selectedIndex >= 0) && (selectedIndex < this.owner.Items.Count))
                {
                    for (int index = 0; index < innerList.Count; index++)
                    {
                        if (innerList[index] == selectedIndex)
                            return index;
                    }
                }
                return -1;
            }

            public void Remove(int index)
            {
                if (((this.owner != null) && (this.owner.Items != null)) && ((index != -1) && this.Contains(index)))
                {
                    innerList.Remove(index);
                    this.owner.SetSelected(index, false);
                }
            }

            int IList.Add(object value)
            {
                throw new NotSupportedException("ListBoxSelectedIndexCollectionIsReadOnly");
            }

            void IList.Clear()
            {
                throw new NotSupportedException("ListBoxSelectedIndexCollectionIsReadOnly");
            }

            bool IList.Contains(object selectedIndex)
            {
                return ((selectedIndex is int) && this.Contains((int)selectedIndex));
            }

            int IList.IndexOf(object selectedIndex)
            {
                if (selectedIndex is int)
                {
                    return this.IndexOf((int)selectedIndex);
                }
                return -1;
            }

            void IList.Insert(int index, object value)
            {
                throw new NotSupportedException("ListBoxSelectedIndexCollectionIsReadOnly");
            }

            void IList.Remove(object value)
            {
                throw new NotSupportedException("ListBoxSelectedIndexCollectionIsReadOnly");
            }

            void IList.RemoveAt(int index)
            {
                throw new NotSupportedException("ListBoxSelectedIndexCollectionIsReadOnly");
            }

            // Properties
            [Browsable(false)]
            public int Count
            {
                get
                {
                    return this.innerList.Count;
                }
            }

            public bool IsReadOnly
            {
                get
                {
                    return true;
                }
            }

            public int this[int index]
            {
                get
                {
                    return IndexOf(index);
                }
            }

            bool ICollection.IsSynchronized
            {
                get
                {
                    return true;
                }
            }

            object ICollection.SyncRoot
            {
                get
                {
                    return this;
                }
            }

            bool IList.IsFixedSize
            {
                get
                {
                    return true;
                }
            }

            object IList.this[int index]
            {
                get
                {
                    return this[index];
                }
                set
                {
                    throw new NotSupportedException("ListBoxSelectedIndexCollectionIsReadOnly");
                }
            }

            // Nested Types
            private class SelectedIndexEnumerator : IEnumerator
            {
                // Fields
                private int current;
                private ListBox.DisabledIndexCollection items;

                // Methods
                public SelectedIndexEnumerator(ListBox.DisabledIndexCollection items)
                {
                    this.items = items;
                    this.current = -1;
                }

                bool IEnumerator.MoveNext()
                {
                    if (this.current < (this.items.Count - 1))
                    {
                        this.current++;
                        return true;
                    }
                    this.current = this.items.Count;
                    return false;
                }

                void IEnumerator.Reset()
                {
                    this.current = -1;
                }

                // Properties
                object IEnumerator.Current
                {
                    get
                    {
                        if ((this.current == -1) || (this.current == this.items.Count))
                        {
                            throw new InvalidOperationException("ListEnumCurrentOutOfRange");
                        }
                        return this.items[this.current];
                    }
                }
            }
        }

        public new void SetSelected(int index, bool value)
        {
            int num = (this.Items == null) ? 0 : this.Items.Count;
            if ((index < 0) || (index >= num))
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (this.SelectionMode == SelectionMode.None)
            {
                throw new InvalidOperationException("ListBoxInvalidSelectionMode");
            }
            if (!disabledIndices.Contains(index))
            {
                if (!value)
                {
                    if (SelectedIndices.Contains(index))
                        SelectedIndices.Remove(index);
                }
                else
                {
                    base.SetSelected(index, value);
                }
            }
            // Selected index deoes not change, however we should redraw the disabled item.
            else
            {
                if (!value)
                {
                    // Remove selected item if it is in the list of selected indices.
                    if (SelectedIndices.Contains(index))
                        SelectedIndices.Remove(index);
                }

            }
            Invalidate(GetItemRectangle(index));
        }
    }

    public class IndexEventArgs : EventArgs
    {
        private int index;
        public int Index
        {
            get
            {
                return index;
            }

            set
            {
                index = value;
            }
        }
        public IndexEventArgs(int index)
        {
            Index = index;
        }
    }
}