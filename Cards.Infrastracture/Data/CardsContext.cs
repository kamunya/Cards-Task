using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Cards.Core.Entities;
using System;
using System.Linq;
using System.Reflection;

namespace Cards.Infrastracture.Data
{
    public class CardsContext : DbContext
    {
        public CardsContext(DbContextOptions<CardsContext> options) : base(options)
        {

        }
        public DbSet<Card> Cards { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            if(Database.ProviderName =="Microsoft.EntityFramework.Sqlite")
            {
                foreach (var entity in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entity.ClrType.GetProperties()
                        .Where(p => p.PropertyType == typeof(decimal));
                    var dateandtimepropertise = entity.ClrType.GetProperties()
                        .Where(t => t.PropertyType == typeof(DateTimeOffset));
                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entity.Name).Property(property.Name)
                            .HasConversion<double>();
                    }
                    foreach (var property in dateandtimepropertise)
                    {
                        modelBuilder.Entity(entity.Name).Property(property.Name)
                            .HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }
        }
        

       
    }
}
