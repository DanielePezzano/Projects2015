using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Universe;
using BLL.Generation.StarSystem;
using Moq;

namespace BusinessTest.Generation.StarSystem
{
    /// <summary>
    /// Summary description for PlanetGeneratorTest
    /// </summary>
    [TestClass]
    public class PlanetGeneratorTest
    {
        //public PlanetGeneratorTest()
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
        public void TestGeneratePlanet()
        {
            PlanetGenerator generator = new PlanetGenerator(null);
            Assert.IsInstanceOfType(generator.CreateBrandNewPlanet(), typeof(Planet));
        }

        [TestMethod]
        public void TestCalculateRadiationLevel()
        {
            PlanetGenerator generator = new PlanetGenerator(null);
            int radiationLevel = generator.CalculateRadiationLevelTest(false, 4, 1.4);
            Assert.IsTrue(radiationLevel > 3);
            radiationLevel = generator.CalculateRadiationLevelTest(true, 4, 1.4);
            Assert.IsTrue(radiationLevel < 3);
            radiationLevel = generator.CalculateRadiationLevelTest(false, 15, 1.4);
            Assert.IsTrue(radiationLevel >= 10);
            radiationLevel = generator.CalculateRadiationLevelTest(true, 15, 1.4);
            Assert.IsTrue(radiationLevel < 10);

            radiationLevel = generator.CalculateRadiationLevelTest(true, 15, 0.5);
            Assert.IsTrue(radiationLevel < 9);
            radiationLevel = generator.CalculateRadiationLevelTest(false, 15, 0.5);
            Assert.IsTrue(radiationLevel >=8);
            radiationLevel = generator.CalculateRadiationLevelTest(true, 15, 11.4);
            Assert.IsTrue(radiationLevel < 3);
            radiationLevel = generator.CalculateRadiationLevelTest(false, 15, 11.4);
            Assert.IsTrue(radiationLevel > 3);
        }
        [TestMethod]
        public void TestAssignTotalSpaces()
        {
            PlanetGenerator generator = new PlanetGenerator(null);
            int spaces = generator.AssignTotalSpacesTest(1.0,  5.53);
            Assert.IsTrue(spaces<=100 && spaces>=99);
            spaces = generator.AssignTotalSpacesTest(0.05, 5.43);
            Assert.IsTrue(spaces <= 5 && spaces >= 4);
            spaces = generator.AssignTotalSpacesTest(755, 0.70);
        }
    }
}
