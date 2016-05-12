using System;
using DAL.Operations.BaseClasses;
using DAL.Operations.Enums;
using DAL.Operations.Extensions;
using Models.Fleets.ShipClasses.Weapons;
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
            if (IsTest)
            {
                return this.SetTestUow(ConnectionString).AntiPlanetWeaponRepository;
            }
            return this.SetProductionUow(ConnectionString).AntiPlanetWeaponRepository;
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
                default:
                    throw new ArgumentOutOfRangeException(nameof(desiredOperation), desiredOperation, null);
            }
        }
    }
}