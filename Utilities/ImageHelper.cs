
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace VietOCR.NET.Utilities
{
    /// <summary>
    /// This class contains many image processing routines found on the web.
    /// </summary>
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

        public static Image Brighten(Image bmp, float value)
        {
            Image bmpNew = null;

            try
            {
                var matrix = new float[][] {
                    new float[] { 1.0f, 0, 0, 0, 0 },
                    new float[] { 0, 1.0f, 0, 0, 0 },
                    new float[] { 0, 0, 1.0f, 0, 0 },
                    new float[] { 0, 0, 0, 1.0f, 0 },
                    new float[] { value, value, value, 0, 1.0f }
                };

                ColorMatrix cm = new ColorMatrix(matrix);

                ImageAttributes ia = new ImageAttributes();
                ia.SetColorMatrix(cm);

                bmpNew = new Bitmap(bmp.Width, bmp.Height);
                ((Bitmap)bmpNew).SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);
                using (Graphics g = Graphics.FromImage(bmpNew))
                {
                    g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, ia);
                    ia.Dispose();
                }
            }
            catch
            {
                if (bmpNew != null)
                {
                    bmpNew.Dispose();
                    bmpNew = null;
                }
            }

            return bmpNew;
        }

        /// <summary>
        /// Constrast image.
        /// http://bobpowell.net/image_contrast.aspx
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Image Contrast(Image bmp, float value)
        {
            Image bmpNew = null;

            try
            {
                var matrix = new float[][] {
                    new float[] {value, 0, 0, 0, 0},
                    new float[] {0, value, 0, 0, 0},
                    new float[] {0, 0, value, 0, 0},
                    new float[] {0, 0, 0, 1f, 0},
                    //including the BLATANT FUDGE
                    new float[] {0.001f, 0.001f, 0.001f, 0, 1f}
                };

                ColorMatrix cm = new ColorMatrix(matrix);

                ImageAttributes ia = new ImageAttributes();
                ia.SetColorMatrix(cm);

                bmpNew = new Bitmap(bmp.Width, bmp.Height);
                ((Bitmap)bmpNew).SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);
                using (Graphics g = Graphics.FromImage(bmpNew))
                {
                    g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, ia);
                    ia.Dispose();
                }
            }
            catch
            {
                if (bmpNew != null)
                {
                    bmpNew.Dispose();
                    bmpNew = null;
                }
            }

            return bmpNew;
        }

        /// <summary>
        /// Auto-crop an image, eliminating empty surrounding area.
        /// This does not work for some reasons, probably b/c the image is not transparent.
        /// http://stackoverflow.com/questions/4820212/automatically-trim-a-bitmap-to-minimum-size
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Image TrimBitmap(Image source)
        {
            Rectangle srcRect = default(Rectangle);
            BitmapData data = null;
            try
            {
                data = ((Bitmap)source).LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                byte[] buffer = new byte[data.Height * data.Stride];
                Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);

                int xMin = int.MaxValue,
                    xMax = int.MinValue,
                    yMin = int.MaxValue,
                    yMax = int.MinValue;

                bool foundPixel = false;

                // Find xMin
                for (int x = 0; x < data.Width; x++)
                {
                    bool stop = false;
                    for (int y = 0; y < data.Height; y++)
                    {
                        byte alpha = buffer[y * data.Stride + 4 * x + 3];
                        if (alpha != 0)
                        {
                            xMin = x;
                            stop = true;
                            foundPixel = true;
                            break;
                        }
                    }
                    if (stop)
                        break;
                }

                // Image is empty...
                if (!foundPixel)
                    return null;

                // Find yMin
                for (int y = 0; y < data.Height; y++)
                {
                    bool stop = false;
                    for (int x = xMin; x < data.Width; x++)
                    {
                        byte alpha = buffer[y * data.Stride + 4 * x + 3];
                        if (alpha != 0)
                        {
                            yMin = y;
                            stop = true;
                            break;
                        }
                    }
                    if (stop)
                        break;
                }

                // Find xMax
                for (int x = data.Width - 1; x >= xMin; x--)
                {
                    bool stop = false;
                    for (int y = yMin; y < data.Height; y++)
                    {
                        byte alpha = buffer[y * data.Stride + 4 * x + 3];
                        if (alpha != 0)
                        {
                            xMax = x;
                            stop = true;
                            break;
                        }
                    }
                    if (stop)
                        break;
                }

                // Find yMax
                for (int y = data.Height - 1; y >= yMin; y--)
                {
                    bool stop = false;
                    for (int x = xMin; x <= xMax; x++)
                    {
                        byte alpha = buffer[y * data.Stride + 4 * x + 3];
                        if (alpha != 0)
                        {
                            yMax = y;
                            stop = true;
                            break;
                        }
                    }
                    if (stop)
                        break;
                }

                srcRect = Rectangle.FromLTRB(xMin, yMin, xMax + 1, yMax + 1);
            }
            finally
            {
                if (data != null)
                    ((Bitmap)source).UnlockBits(data);
            }

            Bitmap dest = new Bitmap(srcRect.Width, srcRect.Height);
            dest.SetResolution(source.HorizontalResolution, source.VerticalResolution);
            Rectangle destRect = new Rectangle(0, 0, srcRect.Width, srcRect.Height);
            using (Graphics graphics = Graphics.FromImage(dest))
            {
                graphics.DrawImage(source, destRect, srcRect, GraphicsUnit.Pixel);
            }
            return dest;
        }

        /// <summary>
        /// Autocrop an image, removing surrounding white space. This may be slow but works.
        /// However, .NET 3.5 is required for Lambda Expression. Consider replacing with a .NET 2.0 solution.
        /// http://stackoverflow.com/questions/248141/remove-surrounding-whitespace-from-an-image
        /// http://www.itdevspace.com/2012/07/c-crop-white-space-from-around-image.html
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static Bitmap AutoCrop(Bitmap bmp)
        {
            int w = bmp.Width;
            int h = bmp.Height;

            Func<int, bool> allWhiteRow = row =>
            {
                for (int i = 0; i < w; ++i)
                {
                    Color clr = bmp.GetPixel(i, row);
                    if (clr.R != 255 || clr.A < 255)
                        return false;
                }
                return true;
            };

            Func<int, bool> allWhiteColumn = col =>
            {
                for (int i = 0; i < h; ++i)
                {
                    Color clr = bmp.GetPixel(col, i);
                    if (clr.R != 255 || clr.A < 0)
                        return false;
                }
                return true;
            };

            int topmost = 0;
            for (int row = 0; row < h; ++row)
            {
                if (allWhiteRow(row))
                    topmost = row;
                else break;
            }

            int bottommost = 0;
            for (int row = h - 1; row >= 0; --row)
            {
                if (allWhiteRow(row))
                    bottommost = row;
                else break;
            }

            int leftmost = 0, rightmost = 0;
            for (int col = 0; col < w; ++col)
            {
                if (allWhiteColumn(col))
                    leftmost = col;
                else
                    break;
            }

            for (int col = w - 1; col >= 0; --col)
            {
                if (allWhiteColumn(col))
                    rightmost = col;
                else
                    break;
            }

            if (rightmost == 0) rightmost = w; // As reached left
            if (bottommost == 0) bottommost = h; // As reached top.

            // allow a 5px-margin
            int margin = 5;

            if ((leftmost - margin) >= 0)
            {
                leftmost -= margin;
            }

            if ((topmost - margin) >= 0)
            {
                topmost -= margin;
            }

            if ((rightmost + margin) <= w)
            {
                rightmost += margin;
            }

            if ((bottommost + margin) <= h)
            {
                bottommost += margin;
            }

            // if same size, return the original
            if (leftmost == 0 && topmost == 0 && rightmost == w && bottommost == h)
            {
                return bmp;
            }

            int croppedWidth = rightmost - leftmost;
            int croppedHeight = bottommost - topmost;

            if (croppedWidth == 0) // No border on left or right
            {
                leftmost = 0;
                croppedWidth = w;
            }

            if (croppedHeight == 0) // No border on top or bottom
            {
                topmost = 0;
                croppedHeight = h;
            }

            try
            {
                var target = new Bitmap(croppedWidth, croppedHeight);
                target.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);
                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(bmp,
                      new RectangleF(0, 0, croppedWidth, croppedHeight),
                      new RectangleF(leftmost, topmost, croppedWidth, croppedHeight),
                      GraphicsUnit.Pixel);
                }
                return target;
            }
            catch (Exception ex)
            {
                throw new Exception(
                  string.Format("Values are topmost={0} btm={1} left={2} right={3} croppedWidth={4} croppedHeight={5}", topmost, bottommost, leftmost, rightmost, croppedWidth, croppedHeight),
                  ex);
            }
        }

        /// <summary>
        /// http://bobpowell.net/grayscale.aspx
        /// http://code.msdn.microsoft.com/windowsdesktop/ColorMatrix-Image-Filters-f6ed20ae
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static Bitmap ConvertGrayscale(Bitmap img)
        {
            Bitmap dest = new Bitmap(img.Width, img.Height);
            dest.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            ColorMatrix cm = new ColorMatrix(new float[][]{   
                                      new float[]{0.3f,0.3f,0.3f,0,0},
                                      new float[]{0.59f,0.59f,0.59f,0,0},
                                      new float[]{0.11f,0.11f,0.11f,0,0},
                                      new float[]{0,0,0,1,0,0},
                                      new float[]{0,0,0,0,1,0},
                                      new float[]{0,0,0,0,0,1}});

            ImageAttributes ia = new ImageAttributes();
            ia.SetColorMatrix(cm);
            using (Graphics g = Graphics.FromImage(dest))
            {
                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
            }

            return dest;
        }

        /// <summary>
        /// http://bobpowell.net/onebit.aspx
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static Bitmap ConvertMonochrome(Bitmap img)
        {
            if (img.PixelFormat == PixelFormat.Format1bppIndexed)
            {
                return img;
            } 
            else if (img.PixelFormat != PixelFormat.Format32bppPArgb)
            {
                Bitmap temp = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppPArgb);
                temp.SetResolution(img.HorizontalResolution, img.VerticalResolution);
                Graphics g = Graphics.FromImage(temp);
                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
                g.Dispose();
                img = temp;
            }

            //lock the bits of the original bitmap
            BitmapData bmdo = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, img.PixelFormat);

            //and the new 1bpp bitmap
            Bitmap dest = new Bitmap(img.Width, img.Height, PixelFormat.Format1bppIndexed);
            dest.SetResolution(img.HorizontalResolution, img.VerticalResolution);
            BitmapData bmdn = dest.LockBits(new Rectangle(0, 0, dest.Width, dest.Height), ImageLockMode.ReadWrite, PixelFormat.Format1bppIndexed);

            //scan through the pixels Y by X
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    //generate the address of the colour pixel
                    int index = y * bmdo.Stride + (x * 4);

                    //check its brightness
                    if (Color.FromArgb(Marshal.ReadByte(bmdo.Scan0, index + 2),
                                    Marshal.ReadByte(bmdo.Scan0, index + 1),
                                    Marshal.ReadByte(bmdo.Scan0, index)).GetBrightness() > 0.5f)
                        SetIndexedPixel(x, y, bmdn, true); //set it if it's bright.
                }
            }

            //tidy up
            dest.UnlockBits(bmdn);
            img.UnlockBits(bmdo);

            return dest;
        }

        protected static void SetIndexedPixel(int x, int y, BitmapData bmd, bool pixel)
        {
            int index = y * bmd.Stride + (x >> 3);
            byte p = Marshal.ReadByte(bmd.Scan0, index);
            byte mask = (byte)(0x80 >> (x & 0x7));
            if (pixel)
                p |= mask;
            else
                p &= (byte)(mask ^ 0xff);

            Marshal.WriteByte(bmd.Scan0, index, p);
        }

        /// <summary>
        /// http://mariusbancila.ro/blog/2009/11/13/using-colormatrix-for-creating-negative-image/
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static Bitmap InvertColor(Bitmap img)
        {
            Bitmap dest = new Bitmap(img.Width, img.Height);
            dest.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            // create the negative color matrix
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {-1, 0, 0, 0, 0},
                new float[] {0, -1, 0, 0, 0},
                new float[] {0, 0, -1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {1, 1, 1, 0, 1}
            });
            ImageAttributes ia = new ImageAttributes();
            ia.SetColorMatrix(colorMatrix);

            using (Graphics g = Graphics.FromImage(dest))
            {
                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
            }
            return dest;
        }

        /// <summary>
        /// http://stackoverflow.com/questions/903632/sharpen-on-a-bitmap-using-c-sharp
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Bitmap Sharpen(Bitmap image)
        {
            Bitmap sharpenImage = CloneImage(image);

            int filterWidth = 3;
            int filterHeight = 3;
            int width = image.Width;
            int height = image.Height;

            // Create sharpening filter.
            double[,] filter = new double[filterWidth, filterHeight];
            filter[0, 0] = filter[0, 1] = filter[0, 2] = filter[1, 0] = filter[1, 2] = filter[2, 0] = filter[2, 1] = filter[2, 2] = -1;
            filter[1, 1] = 9;

            double factor = 1.0;
            double bias = 0.0;

            Color[,] result = new Color[image.Width, image.Height];

            // Lock image bits for read/write.
            BitmapData pbits = sharpenImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            // Declare an array to hold the bytes of the bitmap.
            int bytes = pbits.Stride * height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(pbits.Scan0, rgbValues, 0, bytes);

            int rgb;
            // Fill the color array with the new sharpened color values.
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    double red = 0.0, green = 0.0, blue = 0.0;

                    for (int filterX = 0; filterX < filterWidth; filterX++)
                    {
                        for (int filterY = 0; filterY < filterHeight; filterY++)
                        {
                            int imageX = (x - filterWidth / 2 + filterX + width) % width;
                            int imageY = (y - filterHeight / 2 + filterY + height) % height;

                            rgb = imageY * pbits.Stride + 3 * imageX;

                            red += rgbValues[rgb + 2] * filter[filterX, filterY];
                            green += rgbValues[rgb + 1] * filter[filterX, filterY];
                            blue += rgbValues[rgb + 0] * filter[filterX, filterY];
                        }
                        int r = Math.Min(Math.Max((int)(factor * red + bias), 0), 255);
                        int g = Math.Min(Math.Max((int)(factor * green + bias), 0), 255);
                        int b = Math.Min(Math.Max((int)(factor * blue + bias), 0), 255);

                        result[x, y] = Color.FromArgb(r, g, b);
                    }
                }
            }

            // Update the image with the sharpened pixels.
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    rgb = y * pbits.Stride + 3 * x;

                    rgbValues[rgb + 2] = result[x, y].R;
                    rgbValues[rgb + 1] = result[x, y].G;
                    rgbValues[rgb + 0] = result[x, y].B;
                }
            }

            // Copy the RGB values back to the bitmap.
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, pbits.Scan0, bytes);
            // Release image bits.
            sharpenImage.UnlockBits(pbits);

            return sharpenImage;
        }

        public static Bitmap GaussianBlur(Bitmap sourceBitmap)
        {
            return ConvolutionFilter(sourceBitmap,
                               GaussianBlur55, 1.0 / 159.0, 0);
        }

        /// <summary>
        /// http://softwarebydefault.com/2013/06/09/image-blur-filters/
        /// </summary>
        /// <param name="sourceBitmap"></param>
        /// <param name="filterMatrix"></param>
        /// <param name="factor"></param>
        /// <param name="bias"></param>
        /// <returns></returns>
        private static Bitmap ConvolutionFilter(Bitmap sourceBitmap,
                                                double[,] filterMatrix,
                                                     double factor = 1,
                                                          int bias = 0)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                                     sourceBitmap.Width, sourceBitmap.Height),
                                                       ImageLockMode.ReadOnly,
                                                 PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            double blue = 0.0;
            double green = 0.0;
            double red = 0.0;

            int filterWidth = filterMatrix.GetLength(1);
            int filterHeight = filterMatrix.GetLength(0);

            int filterOffset = (filterWidth - 1) / 2;
            int calcOffset = 0;

            int byteOffset = 0;

            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++)
                {
                    blue = 0;
                    green = 0;
                    red = 0;

                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;

                    for (int filterY = -filterOffset;
                        filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset;
                            filterX <= filterOffset; filterX++)
                        {
                            calcOffset = byteOffset +
                                         (filterX * 4) +
                                         (filterY * sourceData.Stride);

                            blue += (double)(pixelBuffer[calcOffset]) *
                                    filterMatrix[filterY + filterOffset,
                                                        filterX + filterOffset];

                            green += (double)(pixelBuffer[calcOffset + 1]) *
                                     filterMatrix[filterY + filterOffset,
                                                        filterX + filterOffset];

                            red += (double)(pixelBuffer[calcOffset + 2]) *
                                   filterMatrix[filterY + filterOffset,
                                                      filterX + filterOffset];
                        }
                    }

                    blue = factor * blue + bias;
                    green = factor * green + bias;
                    red = factor * red + bias;

                    blue = (blue > 255 ? 255 :
                           (blue < 0 ? 0 :
                            blue));

                    green = (green > 255 ? 255 :
                            (green < 0 ? 0 :
                             green));

                    red = (red > 255 ? 255 :
                          (red < 0 ? 0 :
                           red));

                    resultBuffer[byteOffset] = (byte)(blue);
                    resultBuffer[byteOffset + 1] = (byte)(green);
                    resultBuffer[byteOffset + 2] = (byte)(red);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                                     resultBitmap.Width, resultBitmap.Height),
                                                      ImageLockMode.WriteOnly,
                                                 PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        private static double[,] GaussianBlur55 =
                new double[,]  
                { { 2, 04, 05, 04, 2 }, 
                  { 4, 09, 12, 09, 4 }, 
                  { 5, 12, 15, 12, 5 },
                  { 4, 09, 12, 09, 4 },
                  { 2, 04, 05, 04, 2 }, };


        /// <summary>
        /// Clones a bitmap using DrawImage.
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static Bitmap CloneImage(Bitmap bmp)
        {
            Bitmap bmpNew = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format24bppRgb);
            bmpNew.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);
            using (Graphics g = Graphics.FromImage(bmpNew))
            {
                g.DrawImage(bmp, 0, 0);
            }

            return bmpNew;
        }

    }
}
