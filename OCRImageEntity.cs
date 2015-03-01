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
using System.Text;
using System.Drawing;
using VietOCR.NET.Utilities;
using System.IO;
using System.Drawing.Imaging;

namespace VietOCR.NET
{
    class OCRImageEntity
    {
        IList<Image> images;

        public IList<Image> Images
        {
            get { return images; }
            set { images = value; }
        }

        string inputfilename;

        public string Inputfilename
        {
            get { return inputfilename; }
            set { inputfilename = value; }
        }

        public IList<Image> ClonedImages
        {
            get { return Clone(images); }
        }

        public IList<string> ImageFiles
        {
            get { return CreateImageFiles(ClonedImages); }
        }

        int index;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        Rectangle rect;

        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
        }

        String lang;

        public String Language
        {
            get { return lang; }
            set { lang = value; }
        }

        /** Horizontal Resolution */
        private int dpiX;
        /** Vertical Resolution */
        private int dpiY;

        public OCRImageEntity(IList<Image> images, string inputfilename, int index, Rectangle rect, String lang)
        {
            this.images = images;
            this.inputfilename = inputfilename;
            this.index = index;
            this.rect = rect;
            this.lang = lang;
        }

        /// <summary>
        /// Clone a list of images. Resample if a resolution is specified.
        /// </summary>
        /// <param name="images">List of original images.</param>
        /// <returns>All or one cloned image.</returns>
        private IList<Image> Clone(IList<Image> images)
        {
            IList<Image> clonedImages = new List<Image>();

            foreach (Image image in (index == -1 ? images : ((List<Image>)images).GetRange(index, 1)))
            {
                if (dpiX == 0 || dpiY == 0)
                {
                    if (rect == null || rect == Rectangle.Empty)
                    {
                        clonedImages.Add(image);
                    }
                    else
                    {
                        clonedImages.Add(ImageHelper.Crop(image, rect));
                        rect = Rectangle.Empty; // no rectangle is needed for processing a subimage
                    }
                }
                else
                {
                    // rescaling
                    if (rect == null || rect == Rectangle.Empty)
                    {
                        clonedImages.Add(ImageHelper.Rescale(image, dpiX, dpiY));
                    }
                    else
                    {
                        clonedImages.Add(ImageHelper.Rescale(ImageHelper.Crop(image, rect), dpiX, dpiY));
                        rect = Rectangle.Empty; // no rectangle is needed for processing a subimage
                    }
                }
            }

            return clonedImages;
        }

        private IList<string> CreateImageFiles(IList<Image> images)
        {
            IList<string> files = new List<string>();

            foreach (Image image in images)
            {
                string tempImageFile = Path.GetTempFileName();
                File.Delete(tempImageFile);
                tempImageFile = Path.ChangeExtension(tempImageFile, ".tif");
                image.Save(tempImageFile, ImageFormat.Tiff);
                files.Add(tempImageFile);
            }

            return files;
        }

        public bool ScreenshotMode
        {
            set
            {
                dpiX = value ? 300 : 0;
                dpiY = value ? 300 : 0;
            }
        }

        public void SetResolution(int dpiX, int dpiY)
        {
            this.dpiX = dpiX;
            this.dpiY = dpiY;
        }

        /// <summary>
        /// Not used after all since the Picturebox already uses copies of these images.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        //private Image Clone(Image source)
        //{
        //PixelFormat pxf = source.PixelFormat;
        //Bitmap bmp = (Bitmap)source;
        //Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

        //BitmapData bData = bmp.LockBits(rect, ImageLockMode.ReadOnly, pxf);
        //// number of bytes in the bitmap
        //int byteCount = bData.Stride * bmp.Height;
        //byte[] bmpBytes = new byte[byteCount];

        //// Copy the locked bytes from memory
        //Marshal.Copy(bData.Scan0, bmpBytes, 0, byteCount);
        //bmp.UnlockBits(bData);

        //Bitmap bmpTarget = new Bitmap(source.Width, source.Height, pxf);
        //bmpTarget.SetResolution(source.HorizontalResolution, source.VerticalResolution);
        //BitmapData bDataTarget = bmpTarget.LockBits(rect, ImageLockMode.WriteOnly, pxf);
        //// Copy the bytes to the bitmap object
        //Marshal.Copy(bmpBytes, 0, bDataTarget.Scan0, bmpBytes.Length);
        //bmpTarget.UnlockBits(bDataTarget);
        //return bmpTarget;


        ////create temporary
        //Image temp = new Bitmap(source.Width, source.Height);
        //((Bitmap)temp).SetResolution(source.HorizontalResolution, source.VerticalResolution);

        ////get graphics
        //Graphics g = Graphics.FromImage(temp);

        ////copy original
        //g.DrawImage(source, 0, 0);
        //g.Dispose();

        ////return temp;
        //using (MemoryStream ms = new MemoryStream())
        //{
        //    bmp.Save(ms, ImageFormat.Png);
        //    //byte[] bmpBytes = ms.GetBuffer();
        //    //using (MemoryStream ms1 = new MemoryStream(bmpBytes))
        //    //{
        //        return Image.FromStream(ms);
        //    //}
        //}
        //}
    }
}
