using System;
using DAL.Operations.BaseClasses;
using DAL.Operations.Enums;
using DAL.Operations.Implementations;
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Implementations.Uows.UowDto;
using UnitOfWork.Interfaces.Context;
using UnitOfWork.Interfaces.UnitOfWork;

namespace DAL.Operations.IstanceFactory
{
    public static class IstancesCreator
    {
        public static OpFactory RetrieveOpFactory(string connectionString, bool istest=false)
        {
            return new OpFactory(connectionString,istest);
        }

        public static ContextFactory RetrieveContextFactory(string connectionString, bool isTest)
        {
            return new ContextFactory(connectionString,isTest);
        }

        public static DalCache RetrieveDalCache()
        {
            return new DalCache();
        }

        public static UowRepositories RetrieveRepositories()
        {
            return new UowRepositories();
        }

        public static UowRepositoryFactories RetrieveRepositoryFactories(IContext context,DalCache cache, UowRepositories repositories)
        {
            return new UowRepositoryFactories(context,cache,repositories);
        }

        public static ProductionUow RetrieveProductionUow(IContext context, UowRepositoryFactories factory)
        {
            return new ProductionUow(context,factory);
        }

        public static TestUow RetrieveTestUow(IContext context, UowRepositoryFactories factory)
        {
            return new TestUow(context, factory);
        }

        public static BaseOpAbstract SelectOperator(MappedRepositories repoSelector, bool isTest, string connectionString,IUnitOfWork uow=null)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            switch (repoSelector)
            {
                case MappedRepositories.AntiPlanetWeaponRepository:
                    return (isTest) ?  new AntiPlanetWpOperations(uow,connectionString) : new AntiPlanetWpOperations(connectionString);
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
                case MappedRepositories.PlanetRepository:
                    break;
                case MappedRepositories.SatelliteRepository:
                    break;
                case MappedRepositories.StarRepository:
                    return (isTest) ? new StarOperations(uow, connectionString) : new StarOperations(connectionString);
                case MappedRepositories.GalaxyRepository:
                    break;
                case MappedRepositories.UserRepository:
                    return (isTest) ? new UserOperations(uow, connectionString) : new UserOperations(connectionString);
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return null;
        }
    }
}