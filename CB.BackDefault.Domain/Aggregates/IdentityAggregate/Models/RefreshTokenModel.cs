using System.ComponentModel.DataAnnotations.Schema;

namespace CB.BackDefault.Domain.Aggregates.IdentityAggregate.Models
{
    [Table("RefreshToken")]
    public class RefreshTokenModel
    {
        public Guid Id { get; set; }

        public string TokenHash { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public DateTime ExpirationDate { get; set; }

        public bool Revoked { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public Guid? ReplacedByTokenId { get; set; }

        public DateTime? RevokedAt { get; set; }

        public string? CreatedByIp { get; set; }

        public string? RevokedByIp { get; set; }

        public bool IsExpired => DateTime.UtcNow >= ExpirationDate;
        public bool IsActive => !Revoked && !IsExpired;

        public RefreshTokenModel() { }
        public RefreshTokenModel(Guid id, string tokenHash, string userId, DateTime createdAt, DateTime expirationDate, string createByIp)
        {
            Id = id;
            TokenHash = tokenHash;
            UserId = userId;
            CreatedAt = createdAt;
            ExpirationDate = expirationDate;
            CreatedByIp = createByIp;
        }
    }
}
