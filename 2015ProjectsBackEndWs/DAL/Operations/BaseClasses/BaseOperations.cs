using System;
using DAL.Operations.Extensions;
using DAL.Operations.IstanceFactory;
using Models.Base;
using UnitOfWork.Implementations.Repository.BaseRepository;
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
                var repo = RepoSelector<BaseEntity>(repository, entityId, cacheKey, operation);
                if (repo != null) result = repo.Any(c => c.Id == entityId, cacheKey);
            }
            return result;
        }

        private IRepository<T> RepoSelector<T>(MappedRepositories repository, int entityId, string cacheKey, SpecificOperations operation) where T : BaseEntity
        {
            IRepository<T>result = null;
            switch (repository)
            {
                case MappedRepositories.AntiPlanetWeaponRepository:
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
                    break;
                case MappedRepositories.BuildingRepository:
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
                    break;
                case MappedRepositories.SatelliteRepository:
                    break;
                case MappedRepositories.StarRepository:
                    result= (IsTest) ? (IRepository<T>)operation.SetTestUow().StarRepository :
                        (IRepository<T>)operation.SetProductionUow().StarRepository;
                    break;
                case MappedRepositories.GalaxyRepository:
                    break;
                case MappedRepositories.UserRepository:
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