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
        private IContext _Context = null;
        private bool _Production = false;
        private DalCache _Cache = null;
        private bool disposed = false;

        public RepositoryFactory(IContext context,DalCache cache)
        {
            this._Context = context;
            if (_Context as ProductionContext != null) this._Production = true;
            this._Cache = cache;
        }

        #region Get Repositories
        public IRepository<User> GetUserRepository()
        {
            if (_Production) return new RepositoryProduction<User>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<User>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<Galaxy> GetGalaxyRepository()
        {
            if (_Production) return new RepositoryProduction<Galaxy>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<Galaxy>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<Satellite> GetSatelliteRepository()
        {
            if (_Production) return new RepositoryProduction<Satellite>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<Satellite>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<Star> GetStarRepository()
        {
            if (_Production) return new RepositoryProduction<Star>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<Star>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<Planet> GetPlanetRepository()
        {
            if (_Production) return new RepositoryProduction<Planet>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<Planet>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<InternalMail> GetInternalMailRepository()
        {
            if (_Production) return new RepositoryProduction<InternalMail>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<InternalMail>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<TechBonus> GetTechBonusRepository()
        {
            if (_Production) return new RepositoryProduction<TechBonus>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<TechBonus>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<Technology> GetTechnologyRepository()
        {
            if (_Production) return new RepositoryProduction<Technology>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<Technology>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<TechRequisiteNode> GetTechNodeRepository()
        {
            if (_Production) return new RepositoryProduction<TechRequisiteNode>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<TechRequisiteNode>(this._Context as TestContext, _Cache, string.Empty);
        }


        public IRepository<RaceBonus> GetRaceBonusRepository()
        {
            if (_Production) return new RepositoryProduction<RaceBonus>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<RaceBonus>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<ResearchQueue> GetResearchQueueRepository()
        {
            if (_Production) return new RepositoryProduction<ResearchQueue>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<ResearchQueue>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<FleetQueue> GetFleetQueueRepository()
        {
            if (_Production) return new RepositoryProduction<FleetQueue>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<FleetQueue>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<BuildingQueue> GetBuildingQueueRepository()
        {
            if (_Production) return new RepositoryProduction<BuildingQueue>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<BuildingQueue>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<UserLog> GetUserLogRepository()
        {
            if (_Production) return new RepositoryProduction<UserLog>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<UserLog>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<GalaxyLog> GalaxyLogRepository()
        {
            if (_Production) return new RepositoryProduction<GalaxyLog>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<GalaxyLog>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<Building> GetBuildingRepository()
        {
            if (_Production) return new RepositoryProduction<Building>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<Building>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<BuildingSpec> GetBuildingSpecRepository()
        {
            if (_Production) return new RepositoryProduction<BuildingSpec>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<BuildingSpec>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<Fleet> GetFleetRepository()
        {
            if (_Production) return new RepositoryProduction<Fleet>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<Fleet>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<ShipClass> GetShipClasstRepository()
        {
            if (_Production) return new RepositoryProduction<ShipClass>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<ShipClass>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<Hull> GetHullRepository()
        {
            if (_Production) return new RepositoryProduction<Hull>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<Hull>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<Engine> GetEngineRepository()
        {
            if (_Production) return new RepositoryProduction<Engine>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<Engine>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<Armor> GetArmorRepository()
        {
            if (_Production) return new RepositoryProduction<Armor>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<Armor>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<Shield> GetShieldRepository()
        {
            if (_Production) return new RepositoryProduction<Shield>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<Shield>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<ShipSystem> GetShipSystemRepository()
        {
            if (_Production) return new RepositoryProduction<ShipSystem>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<ShipSystem>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<AntiShipWeapon> GetAntiShipWeaponRepository()
        {
            if (_Production) return new RepositoryProduction<AntiShipWeapon>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<AntiShipWeapon>(this._Context as TestContext, _Cache, string.Empty);
        }

        public IRepository<AntiPlanetWeapon> GetAntiPlanetWeaponRepository()
        {
            if (_Production) return new RepositoryProduction<AntiPlanetWeapon>(this._Context as ProductionContext, _Cache, string.Empty);
            else return new RepositoryTest<AntiPlanetWeapon>(this._Context as TestContext, _Cache, string.Empty);
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
                    this._Context = null;
                }
            }
        }
        
    }
}
