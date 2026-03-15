using CB.BackDefault.Domain.Aggregates.AuthAggregate.Models;
using CB.BackDefault.Infra.Data.Shared.Identity;
using Microsoft.EntityFrameworkCore;

namespace CB.BackDefault.Infra.Data.Aggregates.AuthAggregate.Mappings
{
    public static class RefreshTokenMap
    {
        public static void AddRefreshToken(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshTokenModel>()
                        .HasOne<UserAplication>()
                        .WithMany(u => u.RefreshTokens)
                        .HasForeignKey(r => r.UserId);
        }
    }
}
