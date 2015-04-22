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
using UnitOfWork.Interfaces.Repository;

namespace UnitOfWork.Implementations.Uows.UowDto
{
    public class UowRepositories : IDisposable
    {
        private bool _Disposed = false;

        public IRepository<User> UserRepo { get; set; }
        public IRepository<InternalMail> InternalMailRepo { get; set; }
        public IRepository<Galaxy> GalaxyRepo { get; set; }
        public IRepository<Star> StarRepo { get; set; }
        public IRepository<Satellite> SatelliteRepo { get; set; }
        public IRepository<Planet> PlanetRepo { get; set; }
        public IRepository<TechBonus> TechBonusRepo { get; set; }
        public IRepository<Technology> TechnologyRepo { get; set; }
        public IRepository<TechRequisiteNode> TechNodeRepo { get; set; }
        public IRepository<RaceBonus> RaceBonusRepo { get; set; }
        public IRepository<ResearchQueue> ResQueueRepo { get; set; }
        public IRepository<FleetQueue> FleetQueueRepo { get; set; }
        public IRepository<BuildingQueue> BuildingQueueRepo { get; set; }
        public IRepository<UserLog> UserLogRepo { get; set; }
        public IRepository<GalaxyLog> GalaxyLogRepo { get; set; }
        public IRepository<Building> BuildingRepo { get; set; }
        public IRepository<BuildingSpec> BuildingSpecRepo { get; set; }
        public IRepository<Fleet> FleetRepo { get; set; }
        public IRepository<ShipClass> ShipClassRepo { get; set; }
        public IRepository<Armor> ArmorRepo { get; set; }
        public IRepository<Engine> EngineRepo { get; set; }
        public IRepository<Hull> HullRepo { get; set; }
        public IRepository<Shield> ShieldRepo { get; set; }
        public IRepository<ShipSystem> ShipSystemRepo { get; set; }
        public IRepository<AntiShipWeapon> AntiShipWeaponRepo { get; set; }
        public IRepository<AntiPlanetWeapon> AntiPlanetWeaponRepo { get; set; }

        public UowRepositories()
        {
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_Disposed)
            {
                _Disposed = true;
                if (AntiPlanetWeaponRepo != null) AntiPlanetWeaponRepo = null;
                if (AntiShipWeaponRepo != null) AntiShipWeaponRepo = null;
                if (ShipSystemRepo != null) ShipSystemRepo = null;
                if (ShieldRepo != null) ShieldRepo = null;
                if (HullRepo != null) HullRepo = null;
                if (EngineRepo != null) EngineRepo = null;
                if (ArmorRepo != null) ArmorRepo = null;
                if (ShipClassRepo != null) ShipClassRepo = null;
                if (FleetRepo != null) FleetRepo = null;
                if (BuildingSpecRepo != null) BuildingSpecRepo = null;
                if (BuildingRepo != null) BuildingRepo = null;
                if (GalaxyLogRepo != null) GalaxyLogRepo = null;
                if (UserLogRepo != null) UserLogRepo = null;
                if (BuildingQueueRepo != null) BuildingQueueRepo = null;
                if (FleetQueueRepo != null) FleetQueueRepo = null;
                if (ResQueueRepo != null) ResQueueRepo = null;
                if (RaceBonusRepo != null) RaceBonusRepo = null;
                if (TechNodeRepo != null) TechNodeRepo = null;
                if (TechnologyRepo != null) TechnologyRepo = null;
                if (TechBonusRepo != null) TechBonusRepo = null;
                if (PlanetRepo != null) PlanetRepo = null;
                if (SatelliteRepo != null) SatelliteRepo = null;
                if (StarRepo != null) StarRepo = null;
                if (GalaxyRepo != null) GalaxyRepo = null;
                if (InternalMailRepo != null) InternalMailRepo = null;
                if (UserRepo != null) UserRepo = null;
            }
        }
    }
}
