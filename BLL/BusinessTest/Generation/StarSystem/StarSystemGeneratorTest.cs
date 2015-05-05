﻿using System;
using System.Collections.Generic;
using BLL.Generation.StarSystem;
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
    ///     Summary description for StarSystemGeneratorTest
    /// </summary>
    [TestClass]
    public class StarSystemGeneratorTest
    {
        private ContextFactory _contextFactory;
        private Mock<Galaxy> _galaxy;
        private MockRepository _repo;
        private Mock<Star> _star1;
        private Mock<Star> _star2;
        private Mock<Star> _star3;
        private Mock<Star> _star4;
        private MainUow _uow;

        public StarSystemGeneratorTest()
        {
            if (OrbitGeneratorTest.Rnd == null) OrbitGeneratorTest.Rnd = new Random(Environment.TickCount);
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
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{

        //}
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize]
        public void MyTestInitialize()
        {
            _repo = new MockRepository(MockBehavior.Default);
            _galaxy = _repo.Create<Galaxy>().SetupProperty(x => x.Stars, new List<Star>());
            _star1 = _repo.Create<Star>().SetupProperty(x => x.Galaxy, _galaxy.Object);
            _star2 = _repo.Create<Star>().SetupProperty(x => x.Galaxy, _galaxy.Object);
            _star3 = _repo.Create<Star>().SetupProperty(x => x.Galaxy, _galaxy.Object);
            _star4 = _repo.Create<Star>().SetupProperty(x => x.Galaxy, _galaxy.Object);

            _star1.Object.CoordinateX = 50; // = new Coordinates(50, 50);
            _star1.Object.CoordinateY = 50;
            _star2.Object.CoordinateX = 90; //
            _star2.Object.CoordinateY = 42; //= new Coordinates(90, 42);
            _star3.Object.CoordinateX = 23; //
            _star3.Object.CoordinateY = 100; //= new Coordinates(23, 100);
            _star4.Object.CoordinateX = 0; // = new Coordinates(0, 0);
            _star4.Object.CoordinateY = 0;

            _galaxy.SetupProperty(x => x.Stars,
                new List<Star> {_star2.Object, _star3.Object, _star1.Object, _star4.Object});
            _contextFactory = new ContextFactory(true);
            var context = _contextFactory.Retrieve();
            var cache = new DalCache();
            var repos = new UowRepositories();
            var repoFactories = new UowRepositoryFactories(context, cache, repos);
            _uow = new MainUow(context, repoFactories);

            _uow.StarRepository.Add(_star1.Object);
            _uow.StarRepository.Add(_star2.Object);
            _uow.StarRepository.Add(_star3.Object);
            _uow.StarRepository.Add(_star4.Object);
        }

        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        [TestMethod]
        public void TestGenerate()
        {
            var systemGenerator = new StarSystemGenerator(
                new StarGenerator(),
                new StarPlacer(_uow),
                new IntRange(40, 90),
                new IntRange(40, 90),
                new PlanetCustomConditions());

            var starWithSystem = systemGenerator.Generate(OrbitGeneratorTest.Rnd, string.Empty);

            Assert.IsInstanceOfType(starWithSystem, typeof (Star));
        }
    }
}