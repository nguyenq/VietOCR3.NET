//
// Adapt from http://www.codeproject.com/KB/recipes/NHunspellExtenderProvider.aspx and
// http://www.codedblog.com/2007/09/17/owner-drawing-a-windowsforms-textbox/
//

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace VietOCR.NET.Controls
{
    /// <summary>
    /// This is the class that handles painting the wavy red lines.
    /// 
    /// It utilizes the NativeWindow to find out when it needs to draw
    /// </summary>
    /// <remarks></remarks>
    class CustomPaintTextBox : NativeWindow, IDisposable
    {
        private TextBoxBase clientTextBox;
        private Bitmap myBitmap;
        private Graphics textBoxGraphics;
        private Graphics bufferGraphics;
        private SpellCheckHelper mySpeller;


        /// <summary>
        /// This is called when the textbox is being redrawn.
        /// When it is, for the textbox to get refreshed, call its default
        /// paint method and then call our method
        /// </summary>
        /// <param name="m">The windows message</param>
        /// <remarks></remarks>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case 15:
                    //This is the WM_PAINT message
                    //Invalidate the textBoxBase so that it gets refreshed properly
                    clientTextBox.Invalidate();

                    //call the default win32 Paint method for the TextBoxBase first
                    base.WndProc(ref m);

                    //now use our code to draw the extra stuff
                    this.CustomPaint();

                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        public CustomPaintTextBox(TextBoxBase clientTextBox, SpellCheckHelper speller)
        {
            //Set up the CustomPaintTextBox
            this.clientTextBox = clientTextBox;
            this.mySpeller = speller;

            //Create a bitmap with the same dimensions as the textbox
            myBitmap = new Bitmap(clientTextBox.Width, clientTextBox.Height);

            //Create the graphics object from this bitmpa...this is where we will draw the lines to start with
            bufferGraphics = Graphics.FromImage(this.myBitmap);
            bufferGraphics.Clip = new Region(clientTextBox.ClientRectangle);

            //Get the graphics object for the textbox.  We use this to draw the bufferGraphics
            textBoxGraphics = Graphics.FromHwnd(clientTextBox.Handle);

            //Assign a handle for this class and set it to the handle for the textbox
            this.AssignHandle(clientTextBox.Handle);

            //We also need to make sure we update the handle if the handle for the textbox changes
            //This occurs if wordWrap is turned off for a RichTextBox
            clientTextBox.HandleCreated += TextBoxBase_HandleCreated;

            //We need to add a handler to change the clip rectangle if the textBox is resized
            clientTextBox.ClientSizeChanged += TextBoxBase_ClientSizeChanged;

            //this.disposedValue = false;
        }

        /// <summary>
        /// Gets the ranges of chars that represent the spelling errors and then draw a wavy red line underneath
        /// them.
        /// </summary>
        /// <remarks></remarks>
        //ByVal sender As Object, ByVal e As DoWorkEventArgs)
        private void CustomPaint()
        {
            CharacterRange[] errorRanges = mySpeller.GetSpellingErrorRanges;
            if (errorRanges.Length == 0)
            {
                return;
            }

            RichTextBox tempRTB = null;

            if (clientTextBox is RichTextBox)
            {
                tempRTB = new RichTextBox();
                tempRTB.Rtf = ((RichTextBox)clientTextBox).Rtf;
            }

            //Clear the graphics buffer
            bufferGraphics.Clear(Color.Transparent);


            //First get the ranges of characters visible in the textbox
            Point startPoint = new Point(0, 0);
            Point endPoint = new Point(clientTextBox.ClientRectangle.Width, clientTextBox.ClientRectangle.Height);
            long startIndex = clientTextBox.GetCharIndexFromPosition(startPoint);
            long endIndex = clientTextBox.GetCharIndexFromPosition(endPoint);

            // Create the end points to call the drawWave

            foreach (CharacterRange currentRange in errorRanges)
            {
                //Get the X, Y of the start and end characters
                startPoint = clientTextBox.GetPositionFromCharIndex(currentRange.First);
                endPoint = clientTextBox.GetPositionFromCharIndex(currentRange.First + currentRange.Length - 1);

                if (startPoint.Y != endPoint.Y)
                {
                    //We have a word on multiple lines
                    int curIndex = 0;
                    int startingIndex = 0;
                    curIndex = currentRange.First;
                    startingIndex = curIndex;
                GetNextLine:

                    //Determine the first line of waves to draw
                    while ((clientTextBox.GetPositionFromCharIndex(curIndex).Y == startPoint.Y) & (curIndex <= (currentRange.First + currentRange.Length - 1)))
                    {
                        curIndex++;
                    }

                    //Go back to the previous character
                    curIndex--;

                    endPoint = clientTextBox.GetPositionFromCharIndex(curIndex);
                    Point offsets = GetOffsets(ref clientTextBox, startingIndex, curIndex, tempRTB);

                    //If we're using a RichTextBox, we have to account for the zoom factor
                    if (clientTextBox is RichTextBox)
                    {
                        offsets.Y = (int)(offsets.Y * ((RichTextBox)clientTextBox).ZoomFactor);
                    }

                    //Reset the starting and ending points to make sure we're underneath the word
                    //(The measurestring adds some margin, so remove them)
                    startPoint.Y += offsets.Y - 2;
                    endPoint.Y += offsets.Y - 2;
                    endPoint.X += offsets.X - 0;

                    //Add a new wavy line using the starting and ending point
                    DrawWave(startPoint, endPoint);

                    startingIndex = curIndex + 1;
                    curIndex++;
                    startPoint = clientTextBox.GetPositionFromCharIndex(curIndex);

                    if (curIndex <= (currentRange.First + currentRange.Length - 1))
                    {
                        goto GetNextLine;
                    }
                }
                else
                {
                    Point offsets = GetOffsets(ref clientTextBox, currentRange.First, (currentRange.First + currentRange.Length - 1), tempRTB);

                    //If we're using a RichTextBox, we have to account for the zoom factor
                    if (clientTextBox is RichTextBox)
                    {
                        offsets.Y = (int)(offsets.Y * ((RichTextBox)clientTextBox).ZoomFactor);
                    }
                    //Reset the starting and ending points to make sure we're underneath the word
                    //(The measurestring adds some margin, so remove them)
                    startPoint.Y += offsets.Y - 2;
                    endPoint.Y += offsets.Y - 2;
                    endPoint.X += offsets.X - 4;

                    //Add a new wavy line using the starting and ending point
                    //If e.Cancel Then Return
                    DrawWave(startPoint, endPoint);
                }
            }

            //We've drawn all of the wavy lines, so draw that image over the textbox
            textBoxGraphics.DrawImageUnscaled(myBitmap, 0, 0);
        }

        /// <summary>
        /// Determines the X and Y offsets to use based on font height last letter width
        /// </summary>
        /// <param name="curTextBox"></param>
        /// <param name="startingIndex"></param>
        /// <param name="endingIndex"></param>
        /// <param name="tempRTB"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private Point GetOffsets(ref TextBoxBase curTextBox, int startingIndex, int endingIndex, RichTextBox tempRTB)
        {
            //We now have the top left point of the characters, now we need to add the offsets
            int offsetY = 0;
            Font fontToUse = curTextBox.Font;
            fontToUse = new Font(fontToUse.FontFamily, 0.1f, fontToUse.Style, fontToUse.Unit, fontToUse.GdiCharSet, fontToUse.GdiVerticalFont);

            //If it's a RichTextBox, we have to do some extra things
            if (curTextBox is RichTextBox)
            {
                //We need to go through every character where we will draw the lines and get the tallest
                //font height

                //Create a temporary textbox for getting the RTF info so that we don't have to select and
                //de-select a lot of text and cause the screen to have to refresh
                if (tempRTB == null)
                {
                    tempRTB = new RichTextBox();
                    tempRTB.Rtf = ((RichTextBox)curTextBox).Rtf;
                }


                if (tempRTB.Text.Length > 0)
                {
                    //Have to find the first visible character on that line
                    int curCharLine = tempRTB.GetLineFromCharIndex(startingIndex);
                    int firstCharInLine = tempRTB.GetFirstCharIndexFromLine(curCharLine);
                    int lastCharInLine = tempRTB.GetFirstCharIndexFromLine(curCharLine + 1);

                    if (lastCharInLine == -1)
                    {
                        lastCharInLine = curTextBox.TextLength;
                    }

                    //Now go through every character that is visible and get the biggest font height
                    //Use the tempRTB for this
                    for (int i = firstCharInLine + 1; i <= (lastCharInLine + 1); i++)
                    {
                        tempRTB.SelectionStart = i;
                        tempRTB.SelectionLength = 1;
                        if (tempRTB.SelectionFont.Height > fontToUse.Height)
                        {
                            //fontHeight = .SelectionFont.Height
                            fontToUse = tempRTB.SelectionFont;
                        }
                    }

                    offsetY = fontToUse.Height;
                }
            }
            else
            {
                //If we get here, it's just a standard textbox and we can just use the font height
                fontToUse = curTextBox.Font;
                offsetY = curTextBox.Font.Height;
            }

            //Now find out how wide the last character is
            int offsetX = (int)textBoxGraphics.MeasureString(curTextBox.Text[endingIndex].ToString(), fontToUse).Width;

            return new Point(offsetX, offsetY);
        }

        /// <summary>
        /// The textbox is not redrawn much, so this will force the textbox to call the custom paint function.
        /// Otherwise, text can be entered and no wavy red lines will appear
        /// </summary>
        /// <remarks></remarks>
        public void ForcePaint()
        {
            clientTextBox.Invalidate();
        }


        /// <summary>
        /// Draws the wavy red line given a starting point and an ending point
        /// </summary>
        /// <param name="StartOfLine">A Point representing the starting point</param>
        /// <param name="EndOfLine">A Point representing the ending point</param>
        /// <remarks></remarks>
        private void DrawWave(Point start, Point end)
        {
            Pen newPen = Pens.Red;

            if ((end.X - start.X) > 2)
            {
                ArrayList pl = new ArrayList();
                bool down = true;

                for (int i = start.X; i <= (end.X - 1); i += 2)
                {
                    if (down)
                    {
                        pl.Add(new Point(i, start.Y));
                    }
                    else
                    {
                        pl.Add(new Point(i, start.Y + 2));
                    }
                    down ^= true;
                }

                Point[] p = (Point[])pl.ToArray(typeof(Point));
                bufferGraphics.DrawLines(newPen, p);
            }
            else
            {
                bufferGraphics.DrawLines(newPen, new Point[] { start, end });
            }
        }


        /// <summary>
        /// Reassign this classes handle and the graphics object anytime the textbox's handle is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void TextBoxBase_HandleCreated(object sender, System.EventArgs e)
        {
            this.AssignHandle(clientTextBox.Handle);
            textBoxGraphics = Graphics.FromHwnd(clientTextBox.Handle);
        }


        /// <summary>
        /// When the TextBoxBase is resized, this will reset the objects that are used to draw
        /// the wavy, red line.  Without this, anything outside of the original bounds will not
        /// be drawn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void TextBoxBase_ClientSizeChanged(object sender, System.EventArgs e)
        {
            TextBoxBase tempTextBox = (TextBoxBase)sender;

            //Create a bitmap with the same dimensions as the textbox
            myBitmap = new Bitmap(tempTextBox.Width, tempTextBox.Height);

            //Create the graphics object from this bitmap...this is where we will draw the lines to start with
            bufferGraphics = Graphics.FromImage(myBitmap);
            bufferGraphics.Clip = new Region(tempTextBox.ClientRectangle);

            //Get the graphics object for the textbox.  We use this to draw the bufferGraphics
            textBoxGraphics = Graphics.FromHwnd(tempTextBox.Handle);
        }


        #region "IDisposable Support"

        // To detect redundant calls
        private bool disposedValue;

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue & disposing)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).

                    if (clientTextBox != null)
                    {
                        this.ReleaseHandle();

                        clientTextBox.Invalidate();

                        clientTextBox.HandleCreated -= TextBoxBase_HandleCreated;
                        clientTextBox.ClientSizeChanged -= TextBoxBase_ClientSizeChanged;

                        clientTextBox.Dispose();
                        clientTextBox = null;
                    }

                    if (myBitmap != null)
                    {
                        myBitmap.Dispose();
                        myBitmap = null;
                    }

                    if (textBoxGraphics != null)
                    {
                        textBoxGraphics.Dispose();
                        textBoxGraphics = null;
                    }

                    if (bufferGraphics != null)
                    {
                        bufferGraphics.Dispose();
                        bufferGraphics = null;
                    }

                    if (mySpeller != null)
                    {
                        mySpeller = null;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                // TODO: set large fields to null.
            }
            this.disposedValue = true;
        }

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
