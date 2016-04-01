using System;
using System.Data.Entity;
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
            ((IObjectContextAdapter) (ProductionContext) _context).ObjectContext.CreateDatabase();
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

        private static int DoSaving(DbContext context)
        {
            return context?.SaveChanges() ?? 1;
        }

        #region Repositories properties

        public IRepository<AntiPlanetWeapon> AntiPlanetWeaponRepository => _repoFactories.Repositories.AntiPlanetWeaponRepo;

        public IRepository<AntiShipWeapon> AntiShipWeaponRepository => _repoFactories.Repositories.AntiShipWeaponRepo;

        public IRepository<ShipSystem> ShipSystemRepository => _repoFactories.Repositories.ShipSystemRepo;

        public IRepository<Shield> ShieldRepository => _repoFactories.Repositories.ShieldRepo;

        public IRepository<Hull> HullRepository => _repoFactories.Repositories.HullRepo;

        public IRepository<Engine> EngineRepository => _repoFactories.Repositories.EngineRepo;

        public IRepository<Armor> ArmorRepository => _repoFactories.Repositories.ArmorRepo;

        public IRepository<ShipClass> ShipClassRepository => _repoFactories.Repositories.ShipClassRepo;

        public IRepository<Fleet> FleetRepository => _repoFactories.Repositories.FleetRepo;

        public IRepository<BuildingSpec> BuildingSpecRepository => _repoFactories.Repositories.BuildingSpecRepo;

        public IRepository<Building> BuildingRepository => _repoFactories.Repositories.BuildingRepo;

        public IRepository<GalaxyLog> GalaxyLogRepository => _repoFactories.Repositories.GalaxyLogRepo;

        public IRepository<UserLog> UserLogRepository => _repoFactories.Repositories.UserLogRepo;

        public IRepository<BuildingQueue> BuildingQueueRepository => _repoFactories.Repositories.BuildingQueueRepo;

        public IRepository<FleetQueue> FleetQueueRepository => _repoFactories.Repositories.FleetQueueRepo;

        public IRepository<ResearchQueue> ResearchQueueRepository => _repoFactories.Repositories.ResQueueRepo;

        public IRepository<RaceBonus> RaceBonusRepository => _repoFactories.Repositories.RaceBonusRepo;

        public IRepository<TechRequisiteNode> TechNodesRepository => _repoFactories.Repositories.TechNodeRepo;

        public IRepository<Technology> TechnologyRepository => _repoFactories.Repositories.TechnologyRepo;

        public IRepository<TechBonus> TechBonusRepository => _repoFactories.Repositories.TechBonusRepo;

        public IRepository<InternalMail> InternalMailRepository => _repoFactories.Repositories.InternalMailRepo;

        public IRepository<Planet> PlanetRepository => _repoFactories.Repositories.PlanetRepo;

        public IRepository<Satellite> SatelliteRepository => _repoFactories.Repositories.SatelliteRepo;

        public IRepository<Star> StarRepository => _repoFactories.Repositories.StarRepo;

        public IRepository<Galaxy> GalaxyRepository => _repoFactories.Repositories.GalaxyRepo;

        public IRepository<User> UserRepository => _repoFactories.Repositories.UserRepo;

        #endregion
    }
}