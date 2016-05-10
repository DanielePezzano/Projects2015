using System;
using DAL.Operations.Extensions;
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
            var repo = RepoSelector<BaseEntity>(repository, cacheKey);
            if (repo != null) result = repo.Any(c => c.Id == entityId, cacheKey);
            return result;
        }

        public BaseEntity GetById(MappedRepositories repository, int entityId, string cacheKey)
        {
            BaseEntity resBaseEntity = null;
            var repo = RepoSelector<BaseEntity>(repository, cacheKey);
            if (repo != null) resBaseEntity = repo.GetByKey(entityId, cacheKey);
            return resBaseEntity;
        }

        protected IRepository<T> RepoSelector<T>(MappedRepositories repository, string cacheKey) where T : BaseEntity
        {
            IRepository<T>result = null;
            switch (repository)
            {
                case MappedRepositories.AntiPlanetWeaponRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().AntiPlanetWeaponRepository :
                       (IRepository<T>)this.SetProductionUow().AntiPlanetWeaponRepository;
                    break;
                case MappedRepositories.AntiShipWeaponRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().AntiShipWeaponRepository :
                       (IRepository<T>)this.SetProductionUow().AntiShipWeaponRepository;
                    break;
                case MappedRepositories.ShipSystemRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().ShipSystemRepository :
                       (IRepository<T>)this.SetProductionUow().ShipSystemRepository;
                    break;
                case MappedRepositories.ShieldRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().ShieldRepository :
                       (IRepository<T>)this.SetProductionUow().ShieldRepository;
                    break;
                case MappedRepositories.HullRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().HullRepository :
                       (IRepository<T>)this.SetProductionUow().HullRepository;
                    break;
                case MappedRepositories.EngineRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().EngineRepository :
                       (IRepository<T>)this.SetProductionUow().EngineRepository;
                    break;
                case MappedRepositories.ArmorRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().ArmorRepository :
                       (IRepository<T>)this.SetProductionUow().ArmorRepository;
                    break;
                case MappedRepositories.ShipClassRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().ShipClassRepository :
                       (IRepository<T>)this.SetProductionUow().ShipClassRepository;
                    break;
                case MappedRepositories.FleetRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().FleetRepository :
                       (IRepository<T>)this.SetProductionUow().FleetRepository;
                    break;
                case MappedRepositories.BuildingSpecRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().BuildingSpecRepository :
                      (IRepository<T>)this.SetProductionUow().BuildingSpecRepository;
                    break;
                case MappedRepositories.BuildingRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().BuildingRepository :
                     (IRepository<T>)this.SetProductionUow().BuildingRepository;
                    break;
                case MappedRepositories.BuildingQueueRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().BuildingQueueRepository :
                       (IRepository<T>)this.SetProductionUow().BuildingQueueRepository;
                    break;
                case MappedRepositories.FleetQueueRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().FleetQueueRepository :
                       (IRepository<T>)this.SetProductionUow().FleetQueueRepository;
                    break;
                case MappedRepositories.ResearchQueueRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().ResearchQueueRepository :
                       (IRepository<T>)this.SetProductionUow().ResearchQueueRepository;
                    break;
                case MappedRepositories.RaceBonusRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().RaceBonusRepository :
                       (IRepository<T>)this.SetProductionUow().RaceBonusRepository;
                    break;
                case MappedRepositories.TechNodesRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().TechNodesRepository :
                       (IRepository<T>)this.SetProductionUow().TechNodesRepository;
                    break;
                case MappedRepositories.TechnologyRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().TechnologyRepository :
                      (IRepository<T>)this.SetProductionUow().TechnologyRepository;
                    break;
                case MappedRepositories.TechBonusRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().TechBonusRepository :
                      (IRepository<T>)this.SetProductionUow().TechBonusRepository;
                    break;
                case MappedRepositories.PlanetRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().PlanetRepository :
                     (IRepository<T>)this.SetProductionUow().PlanetRepository;
                    break;
                case MappedRepositories.SatelliteRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().SatelliteRepository :
                     (IRepository<T>)this.SetProductionUow().SatelliteRepository;
                    break;
                case MappedRepositories.StarRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().StarRepository :
                        (IRepository<T>)this.SetProductionUow().StarRepository;
                    break;
                case MappedRepositories.GalaxyRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().GalaxyRepository :
                        (IRepository<T>)this.SetProductionUow().GalaxyRepository;
                    break;
                case MappedRepositories.UserRepository:
                    result = (IsTest) ? (IRepository<T>)this.SetTestUow().UserRepository :
                     (IRepository<T>)this.SetProductionUow().UserRepository;
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