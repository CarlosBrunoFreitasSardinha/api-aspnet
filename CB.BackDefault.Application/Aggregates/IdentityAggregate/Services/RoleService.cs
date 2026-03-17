using CB.BackDefault.Application.Aggregates.IdentityAggregate.Interfaces;
using CB.BackDefault.Application.Aggregates.IdentityAggregate.ViewModels.Response;
using CB.BackDefault.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CB.BackDefault.Application.Aggregates.IdentityAggregate.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<UserApplication> _userManager;

        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<UserApplication> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IEnumerable<RolesResponse>> GetRolesAsync()
        {
            return  await _roleManager.Roles
                .Select(r => new RolesResponse
                {
                    Name = r.Name!
                })
                .ToListAsync();
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
                return false;

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            return result.Succeeded;
        }

        public async Task<bool> DeleteRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null) return false;

            var result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;
        }


        public async Task<bool> AddUserToRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }

        public async Task<bool> RemoveUserFromRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.RemoveFromRoleAsync(user, role);
            return result.Succeeded;
        }

        public async Task<bool> UpdateRoleAsync(string roleId, string newName)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
                return false;

            role.Name = newName;
            role.NormalizedName = newName.ToUpper();

            var result = await _roleManager.UpdateAsync(role);

            return result.Succeeded;
        }


        public async Task<bool> UpdateUserClaimAsync(string userId, string oldClaim, string newClaim)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) return false;

            var claims = await _userManager.GetClaimsAsync(user);

            var existingClaim = claims.FirstOrDefault(c => c.Value == oldClaim);

            if (existingClaim == null) return false;

            var removeResult = await _userManager.RemoveClaimAsync(user, existingClaim);
            if (!removeResult.Succeeded) return false;

            var addResult = await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("permission", newClaim));

            return addResult.Succeeded;
        }
        public async Task<bool> UpdateRoleClaimAsync(string roleName, string oldClaim, string newClaim)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role == null) return false;

            var claims = await _roleManager.GetClaimsAsync(role);

            var existingClaim = claims.FirstOrDefault(c => c.Value == oldClaim);

            if (existingClaim == null) return false;

            var removeResult = await _roleManager.RemoveClaimAsync(role, existingClaim);
            if (!removeResult.Succeeded) return false;

            var addResult = await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim("permission", newClaim));

            return addResult.Succeeded;
        }
    }
}
