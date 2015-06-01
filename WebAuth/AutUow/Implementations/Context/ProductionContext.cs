using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using AuthModel;
using AutUow.Interfaces;

namespace AutUow.Implementations.Context
{
    public class ProductionContext : DbContext, IContext
    {
        public ProductionContext()
            : base("AuthConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Roles> UserRoleses { get; set; }
        public bool IsTest { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Roles>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            base.OnModelCreating(modelBuilder);
        }
    }
}