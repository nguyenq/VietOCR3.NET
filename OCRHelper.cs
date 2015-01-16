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
        /// <param name="imageFile"></param>
        /// <param name="outputFile"></param>
        /// <param name="langCode"></param>
        /// <param name="pageSegMode"></param>
        /// <param name="outputFormat"></param>
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

                bool postprocess = "txt+" == outputFormat;

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
                string result = ocrEngine.RecognizeText(imageList);

                // post-corrections for txt+ output
                if (postprocess)
                {
                    // postprocess to correct common OCR errors
                    result = Processor.PostProcess(result, langCode);
                    // correct common errors caused by OCR
                    result = TextUtilities.CorrectOCRErrors(result);
                    // correct letter cases
                    result = TextUtilities.CorrectLetterCases(result);
                }

                if (outputFormat == "pdf")
                {
                    byte[] bytes = null; // get the byte array
                    File.WriteAllBytes(outputFile, bytes);
                } 
                else 
                {
                    using (StreamWriter sw = new StreamWriter(outputFile, false, new System.Text.UTF8Encoding()))
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
