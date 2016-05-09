using System;
using Models.Buildings;
using Models.Fleets;
using Models.Fleets.ShipClasses;
using Models.Fleets.ShipClasses.Armors;
using Models.Fleets.ShipClasses.Engines;
using Models.Fleets.ShipClasses.Hulls;
using Models.Fleets.ShipClasses.Shields;
using Models.Fleets.ShipClasses.System;
using Models.Fleets.ShipClasses.Weapons;
using Models.Queues;
using Models.Races;
using Models.Tech;
using Models.Universe;
using Models.Users;
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Repository;
using UnitOfWork.Interfaces.Context;

namespace UnitOfWork.Implementations.Uows.UowDto
{
    public class UowRepositoryFactories : IDisposable
    {
        private readonly DalCache _cache;
        private readonly IContext _context;
        private bool _disposed;

        public UowRepositoryFactories(IContext context, DalCache cache, UowRepositories repositories)
        {
            if (context != null)
                _context = context;
            else
                throw new ArgumentNullException(nameof(context));
            if (cache != null)
                _cache = cache;
            else throw new ArgumentNullException(nameof(cache));
            if (repositories != null) Repositories = repositories;
            else throw new ArgumentNullException(nameof(repositories));
            InitializeRepositories();
        }

        public UowRepositories Repositories { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void InitializeRepositories()
        {
            if (Repositories.AntiPlanetWeaponRepo == null)
                Repositories.AntiPlanetWeaponRepo = RepositoryFactory<AntiPlanetWeapon>.GetRepository(_context, _cache);
            if (Repositories.AntiShipWeaponRepo == null)
                Repositories.AntiShipWeaponRepo = RepositoryFactory<AntiShipWeapon>.GetRepository(_context, _cache);
            if (Repositories.ShipSystemRepo == null)
                Repositories.ShipSystemRepo = RepositoryFactory<ShipSystem>.GetRepository(_context, _cache);
            if (Repositories.ShieldRepo == null)
                Repositories.ShieldRepo = RepositoryFactory<Shield>.GetRepository(_context, _cache);
            if (Repositories.HullRepo == null)
                Repositories.HullRepo = RepositoryFactory<Hull>.GetRepository(_context, _cache);
            if (Repositories.EngineRepo == null)
                Repositories.EngineRepo = RepositoryFactory<Engine>.GetRepository(_context, _cache);
            if (Repositories.ArmorRepo == null)
                Repositories.ArmorRepo = RepositoryFactory<Armor>.GetRepository(_context, _cache);
            if (Repositories.ShipClassRepo == null)
                Repositories.ShipClassRepo = RepositoryFactory<ShipClass>.GetRepository(_context, _cache);
            if (Repositories.FleetRepo == null)
                Repositories.FleetRepo = RepositoryFactory<Fleet>.GetRepository(_context, _cache);
            if (Repositories.BuildingSpecRepo == null)
                Repositories.BuildingSpecRepo = RepositoryFactory<BuildingSpec>.GetRepository(_context, _cache);
            if (Repositories.BuildingRepo == null)
                Repositories.BuildingRepo = RepositoryFactory<Building>.GetRepository(_context, _cache);
            if (Repositories.GalaxyRepo == null)
                Repositories.GalaxyRepo = RepositoryFactory<Galaxy>.GetRepository(_context, _cache);
            if (Repositories.BuildingQueueRepo == null)
                Repositories.BuildingQueueRepo = RepositoryFactory<BuildingQueue>.GetRepository(_context, _cache);
            if (Repositories.FleetQueueRepo == null)
                Repositories.FleetQueueRepo = RepositoryFactory<FleetQueue>.GetRepository(_context, _cache);
            if (Repositories.ResQueueRepo == null)
                Repositories.ResQueueRepo = RepositoryFactory<ResearchQueue>.GetRepository(_context, _cache);
            if (Repositories.RaceBonusRepo == null)
                Repositories.RaceBonusRepo = RepositoryFactory<RaceBonus>.GetRepository(_context, _cache);
            if (Repositories.TechNodeRepo == null)
                Repositories.TechNodeRepo = RepositoryFactory<TechRequisiteNode>.GetRepository(_context, _cache);
            if (Repositories.TechnologyRepo == null)
                Repositories.TechnologyRepo = RepositoryFactory<Technology>.GetRepository(_context, _cache);
            if (Repositories.TechBonusRepo == null)
                Repositories.TechBonusRepo = RepositoryFactory<TechBonus>.GetRepository(_context, _cache);
            if (Repositories.PlanetRepo == null)
                Repositories.PlanetRepo = RepositoryFactory<Planet>.GetRepository(_context, _cache);
            if (Repositories.SatelliteRepo == null)
                Repositories.SatelliteRepo = RepositoryFactory<Satellite>.GetRepository(_context, _cache);
            if (Repositories.StarRepo == null)
                Repositories.StarRepo = RepositoryFactory<Star>.GetRepository(_context, _cache);
            if (Repositories.UserRepo == null)
                Repositories.UserRepo = RepositoryFactory<User>.GetRepository(_context, _cache);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;
            Repositories.Dispose();
        }
    }
}