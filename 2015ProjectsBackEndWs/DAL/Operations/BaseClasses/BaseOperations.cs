using System;
using DAL.Operations.Extensions;
using DAL.Operations.IstanceFactory;
using Models.Base;
using UnitOfWork.Interfaces.Repository;

namespace DAL.Operations.BaseClasses
{
    public abstract class BaseOperations:IDisposable
    {
        protected bool IsTest;
        public string ConnectionString;

        protected BaseOperations(string connectionString, bool isTest=false)
        {
            IsTest = isTest;
            ConnectionString = connectionString;
        }

        public bool Any(MappedRepositories repository,int entityId,string cacheKey)
        {
            bool result=false;
            using (var operation = OperationsFactory.RetrieveBaseOperations(ConnectionString, IsTest))
            {
                var repo = RepoSelector<BaseEntity>(repository, cacheKey, operation);
                if (repo != null) result = repo.Any(c => c.Id == entityId, cacheKey);
            }
            return result;
        }

        public BaseEntity GetById(MappedRepositories repository, int entityId, string cacheKey)
        {
            BaseEntity resBaseEntity = null;
            using (var operation = OperationsFactory.RetrieveBaseOperations(ConnectionString, IsTest))
            {
                var repo = RepoSelector<BaseEntity>(repository, cacheKey, operation);
                if (repo != null) resBaseEntity = repo.GetByKey(entityId, cacheKey);
            }
            return resBaseEntity;
        }

        protected IRepository<T> RepoSelector<T>(MappedRepositories repository, string cacheKey, UserOperations operation) where T : BaseEntity
        {
            IRepository<T>result = null;
            switch (repository)
            {
                case MappedRepositories.AntiPlanetWeaponRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().AntiPlanetWeaponRepository :
                       (IRepository<T>)operation.SetProductionUow().AntiPlanetWeaponRepository;
                    break;
                case MappedRepositories.AntiShipWeaponRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().AntiShipWeaponRepository :
                       (IRepository<T>)operation.SetProductionUow().AntiShipWeaponRepository;
                    break;
                case MappedRepositories.ShipSystemRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().ShipSystemRepository :
                       (IRepository<T>)operation.SetProductionUow().ShipSystemRepository;
                    break;
                case MappedRepositories.ShieldRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().ShieldRepository :
                       (IRepository<T>)operation.SetProductionUow().ShieldRepository;
                    break;
                case MappedRepositories.HullRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().HullRepository :
                       (IRepository<T>)operation.SetProductionUow().HullRepository;
                    break;
                case MappedRepositories.EngineRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().EngineRepository :
                       (IRepository<T>)operation.SetProductionUow().EngineRepository;
                    break;
                case MappedRepositories.ArmorRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().ArmorRepository :
                       (IRepository<T>)operation.SetProductionUow().ArmorRepository;
                    break;
                case MappedRepositories.ShipClassRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().ShipClassRepository :
                       (IRepository<T>)operation.SetProductionUow().ShipClassRepository;
                    break;
                case MappedRepositories.FleetRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().FleetRepository :
                       (IRepository<T>)operation.SetProductionUow().FleetRepository;
                    break;
                case MappedRepositories.BuildingSpecRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().BuildingSpecRepository :
                      (IRepository<T>)operation.SetProductionUow().BuildingSpecRepository;
                    break;
                case MappedRepositories.BuildingRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().BuildingRepository :
                     (IRepository<T>)operation.SetProductionUow().BuildingRepository;
                    break;
                case MappedRepositories.BuildingQueueRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().BuildingQueueRepository :
                       (IRepository<T>)operation.SetProductionUow().BuildingQueueRepository;
                    break;
                case MappedRepositories.FleetQueueRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().FleetQueueRepository :
                       (IRepository<T>)operation.SetProductionUow().FleetQueueRepository;
                    break;
                case MappedRepositories.ResearchQueueRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().ResearchQueueRepository :
                       (IRepository<T>)operation.SetProductionUow().ResearchQueueRepository;
                    break;
                case MappedRepositories.RaceBonusRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().RaceBonusRepository :
                       (IRepository<T>)operation.SetProductionUow().RaceBonusRepository;
                    break;
                case MappedRepositories.TechNodesRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().TechNodesRepository :
                       (IRepository<T>)operation.SetProductionUow().TechNodesRepository;
                    break;
                case MappedRepositories.TechnologyRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().TechnologyRepository :
                      (IRepository<T>)operation.SetProductionUow().TechnologyRepository;
                    break;
                case MappedRepositories.TechBonusRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().TechBonusRepository :
                      (IRepository<T>)operation.SetProductionUow().TechBonusRepository;
                    break;
                case MappedRepositories.PlanetRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().PlanetRepository :
                     (IRepository<T>)operation.SetProductionUow().PlanetRepository;
                    break;
                case MappedRepositories.SatelliteRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().SatelliteRepository :
                     (IRepository<T>)operation.SetProductionUow().SatelliteRepository;
                    break;
                case MappedRepositories.StarRepository:
                    result= (IsTest) ? (IRepository<T>)operation.SetTestUow().StarRepository :
                        (IRepository<T>)operation.SetProductionUow().StarRepository;
                    break;
                case MappedRepositories.GalaxyRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().GalaxyRepository :
                        (IRepository<T>)operation.SetProductionUow().GalaxyRepository;
                    break;
                case MappedRepositories.UserRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().UserRepository :
                     (IRepository<T>)operation.SetProductionUow().UserRepository;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(repository), repository, null);
            }
            return result;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}