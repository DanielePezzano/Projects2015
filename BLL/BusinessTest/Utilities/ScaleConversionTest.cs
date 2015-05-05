using BLL.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTest.Utilities
{
    /// <summary>
    ///     Summary description for ScaleConversionTest
    /// </summary>
    [TestClass]
    public class ScaleConversionTest
    {
        private const double Delta = 0.005;

        [TestMethod]
        public void TestScaleConversionFrom10To100()
        {
            using (var conv = new ScaleConversion(10, 100))
            {
                Assert.AreEqual(30.0, conv.Convert(3), Delta);
            }
        }

        [TestMethod]
        public void TestScaleConversionFrom15To10()
        {
            using (var conv = new ScaleConversion(15, 10))
            {
                Assert.AreEqual(2, conv.Convert(3), Delta);
            }
        }

        [TestMethod]
        public void TestScaleConversionFrom20To100()
        {
            using (var conv = new ScaleConversion(20, 100))
            {
                Assert.AreEqual(50, conv.Convert(10), Delta);
            }
        }

        //public ScaleConversionTest()
        //{
        //    //
        //    // TODO: Add constructor logic here
        //    //
        //}

        //private TestContext testContextInstance;

        ///// <summary>
        /////Gets or sets the test context which provides
        /////information about and functionality for the current test run.
        /////</summary>
        //public TestContext TestContext
        //{
        //    get
        //    {
        //        return testContextInstance;
        //    }
        //    set
        //    {
        //        testContextInstance = value;
        //    }
        //}

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion
    }
}