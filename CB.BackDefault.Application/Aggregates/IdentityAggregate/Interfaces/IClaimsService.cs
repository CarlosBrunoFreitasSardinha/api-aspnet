namespace CB.BackDefault.Application.Aggregates.IdentityAggregate.Interfaces
{
    public interface IClaimsService
    {
        Task<bool> AddClaimToUserAsync(string userId, string type, string value);
        Task<IEnumerable<string>> GetClaimFromUserAsync(string userId);
        Task<bool> RemoveClaimFromUserAsync(string userId, string claimType, string claimValue);

        Task<bool> AddClaimToRoleAsync(string roleName, string type, string value);
        Task<IEnumerable<string>> GetClaimFromRoleAsync(string roleName);
        Task<bool> RemoveClaimFromRoleAsync(string roleName, string claimType, string claimValue);
    }
}
