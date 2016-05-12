using BLL.Information;
using DAL.Operations.IstanceFactory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Users;
using Moq;
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Implementations.Uows.UowDto;

namespace BusinessTest.Information
{
    [TestClass]
    public class RetrieveUserInformationTest
    {
        private ContextFactory _contextFactory;
        private MockRepository _repo;
        private TestUow _uow;
        private Mock<User> _user;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _repo = new MockRepository(MockBehavior.Default);
            _user = _repo.Create<User>();
            _user.Object.Email = "danieleTest@test.com";

            _contextFactory = new ContextFactory("UniverseConnection", true);
            var context = _contextFactory.Retrieve();
            var cache = new DalCache();
            var repos = new UowRepositories();
            var repoFactories = new UowRepositoryFactories(context, cache, repos);
            _uow = new TestUow(context, repoFactories);
            _uow.UserRepository.Add(_user.Object);
        }

        [TestMethod]
        public void MailShouldExists()
        {
            var user = new RetrieveUserInformation("danieleTest@test.com", IstancesCreator.RetrieveOpFactory("UniverseConnection", true));

            Assert.IsTrue(user.ExistsEmail(_uow));
            
        }

        [TestMethod]
        public void MailShouldNotExists()
        {
            var user = new RetrieveUserInformation("danieleddddTest@test.com", IstancesCreator.RetrieveOpFactory("UniverseConnection", true));
            Assert.IsFalse(user.ExistsEmail(_uow));
        }
    }
}