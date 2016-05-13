using System;
using System.Collections.Generic;
using System.Linq;
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
    class GalaxyOperations : BaseOpAbstract
    {

        public GalaxyOperations(string connectionString)
            : base(false, connectionString)
        {
        }

        public GalaxyOperations(IUnitOfWork uow, string connectionString)
            : base(true,connectionString)
        {
            Uow = uow;
        }

        private IRepository<Galaxy> RetrieveUow()
        {
            if (!IsTest)
            {
                Uow = Uow ?? this.SetProductionUow(ConnectionString);
                SetUsedUnitOfWork();
                return ((ProductionUow)Uow).GalaxyRepository;
            }
            if (((TestUow)Uow) != null)
            {
                SetUsedUnitOfWork();
                return ((TestUow)Uow).GalaxyRepository;
            }
            Uow = this.SetTestUow(ConnectionString);
            SetUsedUnitOfWork();
            return ((TestUow)Uow).GalaxyRepository;
        }

        private void GetNameAndId()
        {
            Find(null);
            var result = OperationResult.RawResult != null ? OperationResult.RawResult as List<Galaxy> : null;
            if (result != null) OperationResult.RawResult = result.Select(c => new {c.Name, c.Id});
        }

        protected override void SaveEntity(BaseEntity entity)
        {
            throw new NotImplementedException();
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
            if (repository != null)
                OperationResult.RawResult = ((List<Galaxy>) repository.Get(CacheKey, predicate)).OrderBy(c => c.Id);
        }

        public override void Perform(MappedOperations desiredOperation, dynamic predicate = null)
        {
            switch (desiredOperation)
            {
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
                case MappedOperations.GetNameAndId:
                    GetNameAndId();
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
