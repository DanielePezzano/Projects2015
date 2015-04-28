using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Uows;
using Models.Users;
using UnitOfWork.Interfaces.Context;
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Uows.UowDto;
using BLL.Information;

namespace BusinessTest.Information
{
    [TestClass]
    public class RetrieveUserInformationTest
    {
        private MockRepository _Repo;
        private ContextFactory _ContextFactory;
        private Mock<User> _User;
        private MainUow _uow;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            _Repo = new MockRepository(MockBehavior.Default);
            _User = _Repo.Create<User>();
            _User.Object.Email = "danieleTest@test.com";

            _ContextFactory = new ContextFactory(true);
            IContext context = _ContextFactory.Retrieve();
            DalCache cache = new DalCache();
            UowRepositories repos = new UowRepositories();
            UowRepositoryFactories repoFactories = new UowRepositoryFactories(context, cache, repos);
            _uow = new MainUow(context, cache, repoFactories);
            _uow.UserRepository.Add(_User.Object);
        }

        [TestMethod]
        public void TestExistsEmail()
        {
            RetrieveUserInformation user = new RetrieveUserInformation(this._uow, "danieleTest@test.com");
            Assert.IsTrue(user.ExistsEmail());
            user = new RetrieveUserInformation(this._uow, "danieleddddTest@test.com");
            Assert.IsFalse(user.ExistsEmail());
        }
    }
}
