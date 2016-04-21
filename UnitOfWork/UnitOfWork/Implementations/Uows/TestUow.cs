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
using UnitOfWork.Implementations.Repository.BaseRepository;
using UnitOfWork.Implementations.Uows.BaseClass;
using UnitOfWork.Implementations.Uows.UowDto;
using UnitOfWork.Interfaces.Context;
using UnitOfWork.Interfaces.UnitOfWork;

namespace UnitOfWork.Implementations.Uows
{
    public class TestUow : BaseUow, IUnitOfWork
    {
        public TestUow(IContext context, UowRepositoryFactories repoFactories)
            : base(context, repoFactories)
        {
            
        }

        public bool Save()
        {
            return DoSaving(_context as TestContext) >= 0;
        }

        private static int DoSaving(TestContext context)
        {
            return context.SaveChanges() ? 1 : 0;
        }

        protected sealed override void CheckInitialization()
        {
            throw new System.NotImplementedException();
        }

        #region Repositories properties

        public RepositoryTest<AntiPlanetWeapon> AntiPlanetWeaponRepository => (RepositoryTest<AntiPlanetWeapon>)_repoFactories.Repositories.AntiPlanetWeaponRepo;

        public RepositoryTest<AntiShipWeapon> AntiShipWeaponRepository => (RepositoryTest<AntiShipWeapon>)_repoFactories.Repositories.AntiShipWeaponRepo;

        public RepositoryTest<ShipSystem> ShipSystemRepository =>(RepositoryTest<ShipSystem>) _repoFactories.Repositories.ShipSystemRepo;

        public RepositoryTest<Shield> ShieldRepository =>(RepositoryTest<Shield>) _repoFactories.Repositories.ShieldRepo;

        public RepositoryTest<Hull> HullRepository =>(RepositoryTest<Hull>) _repoFactories.Repositories.HullRepo;

        public RepositoryTest<Engine> EngineRepository => (RepositoryTest<Engine>)_repoFactories.Repositories.EngineRepo;

        public RepositoryTest<Armor> ArmorRepository => (RepositoryTest<Armor>)_repoFactories.Repositories.ArmorRepo;

        public RepositoryTest<ShipClass> ShipClassRepository => (RepositoryTest<ShipClass>)_repoFactories.Repositories.ShipClassRepo;

        public RepositoryTest<Fleet> FleetRepository => (RepositoryTest<Fleet>)_repoFactories.Repositories.FleetRepo;

        public RepositoryTest<BuildingSpec> BuildingSpecRepository => (RepositoryTest<BuildingSpec>)_repoFactories.Repositories.BuildingSpecRepo;

        public RepositoryTest<Building> BuildingRepository =>(RepositoryTest<Building>) _repoFactories.Repositories.BuildingRepo;

        public RepositoryTest<GalaxyLog> GalaxyLogRepository => (RepositoryTest<GalaxyLog>)_repoFactories.Repositories.GalaxyLogRepo;

        public RepositoryTest<UserLog> UserLogRepository => (RepositoryTest<UserLog>)_repoFactories.Repositories.UserLogRepo;

        public RepositoryTest<BuildingQueue> BuildingQueueRepository =>(RepositoryTest<BuildingQueue>) _repoFactories.Repositories.BuildingQueueRepo;

        public RepositoryTest<FleetQueue> FleetQueueRepository => (RepositoryTest<FleetQueue>)_repoFactories.Repositories.FleetQueueRepo;

        public RepositoryTest<ResearchQueue> ResearchQueueRepository => (RepositoryTest<ResearchQueue>)_repoFactories.Repositories.ResQueueRepo;

        public RepositoryTest<RaceBonus> RaceBonusRepository =>(RepositoryTest<RaceBonus>) _repoFactories.Repositories.RaceBonusRepo;

        public RepositoryTest<TechRequisiteNode> TechNodesRepository => (RepositoryTest<TechRequisiteNode>)_repoFactories.Repositories.TechNodeRepo;

        public RepositoryTest<Technology> TechnologyRepository =>(RepositoryTest<Technology>)_repoFactories.Repositories.TechnologyRepo;

        public RepositoryTest<TechBonus> TechBonusRepository => (RepositoryTest<TechBonus>)_repoFactories.Repositories.TechBonusRepo;

        public RepositoryTest<InternalMail> InternalMailRepository =>(RepositoryTest<InternalMail>) _repoFactories.Repositories.InternalMailRepo;

        public RepositoryTest<Planet> PlanetRepository =>(RepositoryTest<Planet>) _repoFactories.Repositories.PlanetRepo;

        public RepositoryTest<Satellite> SatelliteRepository =>(RepositoryTest<Satellite>)_repoFactories.Repositories.SatelliteRepo;

        public RepositoryTest<Star> StarRepository => (RepositoryTest<Star>)_repoFactories.Repositories.StarRepo;

        public RepositoryTest<Galaxy> GalaxyRepository =>(RepositoryTest<Galaxy>) _repoFactories.Repositories.GalaxyRepo;

        public RepositoryTest<User> UserRepository =>(RepositoryTest<User>) _repoFactories.Repositories.UserRepo;

        #endregion
    }
}