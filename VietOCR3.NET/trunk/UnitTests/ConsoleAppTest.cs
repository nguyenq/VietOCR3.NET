using VietOCR.NET;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace TestProject
{   
    /// <summary>
    ///This is a test class for ConsoleAppTest and is intended
    ///to contain all ConsoleAppTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ConsoleAppTest
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
        ///A test for Main
        ///</summary>
        [TestMethod()]
        [DeploymentItem("samples/vietsample2.png", "samples")]
        [DeploymentItem("x86", "x86")]
        [DeploymentItem("tessdata", "tessdata")]
        public void MainTest()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string outfile = "out";
            string[] args = { "samples/vietsample2.png", outfile, "-l", "vie", "hocr" };
            ConsoleApp.Main(args);
            Assert.IsTrue(File.Exists(Path.Combine(path, outfile + ".html")));
        }
    }
}
