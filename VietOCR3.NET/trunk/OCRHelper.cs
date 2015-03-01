using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using VietOCR.NET.Utilities;
using VietOCR.NET.Postprocessing;

namespace VietOCR.NET
{
    class OCRHelper
    {
        /// <summary>
        /// Performs OCR for bulk/batch and console operations.
        /// </summary>
        /// <param name="imageFile">Image file</param>
        /// <param name="outputFile">Output file without extension</param>
        /// <param name="langCode">language code</param>
        /// <param name="pageSegMode">page segmentation mode</param>
        /// <param name="outputFormat">format of output file. Possible values: <code>text</code>, <code>text+</code> (with post-corrections), <code>hocr</code></param>
        public static void PerformOCR(string imageFile, string outputFile, string langCode, string pageSegMode, string outputFormat)
        {
            IList<Image> imageList;

            try
            {
                DirectoryInfo dir = Directory.GetParent(outputFile);
                if (dir != null && !dir.Exists)
                {
                    dir.Create();
                }

                bool postprocess = "text+" == outputFormat;

                OCR<Image> ocrEngine = new OCRImages();
                ocrEngine.PageSegMode = pageSegMode;
                ocrEngine.Language = langCode;
                ocrEngine.OutputFormat = outputFormat.Replace("+", string.Empty);

                // convert PDF to TIFF
                if (imageFile.ToLower().EndsWith(".pdf"))
                {
                    imageFile = PdfUtilities.ConvertPdf2Tiff(imageFile);
                }

                imageList = ImageIOHelper.GetImageList(new FileInfo(imageFile));
                string result = ocrEngine.RecognizeText(imageList, imageFile);

                // post-corrections for text+ output
                if (postprocess)
                {
                    // postprocess to correct common OCR errors
                    result = Processor.PostProcess(result, langCode);
                    // correct common errors caused by OCR
                    result = TextUtilities.CorrectOCRErrors(result);
                    // correct letter cases
                    result = TextUtilities.CorrectLetterCases(result);
                }

                //if (outputFormat == "pdf") // not yet supported
                //{
                //    byte[] bytes = null; // get the byte array
                //    File.WriteAllBytes(outputFile, bytes);
                //} 
                //else 
                {
                    string filename = outputFile + "." + outputFormat.Replace("+", string.Empty).Replace("text", "txt").Replace("hocr", "html");
                    using (StreamWriter sw = new StreamWriter(filename, false, new System.Text.UTF8Encoding()))
                    {
                        sw.Write(result);
                    }
                }
            }
            finally
            {
                imageList = null;
            }
        }
    }
}
