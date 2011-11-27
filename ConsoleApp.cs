using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using VietOCR.NET.Postprocessing;
using VietOCR.NET.Utilities;

namespace VietOCR.NET
{
    class ConsoleApp
    {
        public static void Main(string[] args)
        {
            new ConsoleApp().PerformOCR(args);
        }

        void PerformOCR(string[] args)
        {
            try
            {
                if (args[0] == "-?" || args[0] == "-help" || args.Length == 1 || args.Length == 3 || args.Length == 5)
                {
                    Console.WriteLine("Usage: vietocr imagefile outputfile [-l lang] [-psm pagesegmode]");
                    return;
                }
                FileInfo imageFile = new FileInfo(args[0]);
                FileInfo outputFile = new FileInfo(args[1]);

                if (!imageFile.Exists)
                {
                    Console.WriteLine("Input file does not exist.");
                    return;
                }

                string curLangCode = "eng"; //default language
                string psm = "3"; // or alternatively, "PSM_AUTO"; // 3 - Fully automatic page segmentation, but no OSD (default)

                if (args.Length == 4)
                {
                    if (args[2].Equals("-l"))
                    {
                        curLangCode = args[3];
                    }
                    else if (args[2].Equals("-psm"))
                    {
                        psm = args[3];
                    }
                }
                else if (args.Length == 6)
                {
                    curLangCode = args[3];
                    psm = args[5];
                    try
                    {
                        Int16.Parse(psm);
                    }
                    catch
                    {
                        Console.WriteLine("Invalid input value.");
                        return;
                    }
                }

                IList<Image> imageList = ImageIOHelper.GetImageList(imageFile);

                OCR<Image> ocrEngine = new OCRImages();
                ocrEngine.PSM = psm;
                string result = ocrEngine.RecognizeText(imageList, curLangCode);

                // postprocess to correct common OCR errors
                result = Processor.PostProcess(result, curLangCode);
                // correct common errors caused by OCR
                result = TextUtilities.CorrectOCRErrors(result);
                // correct letter cases
                result = TextUtilities.CorrectLetterCases(result);

                using (StreamWriter sw = new StreamWriter(outputFile.FullName + ".txt", false, new System.Text.UTF8Encoding()))
                {
                    sw.Write(result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
