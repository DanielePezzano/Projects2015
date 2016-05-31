using System;
using System.Collections.Generic;
using DAL.Operations.BaseClasses;
using DAL.Operations.Enums;
using DAL.Operations.Extensions;
using Models.Base;
using UnitOfWork.Interfaces.Repository;
using Models.Users;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Interfaces.UnitOfWork;

namespace DAL.Operations.Implementations
{
    public class UserOperations : BaseOpAbstract
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
            if (!IsTest)
            {
                Uow = Uow ?? this.SetProductionUow(ConnectionString);
                SetUsedUnitOfWork();
                return ((ProductionUow)Uow).UserRepository;
            }
            if (((TestUow)Uow) != null)
            {
                SetUsedUnitOfWork();
                return ((TestUow)Uow).UserRepository;
            }
            Uow = this.SetTestUow(ConnectionString);
            SetUsedUnitOfWork();
            return ((TestUow)Uow).UserRepository;
        }

        protected override void Any()
        {
            var repository = RetrieveUow();
            if (repository != null) OperationResult.CheckResult = repository.Any(c => c.Id == EntityId, CacheKey);
        }

        protected override void GetById()
        {
            var repository = RetrieveUow();
            if (repository != null) OperationResult.Entity = repository.GetByKey(EntityId, CacheKey);
        }

        protected override void Find(dynamic predicate)
        {
            var repository = RetrieveUow();
            if (repository != null) OperationResult.RawResult = repository.Get(CacheKey, predicate);
        }

        protected override void SaveEntity(BaseEntity entity)
        {
            OperationResult.Entity = entity;
            if (OperationResult.Entity == null || (User)OperationResult.Entity == null) return;
            var repository = RetrieveUow();
            if (repository == null) return;
            repository.Add((User)OperationResult.Entity);
            SaveUow();
            OperationResult.RawResult = entity.Id;
        }

        protected void FindByEmail(dynamic predicate)
        {
            Find(predicate);
            OperationResult.CheckResult = (OperationResult.RawResult as List<User>) != null &&
                                          (OperationResult.RawResult as List<User>).Count > 0;
        }

        protected override void GetAll()
        {
            var repository = RetrieveUow();
            if (repository != null) OperationResult.RawResult = repository.Get(CacheKey);
        }

        public override void Perform(MappedOperations desiredOperation, dynamic predicate = null)
        {
            switch (desiredOperation)
            {
                    case MappedOperations.GetAll:
                    GetAll();
                    break;
                case MappedOperations.SaveEntity:
                    SaveEntity(predicate);
                    break;
                case MappedOperations.Update:
                    break;
                case MappedOperations.Delete:
                    break;
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
                case MappedOperations.SaveUow:
                    SaveUow();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(desiredOperation), desiredOperation, null);
            }
        }
    }
}