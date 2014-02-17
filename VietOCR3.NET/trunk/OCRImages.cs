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
        public override string RecognizeText(IList<Image> images)
        {
            string tessdata = Path.Combine(basedir, TESSDATA);

            using (TesseractEngine engine = new TesseractEngine(tessdata, Language, EngineMode.Default))
            {
                engine.SetVariable("tessedit_create_hocr", OutputFormat == "hocr" ? "1" : "0");
                Tesseract.PageSegMode psm = (PageSegMode)Enum.Parse(typeof(PageSegMode), PageSegMode);
 
                StringBuilder strB = new StringBuilder();
                int pageNum = 0;

                foreach (Image image in images)
                {
                    pageNum++;
                    using (Pix pix = ConvertBitmapToPix(image))
                    {
                        using (Page page = engine.Process(pix, psm))
                        {
                            string text = OutputFormat == "hocr" ? page.GetHOCRText(pageNum - 1) : page.GetText();

                            if (text == null) return String.Empty;
                            strB.Append(text);
                        }
                    }
                }

                return strB.ToString().Replace("\n", Environment.NewLine);
            }
        }

        /// <summary>
        /// Converts .NET Bitmap to Leptonica Pix.
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private Pix ConvertBitmapToPix(Image image)
        {
            
            try
            {
                return PixConverter.ToPix((Bitmap)image);
            }
            catch
            {
                return ConvertBitmapToPixViaFile(image);
            }
        }

        /// <summary>
        /// Writes .NET image to file and read it back as Pix image. Works in all cases but not efficient.
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private Pix ConvertBitmapToPixViaFile(Image image)
        {
            string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".png";
            image.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
            Pix pix = Pix.LoadFromFile(fileName);
            File.Delete(fileName);

            return pix;
        }
    }
}
