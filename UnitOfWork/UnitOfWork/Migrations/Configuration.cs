using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Models.Universe;
using UnitOfWork.Implementations.Context;

namespace UnitOfWork.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ProductionContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        
        protected override void Seed(ProductionContext context)
        {
            var seed = new Galaxy
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Name = "Galaxy Seed"
            };
            Console.WriteLine(@"Sono arrivato");
            context.Galaxys.Add(seed);
            context.SaveChanges();
        }
    }
}