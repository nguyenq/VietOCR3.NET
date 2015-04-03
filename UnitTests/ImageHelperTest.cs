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
        ///A test for AutoCropBitmap
        ///</summary>
        [TestMethod()]
        [DeploymentItem("samples/vietsample2.png", "samples")]
        public void AutoCropBitmapTest()
        {
            Bitmap source = (Bitmap) Image.FromFile("samples/vietsample2.png");
            Size expectedSize = new Size(2265, 2987);
            Bitmap actual;
            actual = ImageHelper.AutoCrop(source, 0.1);
            Assert.AreEqual(expectedSize, actual.Size);
        }
    }
}
