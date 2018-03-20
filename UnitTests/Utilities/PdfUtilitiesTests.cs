using Microsoft.VisualStudio.TestTools.UnitTesting;
using VietOCR.NET.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VietOCR.NET.Utilities.Tests
{
    [TestClass()]
    public class PdfUtilitiesTests
    {
        [TestMethod()]
        [Ignore]
        public void ConvertPdf2TiffTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        [Ignore]
        public void ConvertPdf2PngTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        [Ignore]
        public void SplitPdfTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        [DeploymentItem("samples/vietsample1.pdf", "samples")]
        public void GetPdfPageCountTest()
        {
            string inputPdfFile = "samples/vietsample1.pdf";
            int expResult = 2;
            int result = PdfUtilities.GetPdfPageCount(inputPdfFile);
            Assert.AreEqual(expResult, result);
        }

        [TestMethod()]
        [Ignore]
        public void MergePdfTest()
        {
            Assert.Fail();
        }
    }
}