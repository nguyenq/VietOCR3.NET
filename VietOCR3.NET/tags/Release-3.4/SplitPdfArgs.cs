using System;
using System.Collections.Generic;
using System.Text;

namespace VietOCR.NET
{
    class SplitPdfArgs
    {
        string inputFilename;

        public string InputFilename
        {
            get { return inputFilename; }
            set { inputFilename = value; }
        }
        string outputFilename;

        public string OutputFilename
        {
            get { return outputFilename; }
            set { outputFilename = value; }
        }

        string fromPage;

        public string FromPage
        {
            get { return fromPage; }
            set { fromPage = value; }
        }
        string toPage;

        public string ToPage
        {
            get { return toPage; }
            set { toPage = value; }
        }
        string numOfPages;

        public string NumOfPages
        {
            get { return numOfPages; }
            set { numOfPages = value; }
        }

        bool pages;
        public bool Pages
        {
            get { return pages; }
            set { pages = value; }
        }
    }
}
