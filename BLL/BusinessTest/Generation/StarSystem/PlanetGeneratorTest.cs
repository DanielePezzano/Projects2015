using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Universe;
using BLL.Generation.StarSystem;
using Moq;
using BLL.Utilities.Structs;

namespace BusinessTest.Generation.StarSystem
{
    /// <summary>
    /// Summary description for PlanetGeneratorTest
    /// </summary>
    [TestClass]
    public class PlanetGeneratorTest
    {

        private MockRepository _Repo;
        private Mock<Star> star;

        public PlanetGeneratorTest()
        {
            _Repo = new MockRepository(MockBehavior.Default);
            star = _Repo.Create<Star>();
            star.Object.Mass = 0.03;
            star.Object.Radius = 10;
            star.Object.RadiationLevel = 7;
        }

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
            PlanetGenerator generator = new PlanetGenerator(this.star.Object);
            Planet firstPlanet = generator.CreateBrandNewPlanet();
            Assert.IsInstanceOfType(firstPlanet, typeof(Planet));
            DoubleRange closeRange = new DoubleRange(0.1,0.7);
            generator.CompletePlanetGeneration(firstPlanet, new OrbitGenerator(this.star.Object, firstPlanet.Mass, 0, closeRange), closeRange);

            Assert.IsInstanceOfType(firstPlanet.Orbit, typeof(OrbitDetail));
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
            int spaces = generator.AssignTotalSpacesTest(1.0,  5.53,false);
            Assert.IsTrue(spaces<=100 && spaces>=99);
            spaces = generator.AssignTotalSpacesTest(0.05, 5.43, false);
            Assert.IsTrue(spaces <= 5 && spaces >= 4);
            spaces = generator.AssignTotalSpacesTest(755, 0.70, false);
        }
    }
}
