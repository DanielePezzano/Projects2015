﻿using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Generation.StarSystem;
using BLL.Utilities.Structs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Universe;
using Moq;

namespace BusinessTest.Generation.StarSystem
{
    /// <summary>
    ///     Summary description for PlanetGeneratorTest
    /// </summary>
    [TestClass]
    public class PlanetGeneratorTest
    {
        private readonly Mock<Planet> _planetHostile;
        private readonly Mock<Star> _star;


        public PlanetGeneratorTest()
        {
            if (OrbitGeneratorTest.Rnd == null) OrbitGeneratorTest.Rnd = new Random(Environment.TickCount);
            var repo = new MockRepository(MockBehavior.Default);
            _star = repo.Create<Star>();
            _star.Object.Mass = 0.03;
            _star.Object.Radius = 10;
            _star.Object.RadiationLevel = 7;
            _star.Object.SurfaceTemp = 5778;

            _planetHostile = repo.Create<Planet>();
            var planetHabitable = repo.Create<Planet>();
            _planetHostile.SetupProperty(c => c.Star, _star.Object);
            planetHabitable.SetupProperty(c => c.Star, _star.Object);

            var closeOrbit = repo.Create<OrbitDetail>();
            closeOrbit.Object.DistanceR = 0.3;
            var mediumOrbit = repo.Create<OrbitDetail>();
            mediumOrbit.Object.DistanceR = 1.4;

            _planetHostile.Object.AtmospherePresent = false;
            _planetHostile.Object.Orbit = closeOrbit.Object;

            planetHabitable.Object.AtmospherePresent = true;
            planetHabitable.Object.Orbit = mediumOrbit.Object;
        }

        //[TestMethod]
        //[Obsolete]
        //public void TestAssignSurfaceTemperature()
        //{
        //    var generator = new PlanetGenerator(_star.Object, new PlanetCustomConditions());
        //    var temp1 = generator.AssignSurfaceTemperatureTest(_planetHostile.Object.Orbit.DistanceR,
        //        _planetHostile.Object.AtmospherePresent, _star.Object.SurfaceTemp);
        //    Assert.AreEqual(570, temp1, "Temp non uguali: atteso: 570, ottenuto : " + temp1);
        //}

        //[TestMethod]
        //[Obsolete]
        //public void TestGeneratePlanet()
        //{
        //    var generator = new PlanetGenerator(_star.Object, new PlanetCustomConditions());
        //    var firstPlanet = generator.CreateBrandNewPlanet(OrbitGeneratorTest.Rnd);
        //    Assert.IsInstanceOfType(firstPlanet, typeof (Planet));
        //    var closeRange = new DoubleRange(0.1, 0.7);
        //    generator.CompletePlanetGeneration(firstPlanet, new OrbitGenerator(_star.Object, 0, closeRange), closeRange,
        //        OrbitGeneratorTest.Rnd);
        //    Assert.IsTrue(Math.Abs(firstPlanet.GravityEarthCompared - firstPlanet.Mass) < 0.001);
        //    Assert.IsInstanceOfType(firstPlanet.Orbit, typeof (OrbitDetail));
        //}

        //[TestMethod]
        //[Obsolete]
        //public void TestCalculateRadiationLevel()
        //{
        //    var generator = new PlanetGenerator(null, new PlanetCustomConditions());
        //    var radiationLevel = generator.CalculateRadiationLevelTest(false, 4, 1.4);
        //    Assert.IsTrue(radiationLevel > 3);
        //    radiationLevel = generator.CalculateRadiationLevelTest(true, 4, 1.4);
        //    Assert.IsTrue(radiationLevel < 3);
        //    radiationLevel = generator.CalculateRadiationLevelTest(false, 15, 1.4);
        //    Assert.IsTrue(radiationLevel >= 10);
        //    radiationLevel = generator.CalculateRadiationLevelTest(true, 15, 1.4);
        //    Assert.IsTrue(radiationLevel < 10);

        //    radiationLevel = generator.CalculateRadiationLevelTest(true, 15, 0.5);
        //    Assert.IsTrue(radiationLevel < 9);
        //    radiationLevel = generator.CalculateRadiationLevelTest(false, 15, 0.5);
        //    Assert.IsTrue(radiationLevel >= 8);
        //    radiationLevel = generator.CalculateRadiationLevelTest(true, 15, 11.4);
        //    Assert.IsTrue(radiationLevel < 3);
        //    radiationLevel = generator.CalculateRadiationLevelTest(false, 15, 11.4);
        //    Assert.IsTrue(radiationLevel > 3);
        //}

        //[TestMethod]
        //[Obsolete]
        //public void TestAssignTotalSpaces()
        //{
        //    var generator = new PlanetGenerator(null, new PlanetCustomConditions());
        //    var spaces = generator.AssignTotalSpacesTest(1.0, 5.53, false);
        //    Assert.IsTrue(spaces <= 100 && spaces >= 99);
        //    spaces = generator.AssignTotalSpacesTest(0.05, 5.43, false);
        //    Assert.IsTrue(spaces <= 5 && spaces >= 4);
        //    generator.AssignTotalSpacesTest(755, 0.70, false);
        //}

        [TestMethod]
        public void TestSolarGeneration()
        {
            var closeRange = new DoubleRange(0.1, 0.7);
            var factory = new SolarSystemFactory(_star.Object, new PlanetCustomConditions(), OrbitGeneratorTest.Rnd, new OrbitGenerator(_star.Object, closeRange),1);
            factory.Construct();

            List<Planet> generatedPlanets = factory.RetrievePlanets();

            Assert.IsInstanceOfType(generatedPlanets.FirstOrDefault(), typeof(Planet));
            Assert.IsNotNull(generatedPlanets.FirstOrDefault());
            Assert.IsTrue(Math.Abs(generatedPlanets.First().GravityEarthCompared - generatedPlanets.First().Mass) < 0.001);
            Assert.IsInstanceOfType(generatedPlanets.First().Orbit, typeof(OrbitDetail));
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
    }
}