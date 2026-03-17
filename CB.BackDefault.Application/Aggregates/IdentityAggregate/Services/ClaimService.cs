using CB.BackDefault.Application.Aggregates.IdentityAggregate.Interfaces;
using CB.BackDefault.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace CB.BackDefault.Application.Aggregates.IdentityAggregate.Services
{
    public class ClaimService : IClaimsService
    {
        private readonly UserManager<UserApplication> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ClaimService(
            UserManager<UserApplication> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // ================= ROLE =================

        public async Task<IEnumerable<string>> GetClaimFromRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role == null)
                return Enumerable.Empty<string>();

            var claims = await _roleManager.GetClaimsAsync(role);

            return claims.Select(c => $"{c.Type}:{c.Value}");
        }

        public async Task<bool> AddClaimToRoleAsync(string roleName, string type, string value)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            var roleClaims = await _roleManager.GetClaimsAsync(role);

            if (roleClaims.Any(c => c.Type == type && c.Value == value))
                return false;

            var result = await _roleManager.AddClaimAsync(role, new Claim(type, value));

            return result.Succeeded;
        }

        public async Task<bool> RemoveClaimFromRoleAsync(string roleName, string claimType, string claimValue)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role == null)
                return false;

            var claim = new Claim(claimType, claimValue);

            var result = await _roleManager.RemoveClaimAsync(role, claim);

            return result.Succeeded;
        }

        // ================= USER =================

        public async Task<IEnumerable<string>> GetClaimFromUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return Enumerable.Empty<string>();

            var claims = await _userManager.GetClaimsAsync(user);

            return claims.Select(c => $"{c.Type}:{c.Value}");
        }
        public async Task<bool> AddClaimToUserAsync(string userId, string type, string value)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userClaims = await _userManager.GetClaimsAsync(user);

            if(userClaims.Any(c => c.Type == type && c.Value == value))
                return false;

            var result = await _userManager.AddClaimAsync(user, new Claim(type, value));

            return result.Succeeded;
        }

        public async Task<bool> RemoveClaimFromUserAsync(string userId, string claimType, string claimValue)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return false;

            var claim = new Claim(claimType, claimValue);

            var result = await _userManager.RemoveClaimAsync(user, claim);

            return result.Succeeded;
        }
    }
}
