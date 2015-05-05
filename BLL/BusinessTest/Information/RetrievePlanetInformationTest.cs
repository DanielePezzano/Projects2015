using BLL.Information;
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
    public class RetrievePlanetInformationTest
    {
        private ContextFactory _contextFactory;
        private Mock<Planet> _planet1;
        private MockRepository _repo;
        private Mock<Star> _star1;
        private MainUow _uow;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _repo = new MockRepository(MockBehavior.Default);
            _star1 = _repo.Create<Star>();
            _star1.Object.CoordinateX = 50; // = new Coordinates(50, 50);
            _star1.Object.CoordinateY = 50;

            _planet1 = _repo.Create<Planet>().SetupProperty(c => c.Star, _star1.Object);
            _planet1.Object.Name = "Test Planet";
            _planet1.Object.Id = 100;

            _contextFactory = new ContextFactory(true);
            var context = _contextFactory.Retrieve();
            var cache = new DalCache();
            var repos = new UowRepositories();
            var repoFactories = new UowRepositoryFactories(context, cache, repos);
            _uow = new MainUow(context, repoFactories);

            _uow.StarRepository.Add(_star1.Object);
            _uow.PlanetRepository.Add(_planet1.Object);
        }

        [TestMethod]
        public void TestRetrievePlanetInformations()
        {
            using (var info = new RetrievePlanetInformation(_uow, 100))
            {
                Assert.IsNotNull(info.Retrieve(string.Empty));
                Assert.IsTrue(info.Retrieve(string.Empty).Id == 100);
            }
        }
    }
}