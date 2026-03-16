using CB.BackDefault.Domain.Aggregates.AuthAggregate.Models;
using CB.BackDefault.Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace CB.BackDefault.Infra.Data.Aggregates.AuthAggregate.Mappings
{
    public static class RefreshTokenMap
    {
        public static void AddRefreshToken(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshTokenModel>()
                        .HasOne<UserApplication>()
                        .WithMany()
                        .HasForeignKey(r => r.UserId);
        }
    }
}
