/**
 * Copyright @ 2011 Quan Nguyen
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
using System.Drawing.Imaging;
using System.IO;
using Tesseract;
using System.Runtime.InteropServices;

namespace VietOCR.NET
{
    class OCRImages : OCR<Image>
    {
        readonly string basedir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        const string TESSDATA = "tessdata/";
        const int oem = 3;

        /// <summary>
        /// Recognize text.
        /// </summary>
        /// <param name="images"></param>
        /// <param name="index"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override string RecognizeText(IList<Image> images, string lang)
        {
            string tessdata = Path.Combine(basedir, TESSDATA);

            using (TesseractEngine engine = new TesseractEngine(tessdata, lang, EngineMode.Default))
            {
                engine.SetVariable("tessedit_create_hocr", Hocr ? "1" : "0");
                Tesseract.PageSegMode psm = (PageSegMode)Enum.Parse(typeof(PageSegMode), PageSegMode);
 
                StringBuilder strB = new StringBuilder();
                int pageNum = 0;

                foreach (Image image in images)
                {
                    pageNum++;
                    using (Pix pix = ConvertBitmapToPix1(image))
                    {
                        using (Page page = engine.Process(pix, psm))
                        {
                            string text = Hocr ? page.GetHOCRText(pageNum - 1) : page.GetText();

                            if (text == null) return String.Empty;
                            strB.Append(text);
                        }
                    }
                }

                return strB.ToString().Replace("\n", Environment.NewLine);
            }
        }

        /// <summary>
        /// Temporary solution. Will be removed after support is built in tesseract wrapper.
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        private Pix ConvertBitmapToPix1(Image bmp)
        {
            string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".png";
            bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
            Pix pix = Pix.LoadFromFile(fileName);
            File.Delete(fileName);

            return pix;
        }

        ///// <summary>
        ///// This currently supports only a few image formats. Will address these in the next version.  1/6/2013
        ///// </summary>
        ///// <param name="image"></param>
        ///// <returns></returns>
        //private Pix ConvertBitmapToPix2(Image image)
        //{
        //    Pix pix = PixConverter.ToPix((Bitmap) image);
        //    return pix;
        //}

        ///// <summary>
        ///// Converts .NET Bitmap to Leptonica Pix.
        ///// Not completed yet!
        ///// </summary>
        ///// <param name="bmp"></param>
        ///// <returns></returns>
        //private Pix ConvertBitmapToPix(Bitmap bmp)
        //{
        //    IntPtr pval = IntPtr.Zero;
        //    BitmapData bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

        //    try
        //    {
        //        var depth = Bitmap.GetPixelFormatSize(bmp.PixelFormat);

        //        // Get the address of the first line.
        //        IntPtr ptr = bd.Scan0;

        //        // Declare an array to hold the bytes of the bitmap. 
        //        int bytes = Math.Abs(bd.Stride) * bmp.Height;
        //        byte[] rgbValues = new byte[bytes];

        //        // Copy the RGB values into the array.
        //        System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

        //        Pix pix = Pix.Create(bmp.Width, bmp.Height, depth);
        //        //    pix.data = rgbValues;

        //        return pix;
        //    }
        //    finally
        //    {
        //        bmp.UnlockBits(bd);
        //    }
        //}
    }
}
