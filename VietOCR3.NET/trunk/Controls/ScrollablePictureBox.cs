/**
 * Copyright @ 2008 Quan Nguyen
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace VietOCR.NET.Controls
{
    public partial class ScrollablePictureBox : PictureBox
    {
        Point startPoint;
        int preX, preY;
        Rectangle rect;
        bool pressOut = false;
        bool moving;
        bool isDragging;

        protected int frameWidth = 5;
        protected int minSize = 5;
        protected int startDragX, startDragY;
        protected bool resizeLeft, resizeTop, resizeRight, resizeBottom, move;
        int selX, selY, selW, selH;
        int offset;
        Point currentScrollPos;

        public ScrollablePictureBox()
        {
            InitializeComponent();

            rect = Rectangle.Empty;

            //System.Timers.Timer timer = new System.Timers.Timer();
            ////timer.SynchronizingObject = this;
            //timer.Interval = 500;
            //timer.Elapsed += new System.Timers.ElapsedEventHandler(TimerOnTick);
            //timer.Start();

            System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
            myTimer.Tick += new EventHandler(TimerOnTick);

            // Sets the timer interval to .5 seconds.
            myTimer.Interval = 500;
            myTimer.Start();

        }
        // This is the method to run when the timer is raised.
        private void TimerOnTick(Object myObject, EventArgs myEventArgs)
        {
            if (rect != Rectangle.Empty)
            {
                offset += 3;

                if (offset > 9)
                {
                    offset = 0;
                }

                // redraw only the region
                this.Invalidate(new Rectangle(rect.X, rect.Y, rect.Width + 1, rect.Height + 1));
            }
        }

        public Rectangle GetRect()
        {
            return rect;
        }

        public void Deselect()
        {
            startPoint = Point.Empty;
            rect = Rectangle.Empty;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // Calling the base class OnPaint
            base.OnPaint(pe);

            // TODO: Add custom paint code here
            if (this.Image == null) return;

            //Graphics g = (Graphics)pe.Graphics;
            //g.DrawImage(this.Image, this.ClientRectangle);

            if (rect != Rectangle.Empty)
            {
                Graphics g = (Graphics)pe.Graphics;

                // Create pen
                Pen blackPen = new Pen(Color.Black);
                //blackPen.DashStyle = DashStyle.Solid;

                List<Rectangle> squares = createSquares(rect);
                foreach (Rectangle square in squares)
                {
                    g.DrawRectangle(blackPen, square);
                }

                blackPen.DashCap = DashCap.Round;
                blackPen.LineJoin = LineJoin.Round;
                blackPen.MiterLimit = 0;
                blackPen.DashPattern = new float[] { 6, 6 };
                blackPen.DashOffset = offset;

                try
                {
                    g.DrawRectangle(blackPen, rect);
                }
                catch (OutOfMemoryException e)
                {
                    Console.WriteLine(e.Message + Environment.NewLine + e.StackTrace + Environment.NewLine + rect.ToString());
                }

                blackPen.Dispose();
            }

        }

        /// <summary>
        /// For picturebox's mousewheel support
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollablePictureBox_MouseEnter(object sender, EventArgs e)
        {
            if (!this.Focused && this.FindForm().ContainsFocus)
            {
                currentScrollPos = ((Panel)this.Parent).AutoScrollPosition;
                //this.Focus();
            }
        }

        private void ScrollablePictureBox_GotFocus(object sender, EventArgs e) 
        {
            ((Panel)this.Parent).AutoScrollPosition = new Point(Math.Abs(currentScrollPos.X), Math.Abs(currentScrollPos.Y));
        }

        /**
         * Creates grip squares.
         *
         */
        List<Rectangle> createSquares(Rectangle rect)
        {
            List<Rectangle> ar = new List<Rectangle>();
            if (moving)
            {
                return ar;
            }

            int wh = 6;

            int x = rect.X - wh / 2;
            int y = rect.Y - wh / 2;
            int w = rect.Width;
            int h = rect.Height;

            ar.Add(new Rectangle(x, y, wh, wh));
            ar.Add(new Rectangle(x + w / 2, y, wh, wh));
            ar.Add(new Rectangle(x + w, y, wh, wh));
            ar.Add(new Rectangle(x + w, y + h / 2, wh, wh));
            ar.Add(new Rectangle(x + w, y + h, wh, wh));
            ar.Add(new Rectangle(x + w / 2, y + h, wh, wh));
            ar.Add(new Rectangle(x, y + h, wh, wh));
            ar.Add(new Rectangle(x, y + h / 2, wh, wh));

            return ar;
        }

        private void ScrollablePictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;

                if (rect == Rectangle.Empty)
                {
                    startPoint = e.Location;
                    pressOut = true;
                }
                else
                {
                    selX = rect.X;
                    selY = rect.Y;
                    selW = rect.Width;
                    selH = rect.Height;

                    Rectangle leftFrame = new Rectangle(selX, selY, frameWidth, selH);
                    Rectangle topFrame = new Rectangle(selX, selY, selW, frameWidth);
                    Rectangle rightFrame = new Rectangle(selX + selW - frameWidth, selY, frameWidth, selH);
                    Rectangle bottomFrame = new Rectangle(selX, selY + selH - frameWidth, selW, frameWidth);

                    Point p = e.Location;

                    bool isInside = rect.Contains(p);
                    bool isLeft = leftFrame.Contains(p);
                    bool isTop = topFrame.Contains(p);
                    bool isRight = rightFrame.Contains(p);
                    bool isBottom = bottomFrame.Contains(p);

                    if (isLeft && isTop)
                    {
                        resizeLeft = true;
                        resizeTop = true;
                        resizeRight = false;
                        resizeBottom = false;
                        move = false;
                    }
                    else if (isTop && isRight)
                    {
                        resizeLeft = false;
                        resizeTop = true;
                        resizeRight = true;
                        resizeBottom = false;
                        move = false;
                    }
                    else if (isRight && isBottom)
                    {
                        resizeLeft = false;
                        resizeTop = false;
                        resizeRight = true;
                        resizeBottom = true;
                        move = false;
                    }
                    else if (isBottom && isLeft)
                    {
                        resizeLeft = true;
                        resizeTop = false;
                        resizeRight = false;
                        resizeBottom = true;
                        move = false;
                    }
                    else if (isLeft)
                    {
                        resizeLeft = true;
                        resizeTop = false;
                        resizeRight = false;
                        resizeBottom = false;
                        move = false;
                    }
                    else if (isTop)
                    {
                        resizeLeft = false;
                        resizeTop = true;
                        resizeRight = false;
                        resizeBottom = false;
                        move = false;
                    }
                    else if (isRight)
                    {
                        resizeLeft = false;
                        resizeTop = false;
                        resizeRight = true;
                        resizeBottom = false;
                        move = false;
                    }
                    else if (isBottom)
                    {
                        resizeLeft = false;
                        resizeTop = false;
                        resizeRight = false;
                        resizeBottom = true;
                        move = false;
                    }
                    else if (isInside)
                    {
                        resizeLeft = false;
                        resizeTop = false;
                        resizeRight = false;
                        resizeBottom = false;
                        move = true;
                    }
                    else
                    {
                        resizeLeft = false;
                        resizeTop = false;
                        resizeRight = false;
                        resizeBottom = false;
                        move = false;
                    }

                    int x = e.X;
                    int y = e.Y;

                    startDragX = x;
                    startDragY = y;

                    preX = rect.X - startDragX;
                    preY = rect.Y - startDragY;

                    if (!rect.Contains(p))
                    {
                        startPoint = p;
                        pressOut = true;
                    }
                }
            }
        }

        private void ScrollablePictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (rect != null)
                {
                    Rectangle testRect = new Rectangle(rect.X - 1, rect.Y - 1, rect.Width + 2, rect.Height + 2);
                    if (!testRect.Contains(e.Location))
                    {
                        Deselect();
                        this.Invalidate();
                    }
                }
            }
        }

        private void ScrollablePictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                MouseDrag(sender, e);
            }

            if (rect != Rectangle.Empty)
            {
                int selX = rect.X;
                int selY = rect.Y;
                int selW = rect.Width;
                int selH = rect.Height;

                Rectangle leftFrame = new Rectangle(selX, selY, frameWidth, selH);
                Rectangle topFrame = new Rectangle(selX, selY, selW, frameWidth);
                Rectangle rightFrame = new Rectangle(selX + selW - frameWidth, selY, frameWidth, selH);
                Rectangle bottomFrame = new Rectangle(selX, selY + selH - frameWidth, selW, frameWidth);

                Point p = e.Location;

                bool isInside = rect.Contains(p);
                bool isLeft = leftFrame.Contains(p);
                bool isTop = topFrame.Contains(p);
                bool isRight = rightFrame.Contains(p);
                bool isBottom = bottomFrame.Contains(p);

                if (isLeft && isTop)
                {
                    this.Cursor = Cursors.SizeNWSE;
                }
                else if (isTop && isRight)
                {
                    this.Cursor = Cursors.SizeNESW;
                }
                else if (isRight && isBottom)
                {
                    this.Cursor = Cursors.SizeNWSE;
                }
                else if (isBottom && isLeft)
                {
                    this.Cursor = Cursors.SizeNESW;
                }
                else if (isLeft)
                {
                    this.Cursor = Cursors.SizeWE;
                }
                else if (isTop)
                {
                    this.Cursor = Cursors.SizeNS;
                }
                else if (isRight)
                {
                    this.Cursor = Cursors.SizeWE;
                }
                else if (isBottom)
                {
                    this.Cursor = Cursors.SizeNS;
                }
                else if (isInside)
                {
                    this.Cursor = Cursors.SizeAll;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                }
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ScrollablePictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;

                if (rect != null)
                {
                    moving = false;
                    pressOut = false;
                    this.Invalidate();
                }
            }

        }

        public void MouseDrag(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = e.X;
                int y = e.Y;

                if (pressOut)
                {
                    rect.X = Math.Min(startPoint.X, x);
                    rect.Y = Math.Min(startPoint.Y, y);
                    rect.Width = Math.Abs(x - startPoint.X);
                    rect.Height = Math.Abs(y - startPoint.Y);
                    moving = true;
                    this.Invalidate();
                }
                else
                {
                    int diffX = startDragX - x;
                    int diffY = startDragY - y;

                    if (resizeLeft)
                    {
                        rect.X = selX - diffX;
                        rect.Width = selW + diffX;
                    }
                    if (resizeTop)
                    {
                        rect.Y = selY - diffY;
                        rect.Height = selH + diffY;
                    }
                    if (resizeRight)
                    {
                        rect.Width = selW - diffX;
                    }
                    if (resizeBottom)
                    {
                        rect.Height = selH - diffY;
                    }
                    if (move)
                    {
                        moving = true;
                        rect.Location = new Point(preX + x, preY + y);
                    }

                    if (rect.Width > minSize && rect.Height > minSize)
                    {
                        this.Invalidate();
                    }
                }
            }
        }

    }
}
