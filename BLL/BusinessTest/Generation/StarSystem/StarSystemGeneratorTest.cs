using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Models.Universe;
using Models.Universe.Strcut;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Cache;
using BLL.Generation.StarSystem;
using System.Runtime.Serialization.Json;
using System.IO;

namespace BusinessTest.Generation.StarSystem
{
    /// <summary>
    /// Summary description for StarSystemGeneratorTest
    /// </summary>
    [TestClass]
    public class StarSystemGeneratorTest
    {
        private MockRepository _Repo;
        private Mock<Galaxy> _Galaxy;
        private Mock<Star> _Star1;
        private Mock<Star> _Star2;
        private Mock<Star> _Star3;
        private Mock<Star> _Star4;
        private ContextFactory _ContextFactory;
        private MainUow _uow;

        public StarSystemGeneratorTest()
        {
            if (OrbitGeneratorTest._Rnd == null) OrbitGeneratorTest._Rnd = new Random(Environment.TickCount);
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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            _Repo = new MockRepository(MockBehavior.Default);
            _Galaxy = _Repo.Create<Galaxy>().SetupProperty(x => x.Stars, new List<Star>());
            _Star1 = _Repo.Create<Star>().SetupProperty(x => x.Galaxy, _Galaxy.Object);
            _Star2 = _Repo.Create<Star>().SetupProperty(x => x.Galaxy, _Galaxy.Object);
            _Star3 = _Repo.Create<Star>().SetupProperty(x => x.Galaxy, _Galaxy.Object);
            _Star4 = _Repo.Create<Star>().SetupProperty(x => x.Galaxy, _Galaxy.Object);

            _Star1.Object.CoordinateX = 50;// = new Coordinates(50, 50);
            _Star1.Object.CoordinateY = 50;
            _Star2.Object.CoordinateX = 90;//
            _Star2.Object.CoordinateY = 42;//= new Coordinates(90, 42);
            _Star3.Object.CoordinateX = 23; //
            _Star3.Object.CoordinateY = 100; //= new Coordinates(23, 100);
            _Star4.Object.CoordinateX = 0; // = new Coordinates(0, 0);
            _Star4.Object.CoordinateY = 0;

            _Galaxy.SetupProperty(x => x.Stars, new List<Star>() { _Star2.Object, _Star3.Object, _Star1.Object, _Star4.Object });
            _ContextFactory = new ContextFactory(true);
            _uow = new MainUow(_ContextFactory, new DalCache());

            _uow.StarRepository.Add(_Star1.Object);
            _uow.StarRepository.Add(_Star2.Object);
            _uow.StarRepository.Add(_Star3.Object);
            _uow.StarRepository.Add(_Star4.Object);
        }

        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestGenerate()
        {
            StarSystemGenerator systemGenerator = new StarSystemGenerator(
                new StarGenerator(),
                new StarPlacer(_uow, _Galaxy.Object), false, false, false, 40, 90, 40, 90, false, false, false, false);

            Star starWithSystem = systemGenerator.Generate(OrbitGeneratorTest._Rnd,string.Empty);

            Assert.IsInstanceOfType(starWithSystem, typeof(Star));
                        
        }
    }
}
