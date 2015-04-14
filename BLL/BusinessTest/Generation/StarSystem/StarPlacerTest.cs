using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Uows;
using Moq;
using Models;
using Models.Universe;
using BLL.Generation.StarSystem;
using Models.Universe.Strcut;
using System.Collections.Generic;

namespace BusinessTest.Generation.StarSystem
{
    /// <summary>
    /// Summary description for StarPlacerTest
    /// </summary>
    [TestClass]
    public class StarPlacerTest
    {
       
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
            Coordinates coord = placer.GenerateRandomCoordinatesTest(230, 400);
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
                using (MainUow uow = new MainUow(cf, new DalCache()))
                {
                    uow.StarRepository.CustomDbset(new List<Star>());
                    MockRepository repo = new MockRepository(MockBehavior.Default);
                    Mock<IGalaxy> galaxy = repo.Create<IGalaxy>().SetupProperty(x => x.Name, "First Galaxy").SetupProperty(x=>x.Stars, new List<Star>());
                    StarPlacer placer = new StarPlacer(uow, galaxy.Object);
                    Coordinates coord = placer.GenerateRandomCoordinatesTest(230, 400);
                    Assert.IsTrue(placer.ValidPlaceTest(coord));
                }
            }
        }

        [TestMethod]
        public void TestCreation()
        {
            using (ContextFactory cf = new ContextFactory(true))
            {
                using (MainUow uow = new MainUow(cf, new DalCache()))
                {
                    try
                    {
                        uow.StarRepository.CustomDbset(new List<Star>());
                        MockRepository repo = new MockRepository(MockBehavior.Default);
                        Mock<IGalaxy> galaxy = repo.Create<IGalaxy>().SetupProperty(x => x.Name, "First Galaxy");

                        Mock<IStar> star1 = repo.Create<IStar>().SetupProperty(x => x.Coordinate, new Coordinates(50, 50)).SetupProperty(x => x.Universe, galaxy.Object);
                        Mock<IStar> star2 = repo.Create<IStar>().SetupProperty(x => x.Coordinate, new Coordinates(90, 42)).SetupProperty(x => x.Universe, galaxy.Object);
                        Mock<IStar> star3 = repo.Create<IStar>().SetupProperty(x => x.Coordinate, new Coordinates(23, 100)).SetupProperty(x => x.Universe, galaxy.Object);
                        Mock<IStar> star4 = repo.Create<IStar>().SetupProperty(x => x.Coordinate, new Coordinates(0, 0)).SetupProperty(x => x.Universe, galaxy.Object);
                        uow.StarRepository.Add(star1.Object as Star);
                        uow.StarRepository.Add(star2.Object as Star);
                        uow.StarRepository.Add(star3.Object as Star);
                        uow.StarRepository.Add(star4.Object as Star);

                        StarGeneration generator = new StarGeneration();
                        Star generated = generator.CreateBrandNewStar();
                        StarPlacer placer = new StarPlacer(uow, galaxy.Object);
                        placer.Place(generated);
                    }
                    catch (System.Exception ex)
                    {
                        var q = ex.Message;                        
                    }
                }
            }
        }
    }
}
