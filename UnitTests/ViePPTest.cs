using VietOCR.NET.Postprocessing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TestProject
{
    
    
    /// <summary>
    ///This is a test class for ViePPTest and is intended
    ///to contain all ViePPTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ViePPTest
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
        ///A test for PostProcess
        ///</summary>
        [TestMethod()]
        public void PostProcessTest()
        {
            ViePP target = new ViePP();
            string text = "uơn";
            string expected = "ươn";
            string actual;
            actual = target.PostProcess(text);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for PostProcess
        ///</summary>
        [TestMethod()]
        public void PostProcessTest1()
        {
            ViePP target = new ViePP();
            string text = "ưon";
            string expected = "ươn";
            string actual;
            actual = target.PostProcess(text);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for PostProcess
        ///</summary>
        [TestMethod()]
        public void PostProcessTest2()
        {
            ViePP target = new ViePP();
            string text = "‘ê ‘ổ ‘ô";
            string expected = "ề ‘ổ ồ";
            string actual;
            actual = target.PostProcess(text);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for PostProcess
        ///</summary>
        [TestMethod()]
        public void PostProcessTest3()
        {
            ViePP target = new ViePP();
            string text = "ê’ ê’n ậ’n";
            string expected = "ế ến ậ’n";
            string actual;
            actual = target.PostProcess(text);
            Assert.AreEqual(expected, actual);
        }
    }
}
