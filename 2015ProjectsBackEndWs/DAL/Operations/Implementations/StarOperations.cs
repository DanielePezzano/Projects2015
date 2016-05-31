using System;
using System.Collections.Generic;
using DAL.Operations.BaseClasses;
using DAL.Operations.Enums;
using DAL.Operations.Extensions;
using Models.Base;
using Models.Universe;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Interfaces.Repository;
using UnitOfWork.Interfaces.UnitOfWork;

namespace DAL.Operations.Implementations
{
    public class StarOperations : BaseOpAbstract
    {
        public StarOperations(string connectionString)
            : base(false, connectionString)
        {
        }

        public StarOperations(IUnitOfWork uow, string connectionString)
            : base(true, connectionString)
        {
            Uow = uow;
        }

        private IRepository<Star> RetrieveUow()
        {
            if (!IsTest)
            {
                Uow = Uow ?? this.SetProductionUow(ConnectionString);
                SetUsedUnitOfWork();
                return ((ProductionUow)Uow).StarRepository;
            }
            if (((TestUow)Uow) != null)
            {
                SetUsedUnitOfWork();
                return ((TestUow)Uow).StarRepository;
            }
            Uow = this.SetTestUow(ConnectionString);
            SetUsedUnitOfWork();
            return ((TestUow)Uow).StarRepository;
        }



        private void ValidPlace(dynamic predicate)
        {
            Find(predicate);
            var list = OperationResult.RawResult as List<Star>;
            OperationResult.CheckResult = list != null && list.Count <= 0;
        }

        protected override void SaveEntity(BaseEntity entity)
        {
            OperationResult.Entity = entity;
            if (OperationResult.Entity == null || (Star) OperationResult.Entity == null) return;
            var repository = RetrieveUow();
            if (repository == null) return;
            repository.Add((Star)OperationResult.Entity);
            Uow.Save();
            OperationResult.RawResult = entity?.Id;
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
                case MappedOperations.ValidStarPlace:
                    ValidPlace(predicate);
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