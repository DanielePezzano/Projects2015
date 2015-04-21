namespace UnitOfWork.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<UnitOfWork.Implementations.Context.ProductionContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(UnitOfWork.Implementations.Context.ProductionContext context)
        {

            Models.Universe.Galaxy seed = new Models.Universe.Galaxy();
            seed.CreatedAt = DateTime.Now;
            seed.UpdatedAt = DateTime.Now;
            seed.Name = "Galaxy Seed";
            Console.WriteLine("Sono arrivato");
            context.Galaxys.AddOrUpdate(seed);
                
        }
    }
}
