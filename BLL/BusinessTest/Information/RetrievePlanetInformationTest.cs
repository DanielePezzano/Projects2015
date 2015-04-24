using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Models.Universe;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Uows;
using System.Collections.Generic;
using UnitOfWork.Interfaces.Context;
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Uows.UowDto;
using BLL.Information;
using BLL.Utilities.Structs;

namespace BusinessTest.Information
{
    [TestClass]
    public class RetrievePlanetInformationTest
    {
        private MockRepository _Repo;
        private ContextFactory _ContextFactory;
        private Mock<Star> _Star1;
        private Mock<Planet> _Planet1;
        private MainUow _uow;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            _Repo = new MockRepository(MockBehavior.Default);
            _Star1 = _Repo.Create<Star>();
            _Star1.Object.CoordinateX = 50;// = new Coordinates(50, 50);
            _Star1.Object.CoordinateY = 50;

            _Planet1 = _Repo.Create<Planet>().SetupProperty(c => c.Star, _Star1.Object);
            _Planet1.Object.Name = "Test Planet";
            _Planet1.Object.Id = 100;

            _ContextFactory = new ContextFactory(true);
            IContext context = _ContextFactory.Retrieve();
            DalCache cache = new DalCache();
            UowRepositories repos = new UowRepositories();
            UowRepositoryFactories repoFactories = new UowRepositoryFactories(context, cache, repos);
            _uow = new MainUow(context, cache, repoFactories);

            _uow.StarRepository.Add(_Star1.Object);
            _uow.PlanetRepository.Add(_Planet1.Object);
        }

        [TestMethod]
        public void TestRetrievePlanetInformations()
        {
            using (RetrievePlanetInformation info = new RetrievePlanetInformation(_uow, 100))
            {
                Assert.IsNotNull(info.Retrieve(string.Empty));
                Assert.IsTrue(info.Retrieve(string.Empty).Id == 100);
            }
        }
    }
}
