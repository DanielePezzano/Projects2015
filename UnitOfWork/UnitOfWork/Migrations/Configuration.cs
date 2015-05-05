using System;
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
        }

        protected override void Seed(ProductionContext context)
        {
            var seed = new Galaxy();
            seed.CreatedAt = DateTime.Now;
            seed.UpdatedAt = DateTime.Now;
            seed.Name = "Galaxy Seed";
            Console.WriteLine("Sono arrivato");
            context.Galaxys.AddOrUpdate(seed);
        }
    }
}