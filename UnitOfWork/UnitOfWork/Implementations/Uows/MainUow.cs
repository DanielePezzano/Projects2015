using System;
using System.Data.Entity.Infrastructure;
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
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Uows.UowDto;
using UnitOfWork.Interfaces.Context;
using UnitOfWork.Interfaces.Repository;
using UnitOfWork.Interfaces.UnitOfWork;

namespace UnitOfWork.Implementations.Uows
{
    public class MainUow : IUnitOfWork, IDisposable
    {
        private readonly UowRepositoryFactories _repoFactories;
        private IContext _context;
        private bool _disposed;

        public MainUow(IContext context, UowRepositoryFactories repoFactories)
        {
            if (context != null) _context = context;
            else throw new ArgumentNullException(nameof(context));
            if (repoFactories != null) _repoFactories = repoFactories;
            else throw new ArgumentNullException(nameof(repoFactories));

            if (_context.IsTest == false) CheckInitialization();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool Save()
        {
            return DoSaving(_context as ProductionContext) >= 0;
        }

        private void CheckInitialization()
        {
            var context = _context as ProductionContext;
            if (context == null || context.Database.Exists()) return;
            ((IObjectContextAdapter) ((ProductionContext) _context)).ObjectContext.CreateDatabase();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context = null;
                    _repoFactories.Dispose();
                }
            }
            _disposed = true;
        }

        private static int DoSaving(ProductionContext context)
        {
            return context?.SaveChanges() ?? 1;
        }

        #region Repositories properties

        public IRepository<AntiPlanetWeapon> AntiPlanetWeaponRepository
        {
            get { return _repoFactories.Repositories.AntiPlanetWeaponRepo; }
        }

        public IRepository<AntiShipWeapon> AntiShipWeaponRepository
        {
            get { return _repoFactories.Repositories.AntiShipWeaponRepo; }
        }

        public IRepository<ShipSystem> ShipSystemRepository
        {
            get { return _repoFactories.Repositories.ShipSystemRepo; }
        }

        public IRepository<Shield> ShieldRepository
        {
            get { return _repoFactories.Repositories.ShieldRepo; }
        }

        public IRepository<Hull> HullRepository
        {
            get { return _repoFactories.Repositories.HullRepo; }
        }

        public IRepository<Engine> EngineRepository
        {
            get { return _repoFactories.Repositories.EngineRepo; }
        }

        public IRepository<Armor> ArmorRepository
        {
            get { return _repoFactories.Repositories.ArmorRepo; }
        }

        public IRepository<ShipClass> ShipClassRepository
        {
            get { return _repoFactories.Repositories.ShipClassRepo; }
        }

        public IRepository<Fleet> FleetRepository
        {
            get { return _repoFactories.Repositories.FleetRepo; }
        }

        public IRepository<BuildingSpec> BuildingSpecRepository
        {
            get { return _repoFactories.Repositories.BuildingSpecRepo; }
        }

        public IRepository<Building> BuildingRepository
        {
            get { return _repoFactories.Repositories.BuildingRepo; }
        }

        public IRepository<GalaxyLog> GalaxyLogRepository
        {
            get { return _repoFactories.Repositories.GalaxyLogRepo; }
        }

        public IRepository<UserLog> UserLogRepository
        {
            get { return _repoFactories.Repositories.UserLogRepo; }
        }

        public IRepository<BuildingQueue> BuildingQueueRepository
        {
            get { return _repoFactories.Repositories.BuildingQueueRepo; }
        }

        public IRepository<FleetQueue> FleetQueueRepository
        {
            get { return _repoFactories.Repositories.FleetQueueRepo; }
        }

        public IRepository<ResearchQueue> ResearchQueueRepository
        {
            get { return _repoFactories.Repositories.ResQueueRepo; }
        }

        public IRepository<RaceBonus> RaceBonusRepository
        {
            get { return _repoFactories.Repositories.RaceBonusRepo; }
        }

        public IRepository<TechRequisiteNode> TechNodesRepository
        {
            get { return _repoFactories.Repositories.TechNodeRepo; }
        }

        public IRepository<Technology> TechnologyRepository
        {
            get { return _repoFactories.Repositories.TechnologyRepo; }
        }

        public IRepository<TechBonus> TechBonusRepository
        {
            get { return _repoFactories.Repositories.TechBonusRepo; }
        }

        public IRepository<InternalMail> InternalMailRepository
        {
            get { return _repoFactories.Repositories.InternalMailRepo; }
        }

        public IRepository<Planet> PlanetRepository
        {
            get { return _repoFactories.Repositories.PlanetRepo; }
        }

        public IRepository<Satellite> SatelliteRepository
        {
            get { return _repoFactories.Repositories.SatelliteRepo; }
        }

        public IRepository<Star> StarRepository
        {
            get { return _repoFactories.Repositories.StarRepo; }
        }

        public IRepository<Galaxy> GalaxyRepository
        {
            get { return _repoFactories.Repositories.GalaxyRepo; }
        }

        public IRepository<User> UserRepository
        {
            get { return _repoFactories.Repositories.UserRepo; }
        }

        #endregion
    }
}