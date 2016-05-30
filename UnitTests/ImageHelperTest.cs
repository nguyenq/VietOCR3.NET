using VietOCR.NET.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.IO;

namespace TestProject
{
    
    
    /// <summary>
    ///This is a test class for ImageHelperTest and is intended
    ///to contain all ImageHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ImageHelperTest
    {


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
        /// A test for AutoCrop Bitmap
        ///</summary>
        [TestMethod()]
        [DeploymentItem("samples/vietsample2.png", "samples")]
        public void AutoCropTest()
        {
            Bitmap source = (Bitmap) Image.FromFile("samples/vietsample2.png");
            Size expectedSize = new Size(2275, 2997);
            Bitmap actual;
            actual = ImageHelper.AutoCrop(source, 0.1);
            Assert.AreEqual(expectedSize, actual.Size);
        }

        /// <summary>
        /// A test for Crop Bitmap
        ///</summary>
        [TestMethod()]
        [DeploymentItem("samples/vietsample2.png", "samples")]
        public void CropTest()
        {
            Image source = Image.FromFile("samples/vietsample2.png");
            Size expectedSize = new Size(100, 100);
            Image actual;
            Rectangle cropArea = new Rectangle(100, 100, 100, 100);
            actual = ImageHelper.Crop(source, cropArea);
            Assert.AreEqual(expectedSize, actual.Size);
        }
    }
}
