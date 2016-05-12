using System;
using System.Collections;
using System.Collections.Generic;
using DAL.Operations.BaseClasses;
using DAL.Operations.Enums;
using DAL.Operations.Extensions;
using UnitOfWork.Interfaces.Repository;
using Models.Users;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Interfaces.UnitOfWork;

namespace DAL.Operations.Implementations
{
    public class UserOperations:BaseOpAbstract
    {
        

        public UserOperations(string connectionString) : base(false, connectionString)
        {
        }

        public UserOperations(IUnitOfWork uow, string connectionString)
            : base(true,connectionString)
        {
            Uow = uow;
        }

        private IRepository<User> RetrieveUow()
        {
            if (!IsTest) return this.SetProductionUow(ConnectionString).UserRepository;
            if (((TestUow)Uow) != null) return ((TestUow)Uow).UserRepository;
            return this.SetTestUow(ConnectionString).UserRepository;
        }

        protected override void Any()
        {
            var uow = RetrieveUow();
            if (uow != null) OperationResult.CheckResult = uow.Any(c => c.Id == EntityId, CacheKey);
        }

        protected override void GetById()
        {
            var uow = RetrieveUow();
            if (uow != null) OperationResult.Entity = uow.GetByKey(EntityId, CacheKey);
        }

        protected override void Find(dynamic predicate)
        {
            var uow = RetrieveUow();
            if (uow != null) OperationResult.ListResult = uow.Get(CacheKey, predicate);
        }

        protected void FindByEmail(dynamic predicate)
        {
            Find(predicate);
            OperationResult.CheckResult = (OperationResult.ListResult as List<User>) != null &&
                                          (OperationResult.ListResult as List<User>).Count > 0;
        }

        public override void Perform(MappedOperations desiredOperation, dynamic predicate = null)
        {
            switch (desiredOperation)
            {
                case MappedOperations.Any:
                    Any();
                    break;
                case MappedOperations.GetById:
                    GetById();
                    break;
                case MappedOperations.FindBy:
                    Find(predicate);
                    break;
                case MappedOperations.FindByEmail:
                    FindByEmail(predicate);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(desiredOperation), desiredOperation, null);
            }
        }
    }
}