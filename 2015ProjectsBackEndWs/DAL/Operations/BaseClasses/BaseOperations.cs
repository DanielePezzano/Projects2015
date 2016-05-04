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

        protected IRepository<T> RepoSelector<T>(MappedRepositories repository, string cacheKey, SpecificOperations operation) where T : BaseEntity
        {
            IRepository<T>result = null;
            switch (repository)
            {
                case MappedRepositories.AntiPlanetWeaponRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().AntiPlanetWeaponRepository :
                       (IRepository<T>)operation.SetProductionUow().AntiPlanetWeaponRepository;
                    break;
                case MappedRepositories.AntiShipWeaponRepository:
                    break;
                case MappedRepositories.ShipSystemRepository:
                    break;
                case MappedRepositories.ShieldRepository:
                    break;
                case MappedRepositories.HullRepository:
                    break;
                case MappedRepositories.EngineRepository:
                    break;
                case MappedRepositories.ArmorRepository:
                    break;
                case MappedRepositories.ShipClassRepository:
                    break;
                case MappedRepositories.FleetRepository:
                    break;
                case MappedRepositories.BuildingSpecRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().BuildingSpecRepository :
                      (IRepository<T>)operation.SetProductionUow().BuildingSpecRepository;
                    break;
                case MappedRepositories.BuildingRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().BuildingRepository :
                     (IRepository<T>)operation.SetProductionUow().BuildingRepository;
                    break;
                case MappedRepositories.GalaxyLogRepository:
                    break;
                case MappedRepositories.UserLogRepository:
                    break;
                case MappedRepositories.BuildingQueueRepository:
                    break;
                case MappedRepositories.FleetQueueRepository:
                    break;
                case MappedRepositories.ResearchQueueRepository:
                    break;
                case MappedRepositories.RaceBonusRepository:
                    result = (IsTest) ? (IRepository<T>)operation.SetTestUow().RaceBonusRepository :
                       (IRepository<T>)operation.SetProductionUow().RaceBonusRepository;
                    break;
                case MappedRepositories.TechNodesRepository:
                    break;
                case MappedRepositories.TechnologyRepository:
                    break;
                case MappedRepositories.TechBonusRepository:
                    break;
                case MappedRepositories.InternalMailRepository:
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