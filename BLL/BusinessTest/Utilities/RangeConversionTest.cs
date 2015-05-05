using BLL.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTest.Utilities
{
    /// <summary>
    ///     Summary description for RangeConversionTest
    /// </summary>
    [TestClass]
    public class RangeConversionTest
    {
        private const double Delta = 0.005;

        [TestMethod]
        public void DoConversionTest()
        {
            using (var conv = new RangeConversion(3, 5, new ScaleConversion(14, 2)))
            {
                Assert.AreEqual(3.71, conv.DoConversion(5), Delta);
            }
        }

        //public RangeConversionTest()
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