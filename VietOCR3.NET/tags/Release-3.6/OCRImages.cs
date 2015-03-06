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
        const int oem = 3;

        /// <summary>
        /// Recognize text.
        /// </summary>
        /// <param name="images"></param>
        /// <param name="inputName">input filename</param>
        /// <param name="index"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override string RecognizeText(IList<Image> images, string inputName)
        {
            IEnumerable<string> configs_file = new List<string>() { CONFIGS_FILE };

            using (TesseractEngine engine = new TesseractEngine(Datapath, Language, EngineMode.Default, configs_file))
            {
                engine.SetVariable("tessedit_create_hocr", OutputFormat == "hocr" ? "1" : "0");
                ControlParameters(engine);
                Tesseract.PageSegMode psm = (PageSegMode)Enum.Parse(typeof(PageSegMode), PageSegMode);

                StringBuilder strB = new StringBuilder();
                int pageNum = 0;

                foreach (Image image in images)
                {
                    pageNum++;
                    using (Pix pix = ConvertBitmapToPix(image))
                    {
                        using (Page page = engine.Process(pix, inputName, psm))
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
        /// Reads tessdata/configs/tess_configvars and SetVariable on Tesseract engine.
        /// This only works for non-init parameters (@see <a href="https://code.google.com/p/tesseract-ocr/wiki/ControlParams">ControlParams</a>).
        /// </summary>
        /// <param name="engine"></param>
        void ControlParameters(TesseractEngine engine)
        {
            string configsFilePath = Path.Combine(Datapath, "tessdata/configs/" + CONFIGVARS_FILE);
            if (!File.Exists(configsFilePath))
            {
                return;
            }

            string[] lines = File.ReadAllLines(configsFilePath);
            foreach (string line in lines)
            {
                if (!line.Trim().StartsWith("#"))
                {
                    try
                    {
                        string[] keyValuePair = line.Trim().Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                        string value = keyValuePair[1];
                        if (value == "T" || value == "F")
                        {
                            engine.SetVariable(keyValuePair[0], value == "T" ? true : false);
                        }
                        else
                        {
                            engine.SetVariable(keyValuePair[0], keyValuePair[1]);
                        }
                    }
                    catch
                    {
                        //ignore and continue on
                    }
                }
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
