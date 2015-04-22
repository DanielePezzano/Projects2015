using Models.Base;
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
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Repository;
using UnitOfWork.Interfaces.Context;
using UnitOfWork.Interfaces.Repository;

namespace UnitOfWork.Implementations.Uows.UowDto
{
    public class UowRepositoryFactories :IDisposable
    {
        private IContext _Context = null;
        private DalCache _Cache = null;
        public UowRepositories Repositories { get; set; }
        private bool _Disposed = false;

        public UowRepositoryFactories(IContext context,DalCache cache, UowRepositories repositories)
        {
            if (context != null)
                this._Context = context;
            else
                throw new ArgumentNullException("context");
            if (cache != null)
                this._Cache = cache;
            else throw new ArgumentNullException("cache");
            if (repositories != null) this.Repositories = repositories;
            else throw new ArgumentNullException("repositories");
            this.InitializeRepositories();
        }

        private void InitializeRepositories()
        {
            if (Repositories.AntiPlanetWeaponRepo == null)
                Repositories.AntiPlanetWeaponRepo = RepositoryFactory<AntiPlanetWeapon>.GetRepository(this._Context, this._Cache);
            if (Repositories.AntiShipWeaponRepo == null)
                Repositories.AntiShipWeaponRepo = RepositoryFactory<AntiShipWeapon>.GetRepository(this._Context, this._Cache);
            if (Repositories.ShipSystemRepo == null)
                Repositories.ShipSystemRepo = RepositoryFactory<ShipSystem>.GetRepository(this._Context, this._Cache);
            if (Repositories.ShieldRepo == null)
                Repositories.ShieldRepo = RepositoryFactory<Shield>.GetRepository(this._Context, this._Cache);
            if (Repositories.HullRepo == null)
                Repositories.HullRepo = RepositoryFactory<Hull>.GetRepository(this._Context, this._Cache);
            if (Repositories.EngineRepo == null)
                Repositories.EngineRepo = RepositoryFactory<Engine>.GetRepository(this._Context, this._Cache);
            if (Repositories.ArmorRepo == null)
                Repositories.ArmorRepo = RepositoryFactory<Armor>.GetRepository(this._Context, this._Cache);
            if (Repositories.ShipClassRepo == null)
                Repositories.ShipClassRepo = RepositoryFactory<ShipClass>.GetRepository(this._Context, this._Cache);
            if (Repositories.FleetRepo == null)
                Repositories.FleetRepo = RepositoryFactory<Fleet>.GetRepository(this._Context, this._Cache);
            if (Repositories.BuildingSpecRepo == null)
                Repositories.BuildingSpecRepo = RepositoryFactory<BuildingSpec>.GetRepository(this._Context, this._Cache);
            if (Repositories.BuildingRepo == null)
                Repositories.BuildingRepo = RepositoryFactory<Building>.GetRepository(this._Context, this._Cache);
            if (Repositories.GalaxyRepo == null)
                Repositories.GalaxyRepo = RepositoryFactory<Galaxy>.GetRepository(this._Context, this._Cache);
            if (Repositories.GalaxyLogRepo == null)
                Repositories.GalaxyLogRepo = RepositoryFactory<GalaxyLog>.GetRepository(this._Context, this._Cache);
            if (Repositories.UserLogRepo == null)
                Repositories.UserLogRepo = RepositoryFactory<UserLog>.GetRepository(this._Context, this._Cache);
            if (Repositories.BuildingQueueRepo == null)
                Repositories.BuildingQueueRepo = RepositoryFactory<BuildingQueue>.GetRepository(this._Context, this._Cache);
            if (Repositories.FleetQueueRepo == null)
                Repositories.FleetQueueRepo = RepositoryFactory<FleetQueue>.GetRepository(this._Context, this._Cache);
            if (Repositories.ResQueueRepo == null)
                Repositories.ResQueueRepo = RepositoryFactory<ResearchQueue>.GetRepository(this._Context, this._Cache);
            if (Repositories.RaceBonusRepo==null)
                Repositories.RaceBonusRepo = RepositoryFactory<RaceBonus>.GetRepository(this._Context, this._Cache);
            if (Repositories.TechNodeRepo==null)
                Repositories.TechNodeRepo = RepositoryFactory<TechRequisiteNode>.GetRepository(this._Context, this._Cache);
            if (Repositories.TechnologyRepo==null)
                Repositories.TechnologyRepo = RepositoryFactory<Technology>.GetRepository(this._Context, this._Cache);
            if (Repositories.TechBonusRepo==null)
                Repositories.TechBonusRepo = RepositoryFactory<TechBonus>.GetRepository(this._Context, this._Cache);
            if (Repositories.InternalMailRepo==null)
                Repositories.InternalMailRepo = RepositoryFactory<InternalMail>.GetRepository(this._Context, this._Cache);
            if (Repositories.PlanetRepo ==null)
                Repositories.PlanetRepo = RepositoryFactory<Planet>.GetRepository(this._Context, this._Cache);
            if (Repositories.SatelliteRepo ==null)
                Repositories.SatelliteRepo = RepositoryFactory<Satellite>.GetRepository(this._Context, this._Cache);
            if (Repositories.StarRepo==null)
                Repositories.StarRepo = RepositoryFactory<Star>.GetRepository(this._Context, this._Cache);
            if (Repositories.UserRepo==null)
                Repositories.UserRepo = RepositoryFactory<User>.GetRepository(this._Context, this._Cache);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_Disposed)
            {
                _Disposed = true;
                this.Repositories.Dispose();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
