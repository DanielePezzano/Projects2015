using BLL.Information;
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
        private MainUow _uow;
        private Mock<User> _user;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _repo = new MockRepository(MockBehavior.Default);
            _user = _repo.Create<User>();
            _user.Object.Email = "danieleTest@test.com";

            _contextFactory = new ContextFactory(true);
            var context = _contextFactory.Retrieve();
            var cache = new DalCache();
            var repos = new UowRepositories();
            var repoFactories = new UowRepositoryFactories(context, cache, repos);
            _uow = new MainUow(context, repoFactories);
            _uow.UserRepository.Add(_user.Object);
        }

        [TestMethod]
        public void TestExistsEmail()
        {
            var user = new RetrieveUserInformation(_uow, "danieleTest@test.com");
            Assert.IsTrue(user.ExistsEmail());
            user = new RetrieveUserInformation(_uow, "danieleddddTest@test.com");
            Assert.IsFalse(user.ExistsEmail());
        }
    }
}