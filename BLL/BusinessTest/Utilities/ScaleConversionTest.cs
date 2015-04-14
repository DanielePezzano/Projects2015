using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL.Utilities;

namespace BusinessTest.Utilities
{
    /// <summary>
    /// Summary description for ScaleConversionTest
    /// </summary>
    [TestClass]
    public class ScaleConversionTest
    {
        const double DELTA = 0.005;
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

        [TestMethod]
        public void TestScaleConversionFrom10To100()
        {
            using (ScaleConversion conv = new ScaleConversion(10, 100))
            {
                Assert.AreEqual(30.0, conv.Convert(3), DELTA);
            }
        }
        [TestMethod]
        public void TestScaleConversionFrom15To10()
        {
            using (ScaleConversion conv = new ScaleConversion(15, 10))
            {
                Assert.AreEqual(2, conv.Convert(3), DELTA);
            }
        }
        [TestMethod]
        public void TestScaleConversionFrom20To100()
        {
            using (ScaleConversion conv = new ScaleConversion(20, 100))
            {
                Assert.AreEqual(50, conv.Convert(10), DELTA);
            }
        }
    }
}
