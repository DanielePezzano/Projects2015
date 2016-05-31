using System;
using DAL.Operations.BaseClasses;
using DAL.Operations.Enums;
using DAL.Operations.Extensions;
using Models.Base;
using Models.Fleets.ShipClasses.Weapons;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Interfaces.Repository;
using UnitOfWork.Interfaces.UnitOfWork;

namespace DAL.Operations.Implementations
{
    public class AntiPlanetWpOperations:BaseOpAbstract
    {
        public AntiPlanetWpOperations(string connectionString) : base(false, connectionString)
        {
        }

        public AntiPlanetWpOperations(IUnitOfWork uow, string connectionString)
            : base(true,connectionString)
        {
            Uow = uow;
        }

        private IRepository<AntiPlanetWeapon> RetrieveUow()
        {
            if (!IsTest)
            {
                Uow = Uow ?? this.SetProductionUow(ConnectionString);
                SetUsedUnitOfWork();
                return ((ProductionUow)Uow).AntiPlanetWeaponRepository;
            }
            if (((TestUow) Uow) != null)
            {
                SetUsedUnitOfWork();
                return ((TestUow)Uow).AntiPlanetWeaponRepository;
            }
            Uow = this.SetTestUow(ConnectionString);
            SetUsedUnitOfWork();
            return ((TestUow)Uow).AntiPlanetWeaponRepository;
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

        protected override void SaveEntity(BaseEntity entity)
        {
            OperationResult.Entity = entity;
            if (OperationResult.Entity == null || (AntiPlanetWeapon)OperationResult.Entity == null) return;
            var repository = RetrieveUow();
            if (repository == null) return;
            repository.Add((AntiPlanetWeapon)OperationResult.Entity);
            Uow.Save();
            OperationResult.RawResult = entity.Id;
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
                    case MappedOperations.SaveUow:
                    SaveUow();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(desiredOperation), desiredOperation, null);
            }
        }
    }
}