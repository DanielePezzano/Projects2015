﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Uows;
using Moq;
using Models;
using Models.Universe;
using BLL.Generation.StarSystem;
using Models.Universe.Strcut;
using System.Collections.Generic;
using System.Linq;
using System;
using UnitOfWork.Implementations.Uows.UowDto;
using UnitOfWork.Interfaces.Context;
using BLL.Utilities.Structs;

namespace BusinessTest.Generation.StarSystem
{
    /// <summary>
    /// Summary description for StarPlacerTest
    /// </summary>
    [TestClass]
    public class StarPlacerTest
    {
        private static Random rnd;

        public StarPlacerTest()
        {
            if (rnd == null) rnd = new Random();
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
        public void TestCoordinatesGeneration()
        {
            StarPlacer placer = new StarPlacer(null, null);
            Coordinates coord = placer.GenerateRandomCoordinatesTest(230, 400, 230, 400, rnd);
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
            using (ContextFactory cf = new ContextFactory(true))
            {
                IContext context = cf.Retrieve();
                DalCache cache = new DalCache();
                UowRepositories repos = new UowRepositories();
                using (UowRepositoryFactories repoFactories = new UowRepositoryFactories(context, cache, repos))
                {
                    using (MainUow uow = new MainUow(context, cache, repoFactories))
                    {
                        uow.StarRepository.CustomDbset(new List<Star>());
                        MockRepository repo = new MockRepository(MockBehavior.Default);

                        Mock<Galaxy> galaxy = repo.Create<Galaxy>().SetupProperty(x => x.Stars, new List<Star>());
                        StarPlacer placer = new StarPlacer(uow, galaxy.Object);
                        Coordinates coord = placer.GenerateRandomCoordinatesTest(230, 400, 230, 400, rnd);
                        Assert.IsTrue(placer.ValidPlaceTest(coord));
                    }
                }
            }
        }

        [TestMethod]
        public void TestCreation()
        {
            using (ContextFactory cf = new ContextFactory(true))
            {
                IContext context = cf.Retrieve();
                DalCache cache = new DalCache();
                UowRepositories repos = new UowRepositories();
                using (UowRepositoryFactories repoFactories = new UowRepositoryFactories(context, cache, repos))
                {
                    using (MainUow uow = new MainUow(context, cache, repoFactories))
                    {
                        #region Prepare object for test
                        uow.StarRepository.CustomDbset(new List<Star>());
                        MockRepository repo = new MockRepository(MockBehavior.Default);
                        Mock<Galaxy> galaxy = repo.Create<Galaxy>().SetupProperty(x => x.Stars, new List<Star>());

                        Mock<Star> star1 = repo.Create<Star>().SetupProperty(x => x.Galaxy, galaxy.Object);
                        Mock<Star> star2 = repo.Create<Star>().SetupProperty(x => x.Galaxy, galaxy.Object);
                        Mock<Star> star3 = repo.Create<Star>().SetupProperty(x => x.Galaxy, galaxy.Object);
                        Mock<Star> star4 = repo.Create<Star>().SetupProperty(x => x.Galaxy, galaxy.Object);

                        star1.Object.CoordinateX = 50;// = new Coordinates(50, 50);
                        star1.Object.CoordinateY = 50;
                        star2.Object.CoordinateX = 90;//
                        star2.Object.CoordinateY = 42;//= new Coordinates(90, 42);
                        star3.Object.CoordinateX = 23; //
                        star3.Object.CoordinateY = 100; //= new Coordinates(23, 100);
                        star4.Object.CoordinateX = 0; // = new Coordinates(0, 0);
                        star4.Object.CoordinateY = 0;

                        galaxy.SetupProperty(x => x.Stars, new List<Star>() { star1.Object, star2.Object, star3.Object, star4.Object });
                        uow.StarRepository.Add(star1.Object);
                        uow.StarRepository.Add(star2.Object);
                        uow.StarRepository.Add(star3.Object);
                        uow.StarRepository.Add(star4.Object);
                        StarGenerator generator = new StarGenerator();
                        Star generated = generator.CreateBrandNewStar();
                        #endregion

                        StarPlacer placer = new StarPlacer(uow, galaxy.Object);
                        placer.Place(generated, new IntRange(40, 90), new IntRange(40, 90), rnd, string.Empty);
                        //Assert.IsInstanceOfType(generated.Coordinate, typeof(Coordinates));
                    }
                }
            }
        }
    }
}
