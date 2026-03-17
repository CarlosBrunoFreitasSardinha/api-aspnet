using CB.BackDefault.Domain.Aggregates.IdentityAggregate.Models;
using Microsoft.EntityFrameworkCore;

namespace CB.BackDefault.Infra.Data.Context
{
    public static class ConfiguracaoChavePrimaria
    {
        public static void addChavePrimaria(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshTokenModel>().HasKey(c=>c.Id);
        }
    }
}
