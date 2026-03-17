using CB.BackDefault.Application.Aggregates.IdentityAggregate.ViewModels.Response;

namespace CB.BackDefault.Application.Aggregates.IdentityAggregate.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RolesResponse>> GetRolesAsync();
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> DeleteRoleAsync(string roleName);

        Task<bool> AddUserToRoleAsync(string userId, string role);
        Task<bool> RemoveUserFromRoleAsync(string userId, string role);
        Task<bool> UpdateRoleAsync(string roleId, string newName);
        Task<bool> UpdateUserClaimAsync(string userId, string oldClaim, string newClaim);
        Task<bool> UpdateRoleClaimAsync(string roleName, string oldClaim, string newClaim);
    }
}