using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ConvertPDF;

namespace VietOCR.NET.Utilities
{
    class PdfUtilities
    {
        /// <summary>
        /// Convert PDF to TIFF format.
        /// </summary>
        /// <param name="inputPdfFile"></param>
        /// <returns>a multi-page TIFF image</returns>
        public static string ConvertPdf2Tiff(string inputPdfFile)
        {
            string[] pngFiles = null;

            try
            {
                pngFiles = ConvertPdf2Png(inputPdfFile);
                string tiffFile = Path.GetTempFileName();
                File.Delete(tiffFile);
                tiffFile = Path.ChangeExtension(tiffFile, ".tif");

                // put PNG images into a single multi-page TIFF image for return
                ImageIOHelper.MergeTiff(pngFiles, tiffFile);
                return tiffFile;
            }
            catch (ApplicationException ae)
            {
                Console.WriteLine("ERROR: " + ae.Message);
                throw;
            }
            finally
            {
                if (pngFiles != null)
                {
                    // delete temporary PNG images
                    foreach (string tempFile in pngFiles)
                    {
                        File.Delete(tempFile);
                    }
                }
            }
        }

        /// <summary>
        /// Convert PDF to PNG format.
        /// </summary>
        /// <param name="inputPdfFile"></param>
        /// <returns>an array of PNG images</returns>
        public static string[] ConvertPdf2Png(string inputPdfFile)
        {
            PDFConvert converter = new PDFConvert();
            converter.GraphicsAlphaBit = 4;
            converter.TextAlphaBit = 4;
            converter.ResolutionX = 300; // -r300
            converter.OutputFormat = "pnggray"; // -sDEVICE
            converter.ThrowOnlyException = true; // rethrow exceptions

            string sOutputFile = string.Format("{0}\\workingimage%03d.png", Path.GetDirectoryName(inputPdfFile));
            bool success = converter.Convert(inputPdfFile, sOutputFile);

            if (success)
            {
                // find working files
                string[] workingFiles = Directory.GetFiles(Path.GetDirectoryName(inputPdfFile), "workingimage???.png");
                Array.Sort(workingFiles);
                return workingFiles;
            }
            else
            {
                return new string[0];
            }
        }

        /// <summary>
        /// Split PDF.
        /// </summary>
        /// <param name="inputPdfFile"></param>
        /// <param name="outputPdfFile"></param>
        /// <param name="firstPage"></param>
        /// <param name="lastPage"></param>
        public static void SplitPdf(string inputPdfFile, string outputPdfFile, string firstPage, string lastPage)
        {
            PDFConvert converter = new PDFConvert();
            converter.OutputFormat = "pdfwrite"; // -sDEVICE
            converter.ThrowOnlyException = true; // rethrow exceptions

            //gs -sDEVICE=pdfwrite -dNOPAUSE -dQUIET -dBATCH -dFirstPage=m -dLastPage=n -sOutputFile=out.pdf in.pdf
            if (firstPage.Trim().Length > 0)
            {
                converter.FirstPageToConvert = Int32.Parse(firstPage);
            }

            if (lastPage.Trim().Length > 0)
            {
                converter.LastPageToConvert = Int32.Parse(lastPage);
            }

            if (!converter.Convert(inputPdfFile, outputPdfFile))
            {
                throw new ApplicationException("Split PDF failed.");
            }
        }

        /// <summary>
        /// Get PDF Page Count.
        /// </summary>
        /// <param name="inputPdfFile"></param>
        /// <returns></returns>
        public static int GetPdfPageCount(string inputPdfFile)
        {
            PDFConvert converter = new PDFConvert();
            converter.RedirectIO = true;
            converter.ThrowOnlyException = true; // rethrow exceptions

            //gs -q -sPDFname=test.pdf pdfpagecount.ps
            List<string> gsArgs = new List<string>();
            gsArgs.Add("-gs");
            gsArgs.Add("-dNOPAUSE");
            gsArgs.Add("-dQUIET");
            gsArgs.Add("-dBATCH");
            gsArgs.Add("-sPDFname=" + inputPdfFile);
            gsArgs.Add("Library/pdfpagecount.ps");

            int pageCount = 0;

            using (StringWriter writer = new StringWriter())
            {
                try
                {
                    converter.StdOut = writer;
                    if (converter.Initialize(gsArgs.ToArray()))
                    {
                        pageCount = Int32.Parse(writer.ToString().Replace("%%Pages: ", String.Empty));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return pageCount;
        }

        /// <summary>
        /// Merge PDF files.
        /// </summary>
        /// <param name="inputPdfFiles"></param>
        /// <param name="outputPdfFile"></param>
        public static void MergePdf(string[] inputPdfFiles, string outputPdfFile)
        {
            PDFConvert converter = new PDFConvert();
            converter.ThrowOnlyException = true; // rethrow exceptions

            //gs -sDEVICE=pdfwrite -dNOPAUSE -dQUIET -dBATCH -sOutputFile=out.pdf in1.pdf in2.pdf in3.pdf
            List<string> gsArgs = new List<string>();
            gsArgs.Add("-gs");
            gsArgs.Add("-sDEVICE=pdfwrite");
            gsArgs.Add("-dNOPAUSE");
            gsArgs.Add("-dQUIET");
            gsArgs.Add("-dBATCH");
            gsArgs.Add("-sOutputFile=" + outputPdfFile);
            
            foreach (string inputPdfFile in inputPdfFiles)
            {
                gsArgs.Add(inputPdfFile);
            }

            if (!converter.Initialize(gsArgs.ToArray()))
            {
                throw new ApplicationException("Merge PDF failed.");
            }
        }
    }
}
