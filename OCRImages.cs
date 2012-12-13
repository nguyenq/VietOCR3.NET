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

namespace VietOCR.NET
{
    class OCRImages : OCR<Image>
    {
        readonly string basedir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        const string TESSDATA = "tessdata/";
        const int oem = 3;

        /// <summary>
        /// Recognize text
        /// </summary>
        /// <param name="images"></param>
        /// <param name="index"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override string RecognizeText(IList<Image> images, string lang)
        {
            string tessdata = Path.Combine(basedir, TESSDATA);
            TesseractEngine processor = new TesseractEngine(tessdata, lang, EngineMode.Default);
            //processor.((ePageSegMode)Enum.Parse(typeof(ePageSegMode), PageSegMode));

            StringBuilder strB = new StringBuilder();

            foreach (Image image in images)
            {
                string text = processor.Process(ConvertImage2Pix((Bitmap)image), Tesseract.PageSegMode.Auto).GetText();

                if (text == null) return String.Empty;
                strB.Append(text);
            }

            return strB.ToString().Replace("\n", Environment.NewLine);
        }

        private IPix ConvertImage2Pix(Bitmap bmp)
        {
            IntPtr pval = IntPtr.Zero;
            BitmapData bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

            try
            {
                pval = bd.Scan0;
                return Pix.Create(pval);
            }
            finally
            {
                bmp.UnlockBits(bd);
            }
        }
    }
}
