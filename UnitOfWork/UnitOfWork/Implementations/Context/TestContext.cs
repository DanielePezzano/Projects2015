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
using UnitOfWork.Interfaces.Context;

namespace UnitOfWork.Implementations.Context
{
    public class TestContext : IContext
    {
        public bool IsTest { get; set; }
        public List<Galaxy> Galaxys { get; set; }
        public List<Star> Stars { get; set; }
        public List<Satellite> Satellites { get; set; }
        public List<Planet> Planets { get; set; }
        public List<User> Users { get; set; }
        public List<InternalMail> InternalMails { get; set; }
        public List<TechBonus> TechBonuses { get; set; }
        public List<Technology> Technologies { get; set; }
        public List<TechRequisiteNode> TechRequisiteNodes { get; set; }
        public List<RaceBonus> RaceBonuses { get; set; }
        public List<ResearchQueue> ResearchQueues { get; set; }
        public List<FleetQueue> FleetQueues { get; set; }
        public List<BuildingQueue> BuildingQueues { get; set; }
        public List<UserLog> UserLogs { get; set; }
        public List<GalaxyLog> GalaxyLogs { get; set; }
        public List<Building> Buildings { get; set; }
        public List<BuildingSpec> BuildingSpecs { get; set; }
        public List<Fleet> Fleets { get; set; }
        public List<ShipClass> ShipClasses { get; set; }
        public List<Armor> Armors { get; set; }
        public List<Engine> Engines { get; set; }
        public List<Hull> Hulls { get; set; }
        public List<Shield> Shields { get; set; }
        public List<ShipSystem> ShipSystems { get; set; }
        public List<AntiShipWeapon> AntiShipWeapons { get; set; }
        public List<AntiPlanetWeapon> AntiPlanetWeapons { get; set; }
    }
}
