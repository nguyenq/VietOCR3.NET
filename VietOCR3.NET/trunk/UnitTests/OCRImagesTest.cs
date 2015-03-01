using VietOCR.NET;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using VietOCR.NET.Utilities;

namespace TestProject
{     
    /// <summary>
    ///This is a test class for OCRImagesTest and is intended
    ///to contain all OCRImagesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OCRImagesTest
    {
        String lang = "vie";
        OCRImageEntity entity;

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for RecognizeText
        ///</summary>
        [TestMethod()]
        [DeploymentItem("samples/vietsample1.tif", "samples")]
        [DeploymentItem("x86", "x86")]
        [DeploymentItem("tessdata", "tessdata")]
        public void RecognizeTextTest()
        {
            string selectedImageFile = "samples/vietsample1.tif";
            FileInfo imageFile = new FileInfo(selectedImageFile);
            IList<Image> imageList = ImageIOHelper.GetImageList(imageFile);
            entity = new OCRImageEntity(imageList, selectedImageFile, - 1, Rectangle.Empty, lang);
            OCRImages target = new OCRImages();
            target.Language = entity.Language;
            IList<Image> images = entity.ClonedImages;
            string expected = "Tôi từ chinh chiến cũng ra đi";
            string actual;
            actual = target.RecognizeText(images, selectedImageFile);
            Assert.IsTrue(actual.Contains(expected));
        }
    }
}
