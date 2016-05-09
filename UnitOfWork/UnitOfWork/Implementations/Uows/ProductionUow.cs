using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
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
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Repository.BaseRepository;
using UnitOfWork.Implementations.Uows.BaseClass;
using UnitOfWork.Implementations.Uows.UowDto;
using UnitOfWork.Interfaces.Context;
using UnitOfWork.Interfaces.UnitOfWork;

namespace UnitOfWork.Implementations.Uows
{
    public class ProductionUow : BaseUow, IUnitOfWork,IUnitOfWorkAsync
    {
        

        public ProductionUow(IContext context, UowRepositoryFactories repoFactories):base(context,repoFactories)
        {
           CheckInitialization();
        }
       

        public bool Save()
        {
            return DoSaving(_context as ProductionContext) >= 0;
        }

        public Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return (_context as ProductionContext)?.SaveChangesAsync(cancellationToken);
        }

        protected sealed override void CheckInitialization()
        {
            var context = _context as ProductionContext;
            if (context == null || context.Database.Exists()) return;
            ((IObjectContextAdapter) (ProductionContext) _context).ObjectContext.CreateDatabase();
        }
        

        private static int DoSaving(DbContext context)
        {
            return context?.SaveChanges() ?? 1;
        }

        #region Repositories properties

        public RepositoryProduction<AntiPlanetWeapon> AntiPlanetWeaponRepository => (RepositoryProduction<AntiPlanetWeapon>)_repoFactories.Repositories.AntiPlanetWeaponRepo;
        public RepositoryProduction<AntiShipWeapon> AntiShipWeaponRepository => (RepositoryProduction<AntiShipWeapon>)_repoFactories.Repositories.AntiShipWeaponRepo;
        public RepositoryProduction<ShipSystem> ShipSystemRepository =>(RepositoryProduction<ShipSystem>) _repoFactories.Repositories.ShipSystemRepo;
        public RepositoryProduction<Shield> ShieldRepository =>(RepositoryProduction<Shield>) _repoFactories.Repositories.ShieldRepo;
        public RepositoryProduction<Hull> HullRepository =>(RepositoryProduction<Hull>) _repoFactories.Repositories.HullRepo;
        public RepositoryProduction<Engine> EngineRepository => (RepositoryProduction<Engine>)_repoFactories.Repositories.EngineRepo;
        public RepositoryProduction<Armor> ArmorRepository => (RepositoryProduction<Armor>)_repoFactories.Repositories.ArmorRepo;
        public RepositoryProduction<ShipClass> ShipClassRepository => (RepositoryProduction<ShipClass>)_repoFactories.Repositories.ShipClassRepo;
        public RepositoryProduction<Fleet> FleetRepository => (RepositoryProduction<Fleet>)_repoFactories.Repositories.FleetRepo;
        public RepositoryProduction<BuildingSpec> BuildingSpecRepository => (RepositoryProduction<BuildingSpec>)_repoFactories.Repositories.BuildingSpecRepo;
        public RepositoryProduction<Building> BuildingRepository =>(RepositoryProduction<Building>) _repoFactories.Repositories.BuildingRepo;
        public RepositoryProduction<BuildingQueue> BuildingQueueRepository =>(RepositoryProduction<BuildingQueue>) _repoFactories.Repositories.BuildingQueueRepo;
        public RepositoryProduction<FleetQueue> FleetQueueRepository => (RepositoryProduction<FleetQueue>)_repoFactories.Repositories.FleetQueueRepo;
        public RepositoryProduction<ResearchQueue> ResearchQueueRepository => (RepositoryProduction<ResearchQueue>)_repoFactories.Repositories.ResQueueRepo;
        public RepositoryProduction<RaceBonus> RaceBonusRepository =>(RepositoryProduction<RaceBonus>) _repoFactories.Repositories.RaceBonusRepo;
        public RepositoryProduction<TechRequisiteNode> TechNodesRepository => (RepositoryProduction<TechRequisiteNode>)_repoFactories.Repositories.TechNodeRepo;
        public RepositoryProduction<Technology> TechnologyRepository =>(RepositoryProduction<Technology>)_repoFactories.Repositories.TechnologyRepo;
        public RepositoryProduction<TechBonus> TechBonusRepository => (RepositoryProduction<TechBonus>)_repoFactories.Repositories.TechBonusRepo;
        public RepositoryProduction<Planet> PlanetRepository =>(RepositoryProduction<Planet>) _repoFactories.Repositories.PlanetRepo;
        public RepositoryProduction<Satellite> SatelliteRepository =>(RepositoryProduction<Satellite>)_repoFactories.Repositories.SatelliteRepo;
        public RepositoryProduction<Star> StarRepository => (RepositoryProduction<Star>)_repoFactories.Repositories.StarRepo;
        public RepositoryProduction<Galaxy> GalaxyRepository =>(RepositoryProduction<Galaxy>) _repoFactories.Repositories.GalaxyRepo;
        public RepositoryProduction<User> UserRepository =>(RepositoryProduction<User>) _repoFactories.Repositories.UserRepo;

        #endregion
    }
}