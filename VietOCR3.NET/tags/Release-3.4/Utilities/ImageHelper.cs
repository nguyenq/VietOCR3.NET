using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace VietOCR.NET.Utilities
{
    class ImageHelper
    {
        /// <summary>
        /// Rescales an image.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="dpiX"></param>
        /// <param name="dpiY"></param>
        /// <returns></returns>
        public static Image Rescale(Image image, int dpiX, int dpiY)
        {
            Bitmap bm = new Bitmap((int)(image.Width * dpiX / image.HorizontalResolution), (int)(image.Height * dpiY / image.VerticalResolution));
            bm.SetResolution(dpiX, dpiY);
            Graphics g = Graphics.FromImage(bm);
            g.InterpolationMode = InterpolationMode.Bicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawImage(image, 0, 0);
            g.Dispose();

            return bm;
        }

        /// <summary>
        /// Gets an Image from Clipboard
        /// </summary>
        /// <returns></returns>
        public static Image GetClipboardImage()
        {
            if (Clipboard.ContainsImage())
            {
                return Clipboard.GetImage();
            }
            return null;
        }

        /// <summary>
        /// Crops an image.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="cropArea"></param>
        /// <returns></returns>
        public static Image Crop(Image image, Rectangle cropArea)
        {
            Bitmap bmp = new Bitmap(cropArea.Width, cropArea.Height);
            bmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.DrawImage(image, 0, 0, cropArea, GraphicsUnit.Pixel);
            gfx.Dispose();

            return bmp;
        }

        /// <summary>
        /// Crops an image (another method).
        /// </summary>
        /// <param name="image"></param>
        /// <param name="cropArea"></param>
        /// <returns></returns>
        //public static Image Crop(Image image, Rectangle cropArea)
        //{
        //    Bitmap bitmap = new Bitmap(image);
        //    bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
        //    return bitmap.Clone(cropArea, image.PixelFormat); // this has thrown OutOfMemoryException on WinXP
        //}

        /// <summary>
        /// Rotates an image.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Bitmap Rotate(Image image, double angle)
        {
            Bitmap bm = new Bitmap(image.Width, image.Height);
            bm.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            Graphics g = Graphics.FromImage(bm);
            //move rotation point to center of image
            g.TranslateTransform((float)image.Width / 2, (float)image.Height / 2);
            //rotate
            g.RotateTransform((float)angle);
            //move image back
            g.TranslateTransform(-(float)image.Width / 2, -(float)image.Height / 2);
            //draw passed in image onto graphics object
            g.DrawImage(image, 0, 0);
            g.Dispose();

            return bm;
        }
    }
}
