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
        public static void PerformOCR(string imageFile, string outputFile, string langCode, string pageSegMode, string outputFormat)
        {
            IList<Image> imageList;

            try
            {
                imageList = ImageIOHelper.GetImageList(new FileInfo(imageFile));
                OCR<Image> ocrEngine = new OCRImages();
                ocrEngine.PageSegMode = pageSegMode;
                ocrEngine.OutputFormat = outputFormat;
                string result = ocrEngine.RecognizeText(imageList, langCode);

                // skip post-corrections if hocr output
                if (outputFormat == "txt")
                {
                    // postprocess to correct common OCR errors
                    result = Processor.PostProcess(result, langCode);
                    // correct common errors caused by OCR
                    result = TextUtilities.CorrectOCRErrors(result);
                    // correct letter cases
                    result = TextUtilities.CorrectLetterCases(result);
                }

                DirectoryInfo dir = Directory.GetParent(outputFile);
                if (!dir.Exists)
                {
                    dir.Create();
                }

                using (StreamWriter sw = new StreamWriter(outputFile, false, new System.Text.UTF8Encoding()))
                {
                    sw.Write(result);
                }
            }
            finally
            {
                imageList = null;
            }
        }
    }
}
