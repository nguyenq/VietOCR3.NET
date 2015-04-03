/**
 * Copyright @ 2008
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
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace VietOCR.NET.Utilities
{
    /// <summary>
    /// Common image processing routines.
    /// </summary>
    public class ImageHelper
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
        /// Gets an Image from Clipboard.
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

        /// <summary>
        /// Brightens an image.
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="value"></param>
        /// <returns></returns>
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
        /// Constrasts an image.
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

        // allow a 10px-margin
        private const int margin = 10;

        /// <summary>
        /// Autocrops an image.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="tolerance">range from 0.0 to 1.0</param>
        /// <returns></returns>
        public static Bitmap AutoCrop(Bitmap source, double tolerance)
        {
            // Get top-left pixel color as the "baseline" for cropping
            Color baseColor = source.GetPixel(0, 0);

            int width = source.Width;
            int height = source.Height;

            int minX = 0;
            int minY = 0;
            int maxX = width;
            int maxY = height;

            // Check from top and left. Immediately break the loops when encountering a non-white pixel.
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (colorWithinTolerance(baseColor, source.GetPixel(x, y), tolerance))
                    {
                        minY = y;
                        goto lable1;
                    }
                }
            }
        lable1:

            for (int x = 0; x < width; x++)
            {
                for (int y = minY; y < height; y++)
                {
                    if (colorWithinTolerance(baseColor, source.GetPixel(x, y), tolerance))
                    {
                        minX = x;
                        goto lable2;
                    }
                }
            }
        lable2:
			// Get lower-left pixel color as the "baseline" for cropping
            baseColor = source.GetPixel(minX, height - 1);

            for (int y = height - 1; y >= minY; y--)
            {
                for (int x = minX; x < width; x++)
                {
                    if (colorWithinTolerance(baseColor, source.GetPixel(x, y), tolerance))
                    {
                        maxY = y;
                        goto lable3;
                    }
                }
            }
        lable3:

            for (int x = width - 1; x >= minX; x--)
            {
                for (int y = minY; y < maxY; y++)
                {
                    if (colorWithinTolerance(baseColor, source.GetPixel(x, y), tolerance))
                    {
                        maxX = x;
                        goto lable4;
                    }
                }
            }
        lable4:

            if ((minX - margin) >= 0)
            {
                minX -= margin;
            }

            if ((minY - margin) >= 0)
            {
                minY -= margin;
            }

            if ((maxX + margin) < width)
            {
                maxX += margin;
            }

            if ((maxY + margin) < height)
            {
                maxY += margin;
            }

            int newWidth = maxX - minX + 1;
            int newHeight = maxY - minY + 1;

            // if same size, return the original
            if (newWidth == width && newHeight == height)
            {
                return source;
            }

            Bitmap target = new Bitmap(newWidth, newHeight);
            target.SetResolution(source.HorizontalResolution, source.VerticalResolution);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(source,
                      new RectangleF(0, 0, newWidth, newHeight),
                      new RectangleF(minX, minY, newWidth, newHeight),
                      GraphicsUnit.Pixel);
            }

            return target;
        }

        /// <summary>
        /// Determines color distance.
        /// http://stackoverflow.com/questions/10678015/how-to-auto-crop-an-image-white-border-in-java
        /// </summary>
        /// <param name="a">a color</param>
        /// <param name="b">a color</param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        private static bool colorWithinTolerance(Color a, Color b, double tolerance)
        {
            int aAlpha = (int)a.A;   // Alpha level
            int aRed = (int)a.R;     // Red level
            int aGreen = (int)a.G;   // Green level
            int aBlue = (int)a.B;    // Blue level

            int bAlpha = (int)b.A;   // Alpha level
            int bRed = (int)b.R;     // Red level
            int bGreen = (int)b.G;   // Green level
            int bBlue = (int)b.B;    // Blue level

            double distance = Math.Sqrt((aAlpha - bAlpha) * (aAlpha - bAlpha)
                    + (aRed - bRed) * (aRed - bRed)
                    + (aGreen - bGreen) * (aGreen - bGreen)
                    + (aBlue - bBlue) * (aBlue - bBlue));

            // 510.0 is the maximum distance between two colors 
            // (0,0,0,0 -> 255,255,255,255)
            double percentAway = distance / 510.0d;

            return (percentAway > tolerance);
        }

        /// <summary>
        /// Converts an image to grayscale.
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
        /// Converts an image to monochrome.
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
        /// Inverts color of an image.
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
        /// Sharpens an image.
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
