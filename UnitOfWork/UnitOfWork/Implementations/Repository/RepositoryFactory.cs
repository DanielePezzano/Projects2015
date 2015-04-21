using Models.Buildings;
using Models.Fleets;
using Models.Fleets.ShipClasses;
using Models.Fleets.ShipClasses.Armors;
using Models.Fleets.ShipClasses.Engines;
using Models.Fleets.ShipClasses.Hulls;
using Models.Fleets.ShipClasses.Shields;
using Models.Fleets.ShipClasses.System;
using Models.Fleets.ShipClasses.Weapons;
using Models.Logs;
using Models.Queues;
using Models.Races;
using Models.Tech;
using Models.Universe;
using Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Repository.BaseRepository;
using UnitOfWork.Interfaces.Context;
using UnitOfWork.Interfaces.Repository;

namespace UnitOfWork.Implementations.Repository
{
    public sealed class RepositoryFactory : IDisposable
    {
        private bool _Production = false;
        private DalCache _Cache = null;
        private bool disposed = false;

        public RepositoryFactory(IContext context,DalCache cache)
        {
            if (context as ProductionContext != null) this._Production = true;
            this._Cache = cache;
        }

        #region Get Repositories
        public IRepository<User> GetUserRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<User>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<User>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<Galaxy> GetGalaxyRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<Galaxy>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<Galaxy>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<Satellite> GetSatelliteRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<Satellite>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<Satellite>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<Star> GetStarRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<Star>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<Star>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<Planet> GetPlanetRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<Planet>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<Planet>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<InternalMail> GetInternalMailRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<InternalMail>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<InternalMail>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<TechBonus> GetTechBonusRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<TechBonus>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<TechBonus>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<Technology> GetTechnologyRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<Technology>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<Technology>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<TechRequisiteNode> GetTechNodeRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<TechRequisiteNode>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<TechRequisiteNode>(context as TestContext, _Cache, string.Empty);
        }


        public IRepository<RaceBonus> GetRaceBonusRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<RaceBonus>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<RaceBonus>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<ResearchQueue> GetResearchQueueRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<ResearchQueue>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<ResearchQueue>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<FleetQueue> GetFleetQueueRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<FleetQueue>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<FleetQueue>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<BuildingQueue> GetBuildingQueueRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<BuildingQueue>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<BuildingQueue>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<UserLog> GetUserLogRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<UserLog>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<UserLog>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<GalaxyLog> GalaxyLogRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<GalaxyLog>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<GalaxyLog>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<Building> GetBuildingRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<Building>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<Building>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<BuildingSpec> GetBuildingSpecRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<BuildingSpec>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<BuildingSpec>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<Fleet> GetFleetRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<Fleet>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<Fleet>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<ShipClass> GetShipClasstRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<ShipClass>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<ShipClass>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<Hull> GetHullRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<Hull>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<Hull>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<Engine> GetEngineRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<Engine>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<Engine>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<Armor> GetArmorRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<Armor>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<Armor>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<Shield> GetShieldRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<Shield>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<Shield>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<ShipSystem> GetShipSystemRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<ShipSystem>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<ShipSystem>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<AntiShipWeapon> GetAntiShipWeaponRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<AntiShipWeapon>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<AntiShipWeapon>(context as TestContext, _Cache, string.Empty);
        }

        public IRepository<AntiPlanetWeapon> GetAntiPlanetWeaponRepository(IContext context)
        {
            if (_Production) return new RepositoryProduction<AntiPlanetWeapon>(context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<AntiPlanetWeapon>(context as TestContext, _Cache, string.Empty);
        }
        #endregion

        public void Dispose()
        {
            this.Disposing(true);
            GC.SuppressFinalize(this);
        }

        private void Disposing(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                }
            }
        }
        
    }
}
