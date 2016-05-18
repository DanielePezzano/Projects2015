using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Generation.StarSystem.IstanceFactory;
using DAL.Operations.IstanceFactory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Universe;
using Moq;
using SharedDto.Universe.Planets;
using SharedDto.UtilityDto;
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Implementations.Uows.UowDto;

namespace BusinessTest.Generation.StarSystem
{
    /// <summary>
    ///     Summary description for PlanetGeneratorTest
    /// </summary>
    [TestClass]
    public class PlanetGeneratorTest
    {
        internal readonly Mock<Star> Star;
        

        public PlanetGeneratorTest()
        {
            if (OrbitGeneratorTest.Rnd == null) OrbitGeneratorTest.Rnd = new Random(Environment.TickCount);
            var repo = new MockRepository(MockBehavior.Default);
            Star = repo.Create<Star>();
            Star.Object.Mass = 0.03;
            Star.Object.Radius = 10;
            Star.Object.RadiationLevel = 7;
            Star.Object.SurfaceTemp = 5778;

            var planetHostile = repo.Create<Planet>();
            var planetHabitable = repo.Create<Planet>();
            planetHostile.SetupProperty(c => c.Star, Star.Object);
            planetHabitable.SetupProperty(c => c.Star, Star.Object);

            var closeOrbit = repo.Create<OrbitDetail>();
            closeOrbit.Object.DistanceR = 0.3;
            var mediumOrbit = repo.Create<OrbitDetail>();
            mediumOrbit.Object.DistanceR = 1.4;

            planetHostile.Object.AtmospherePresent = false;
            planetHostile.Object.Orbit = closeOrbit.Object;

            planetHabitable.Object.AtmospherePresent = true;
            planetHabitable.Object.Orbit = mediumOrbit.Object;
        }

        [TestMethod]
        public void TestPlanetGeneration()
        {
            using (var cf = new ContextFactory("UniverseConnection", true))
            {
                var context = cf.Retrieve();
                var cache = new DalCache();
                var repos = new UowRepositories();
                using (var repoFactories = new UowRepositoryFactories(context, cache, repos))
                {
                    using (var uow = new TestUow(context, repoFactories))
                    {
                        uow.StarRepository.CustomDbset(new List<Star>());

                        var generator = FactoryGenerator.RetrieveStarSystemGenerator(
                            OrbitGeneratorTest.Rnd,  
                            new SystemGenerationDto()
                            {
                                FoodPoor = false,
                                FoodRich = false,
                                ForceWater = false,
                                ForceLiving = true,
                                MostlyWater = false,
                                MineralRich = false,
                                MineralPoor = false,
                                MinX = 0,MaxX = 10,MinY = 0,MaxY = 10

                            }, IstancesCreator.RetrieveOpFactory("UniverseConnection", true));

                        var star = generator.Generate(1,  "",uow);

                        if (star.Planets.Count <= 0) return;
                        var generatedPlanets = star.Planets.ToList();

                        Assert.IsInstanceOfType(generatedPlanets.FirstOrDefault(), typeof (PlanetDto));
                        Assert.IsNotNull(generatedPlanets.FirstOrDefault());
                    }
                }
            }
            
        }

        [TestMethod]
        public void TestOneLivingPlanet()
        {
            using (var cf = new ContextFactory("UniverseConnection", true))
            {
                var context = cf.Retrieve();
                var cache = new DalCache();
                var repos = new UowRepositories();
                using (var repoFactories = new UowRepositoryFactories(context, cache, repos))
                {
                    using (var uow = new TestUow(context, repoFactories))
                    {
                        uow.StarRepository.CustomDbset(new List<Star>());

                        var generator = FactoryGenerator.RetrieveStarSystemGenerator(
                            OrbitGeneratorTest.Rnd, new SystemGenerationDto()
                            {
                                FoodPoor = false,
                                FoodRich = false,
                                ForceWater = false,
                                ForceLiving = true,
                                MostlyWater = false,
                                MineralRich = false,
                                MineralPoor = false,
                                MinX = 0,
                                MaxX = 10,
                                MinY = 0,
                                MaxY = 10

                            }, IstancesCreator.RetrieveOpFactory("UniverseConnection", true));

                        var star = generator.Generate(1, "", uow);

                        var generatedPlanets = star.Planets.ToList();
                        Assert.IsTrue(generatedPlanets.Count(c => c.IsHabitable) >= 1);
                        Assert.IsTrue(generatedPlanets.Where(c=>c.IsHabitable).Select(c=>c.MaxPopulation).First()>0);
                    }
                }
            }
        }
        
         [TestMethod]
        public void TestOneMostlyWater()
        {
            using (var cf = new ContextFactory("UniverseConnection", true))
            {
                var context = cf.Retrieve();
                var cache = new DalCache();
                var repos = new UowRepositories();
                using (var repoFactories = new UowRepositoryFactories(context, cache, repos))
                {
                    using (var uow = new TestUow(context, repoFactories))
                    {
                        uow.StarRepository.CustomDbset(new List<Star>());

                        var generator = FactoryGenerator.RetrieveStarSystemGenerator(
                            OrbitGeneratorTest.Rnd, new SystemGenerationDto()
                            {
                                FoodPoor = false,
                                FoodRich = false,
                                ForceWater = false,
                                ForceLiving = false,
                                MostlyWater = true,
                                MineralRich = false,
                                MineralPoor = false,
                                MinX = 0,
                                MaxX = 10,
                                MinY = 0,
                                MaxY = 10

                            }, IstancesCreator.RetrieveOpFactory("UniverseConnection", true));

                        var star = generator.Generate(1, "", uow);

                        var generatedPlanets = star.Planets.ToList();
                        Console.WriteLine(@"pianeti " + generatedPlanets.Count(c => c.WaterSpaces + c.WaterRadiatedSpaces > c.GroundSpaces));
                        var check =
                            generatedPlanets.Count(
                                c => c.WaterSpaces + c.WaterRadiatedSpaces > c.GroundSpaces) >= 1;
                        Assert.IsTrue(check);
                    }
                }
            }
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