using System;
using System.Collections.Generic;
using BLL.Generation.StarSystem;
using BLL.Generation.StarSystem.Builders;
using BLL.Utilities.Structs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Universe;
using Moq;
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Implementations.Uows.UowDto;

namespace BusinessTest.Generation.StarSystem
{
    /// <summary>
    ///     Summary description for StarPlacerTest
    /// </summary>
    [TestClass]
    public class StarPlacerTest
    {
        private static Random _rnd;

        public StarPlacerTest()
        {
            if (_rnd == null) _rnd = new Random();
        }

        [TestMethod]
        public void TestCoordinatesGeneration()
        {
            var placer = new StarPlacer(null);
            var coord = placer.GenerateRandomCoordinatesTest(230, 400, 230, 400, _rnd);
            Assert.IsTrue(coord.X > 0);
            Assert.IsTrue(coord.Y > 0);
            Assert.IsTrue(coord.X >= 230);
            Assert.IsTrue(coord.X <= 400);
            Assert.IsTrue(coord.Y >= 230);
            Assert.IsTrue(coord.Y <= 400);
        }

        [TestMethod]
        public void TestValidPlace()
        {
            using (var cf = new ContextFactory(true))
            {
                var context = cf.Retrieve();
                var cache = new DalCache();
                var repos = new UowRepositories();
                using (var repoFactories = new UowRepositoryFactories(context, cache, repos))
                {
                    using (var uow = new MainUow(context, repoFactories))
                    {
                        uow.StarRepository.CustomDbset(new List<Star>());
                        var repo = new MockRepository(MockBehavior.Default);

                        repo.Create<Galaxy>().SetupProperty(x => x.Stars, new List<Star>());
                        var placer = new StarPlacer(uow);
                        var coord = placer.GenerateRandomCoordinatesTest(230, 400, 230, 400, _rnd);
                        Assert.IsTrue(placer.ValidPlaceTest(coord));
                    }
                }
            }
        }

        [TestMethod]
        public void TestCreation()
        {
            using (var cf = new ContextFactory(true))
            {
                var context = cf.Retrieve();
                var cache = new DalCache();
                var repos = new UowRepositories();
                using (var repoFactories = new UowRepositoryFactories(context, cache, repos))
                {
                    using (var uow = new MainUow(context, repoFactories))
                    {
                        #region Prepare object for test

                        uow.StarRepository.CustomDbset(new List<Star>());
                        var repo = new MockRepository(MockBehavior.Default);
                        var galaxy = repo.Create<Galaxy>().SetupProperty(x => x.Stars, new List<Star>());

                        var star1 = repo.Create<Star>().SetupProperty(x => x.Galaxy, galaxy.Object);
                        var star2 = repo.Create<Star>().SetupProperty(x => x.Galaxy, galaxy.Object);
                        var star3 = repo.Create<Star>().SetupProperty(x => x.Galaxy, galaxy.Object);
                        var star4 = repo.Create<Star>().SetupProperty(x => x.Galaxy, galaxy.Object);

                        star1.Object.CoordinateX = 50; // = new Coordinates(50, 50);
                        star1.Object.CoordinateY = 50;
                        star2.Object.CoordinateX = 90; //
                        star2.Object.CoordinateY = 42; //= new Coordinates(90, 42);
                        star3.Object.CoordinateX = 23; //
                        star3.Object.CoordinateY = 100; //= new Coordinates(23, 100);
                        star4.Object.CoordinateX = 0; // = new Coordinates(0, 0);
                        star4.Object.CoordinateY = 0;

                        galaxy.SetupProperty(x => x.Stars,
                            new List<Star> {star1.Object, star2.Object, star3.Object, star4.Object});
                        uow.StarRepository.Add(star1.Object);
                        uow.StarRepository.Add(star2.Object);
                        uow.StarRepository.Add(star3.Object);
                        uow.StarRepository.Add(star4.Object);
                        var generator = new StarBuilder();
                        var generated = generator.CreateBrandNewStar();

                        #endregion

                        var placer = new StarPlacer(uow);
                        placer.Place(generated, new IntRange(40, 90), new IntRange(40, 90), _rnd, string.Empty);
                        //Assert.IsInstanceOfType(generated.Coordinate, typeof(Coordinates));
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