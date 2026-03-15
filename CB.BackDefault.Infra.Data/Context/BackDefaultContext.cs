using CB.BackDefault.Domain.Aggregates.AuthAggregate.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CB.BackDefault.Infra.Data.Context
{
    public class BackDefaultContext : IdentityDbContext<IdentityUser>
    {

        public BackDefaultContext(DbContextOptions<BackDefaultContext> options) : base(options)
        {
        }
        public DbSet<RefreshTokenModel> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(Guid))))
                property.SetColumnType("uuid");

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
               e => e.GetProperties().Where(p => p.ClrType == typeof(DateTime))))
                property.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                                            v => v.ToUniversalTime(),
                                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
               e => e.GetProperties().Where(p => p.ClrType == typeof(DateTime))))
                property.SetColumnType("timestamp with time zone");

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
               e => e.GetProperties().Where(p => p.ClrType == typeof(decimal))))
                property.SetColumnType("decimal(10,2)");


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                            v => v.ToUniversalTime(),
                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                    }
                }
            }

            modelBuilder.addChavePrimaria();

            base.OnModelCreating(modelBuilder);
        }

    } 

}
