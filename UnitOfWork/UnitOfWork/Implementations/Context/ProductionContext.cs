using System.Data.Entity;
using Models.Universe;
using UnitOfWork.Interfaces.Context;
using Models.Users;
using Models.Tech;
using Models.Races;
using Models.Queues;
using Models.Logs;
using Models.Buildings;
using Models.Fleets;
using Models.Fleets.ShipClasses;
using Models.Fleets.ShipClasses.Armors;
using Models.Fleets.ShipClasses.Engines;
using Models.Fleets.ShipClasses.Hulls;
using Models.Fleets.ShipClasses.Shields;
using Models.Fleets.ShipClasses.System;
using Models.Fleets.ShipClasses.Weapons;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitOfWork.Implementations.Context
{
    public class ProductionContext :DbContext, IContext
    {
        public bool IsTest { get; set; }
        public DbSet<Galaxy> Galaxys { get; set; }
        public DbSet<Star> Stars { get; set; }
        public DbSet<Satellite> Satellites { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<InternalMail> InternalMails { get; set; }
        public DbSet<TechBonus> TechBonuses { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<TechRequisiteNode> TechRequisiteNodes { get; set; }
        public DbSet<RaceBonus> RaceBonuses { get; set; }
        public DbSet<ResearchQueue> ResearchQueues { get; set; }
        public DbSet<FleetQueue> FleetQueues { get; set; }
        public DbSet<BuildingQueue> BuildingQueues { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }
        public DbSet<GalaxyLog> GalaxyLogs { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<BuildingSpec> BuildingSpecs { get; set; }
        public DbSet<Fleet> Fleets { get;set; }
        public DbSet<ShipClass> ShipClasses { get; set; }
        public DbSet<Armor> Armors { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<Hull> Hulls { get;set; }
        public DbSet<Shield> Shields { get;set; }
        public DbSet<ShipSystem> ShipSystems { get;set; }
        public DbSet<AntiShipWeapon> AntiShipWeapons { get; set; }
        public DbSet<AntiPlanetWeapon> AntiPlanetWeapons { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Galaxy>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Star>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Satellite>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Planet>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<User>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<InternalMail>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<TechBonus>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Technology>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<TechRequisiteNode>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<RaceBonus>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ResearchQueue>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<FleetQueue>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<BuildingQueue>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<UserLog>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<GalaxyLog>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Building>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<BuildingSpec>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Fleet>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ShipClass>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Armor>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Engine>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Hull>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ShipSystem>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<AntiShipWeapon>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<AntiPlanetWeapon>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            base.OnModelCreating(modelBuilder);
        }
    }
}
