using BLL.Information;
using BLL.Utilities.Structs;
using DAL.Operations.IstanceFactory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Universe;
using Moq;
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Implementations.Uows.UowDto;

namespace BusinessTest.Information
{
    [TestClass]
    public class RetrieveInformationsTest
    {
        private ContextFactory _contextFactory;
        private Mock<Galaxy> _galaxy;
        private MockRepository _repo;
        private Mock<Star> _star1;
        private Mock<Star> _star2;
        private Mock<Star> _star3;
        private Mock<Star> _star4;
        private TestUow _uow;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _repo = new MockRepository(MockBehavior.Default);
            _star1 = _repo.Create<Star>();
            _star2 = _repo.Create<Star>();
            _star3 = _repo.Create<Star>();
            _star4 = _repo.Create<Star>();

            _star1.Object.CoordinateX = 50; // = new Coordinates(50, 50);
            _star1.Object.CoordinateY = 50;
            _star2.Object.CoordinateX = 90; //
            _star2.Object.CoordinateY = 42; //= new Coordinates(90, 42);
            _star3.Object.CoordinateX = 23; //
            _star3.Object.CoordinateY = 100; //= new Coordinates(23, 100);
            _star4.Object.CoordinateX = 0; // = new Coordinates(0, 0);
            _star4.Object.CoordinateY = 0;

           
            _contextFactory = new ContextFactory("UniverseConnection", true);
            var context = _contextFactory.Retrieve();
            var cache = new DalCache();
            var repos = new UowRepositories();
            var repoFactories = new UowRepositoryFactories(context, cache, repos);
            _uow = new TestUow(context, repoFactories);

            _uow.StarRepository.Add(_star1.Object);
            _uow.StarRepository.Add(_star2.Object);
            _uow.StarRepository.Add(_star3.Object);
            _uow.StarRepository.Add(_star4.Object);
        }

        [TestMethod]
        public void TestStarsInRange()
        {
            var retrieve = new RetrieveInformations(new IntRange(20, 50), new IntRange(0, 100), IstancesCreator.RetrieveOpFactory("UniverseConnection", true));
            var stars = retrieve.StarsInRange(string.Empty, _uow);
            Assert.AreEqual(2, stars.Count);
        }
    }
}