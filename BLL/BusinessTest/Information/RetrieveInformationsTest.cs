﻿using System;
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
    public class RetrieveInformationsTest
    {
        private MockRepository _Repo;
        private Mock<Galaxy> _Galaxy;
        private Mock<Star> _Star1;
        private Mock<Star> _Star2;
        private Mock<Star> _Star3;
        private Mock<Star> _Star4;
        private ContextFactory _ContextFactory;
        private MainUow _uow;

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
            IContext context = _ContextFactory.Retrieve();
            DalCache cache = new DalCache();
            UowRepositories repos = new UowRepositories();
            UowRepositoryFactories repoFactories = new UowRepositoryFactories(context, cache, repos);
            _uow = new MainUow(context, cache, repoFactories);

            _uow.StarRepository.Add(_Star1.Object);
            _uow.StarRepository.Add(_Star2.Object);
            _uow.StarRepository.Add(_Star3.Object);
            _uow.StarRepository.Add(_Star4.Object);

        }

        [TestMethod]
        public void TestStarsInRange()
        {
            RetrieveInformations retrieve = new RetrieveInformations(_uow, new IntRange(20, 50), new IntRange(0, 100));
            List<Star> stars = retrieve.StarsInRange(string.Empty);
            Assert.AreEqual(2, stars.Count);
        }
    }
}