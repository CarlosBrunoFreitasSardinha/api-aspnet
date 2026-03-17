using CB.BackDefault.Api.Controllers.IdentityAggregate.Request;
using CB.BackDefault.Application.Aggregates.IdentityAggregate.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CB.BackDefault.Api.Controllers.AuthAggregate
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize]
        [HttpGet("")]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _roleService.GetRolesAsync();

            if (!result.Any()) return NoContent();

            return Ok(result);
        }

        [Authorize]
        [HttpPost("")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var result = await _roleService.CreateRoleAsync(roleName);

            if (!result) return BadRequest("Role já existe");

            return Ok();
        }

        [Authorize]
        [HttpDelete("{roleName}")]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            var result = await _roleService.DeleteRoleAsync(roleName);

            if (!result) return NotFound();

            return Ok();
        }

        [Authorize]
        [HttpPut("{roleId}")]
        public async Task<IActionResult> UpdateRole(string roleId, [FromBody] UpdateRoleRequest request)
        {
            var result = await _roleService.UpdateRoleAsync(roleId, request.Name);

            if (!result)
                return BadRequest("Erro ao atualizar role");

            return Ok();
        }

        [Authorize]
        [HttpPost("{roleName}/users/{userId}")]
        public async Task<IActionResult> AddUserToRole(string userId, string roleName)
        {
            var result = await _roleService.AddUserToRoleAsync(userId, roleName);

            if (!result) return BadRequest("Role já existe");

            return Ok();
        }

        [Authorize]
        [HttpDelete("{roleName}/users/{userId}")]
        public async Task<IActionResult> RemoveUserFromRole(string userId, string roleName)
        {
            var result = await _roleService.RemoveUserFromRoleAsync(userId, roleName);

            if (!result) return NotFound();

            return Ok();
        }

        [Authorize]
        [HttpPut("users/{userId}/claims")]
        public async Task<IActionResult> UpdateUserClaim(string userId, [FromBody] UpdateClaimRequest request)
        {
            var result = await _roleService.UpdateUserClaimAsync(userId, request.OldValue, request.NewValue);

            if (!result)
                return BadRequest("Erro ao atualizar claim");

            return Ok();
        }

        [Authorize]
        [HttpPut("{roleName}/claims")]
        public async Task<IActionResult> UpdateRoleClaim(string roleName, [FromBody] UpdateClaimRequest request)
        {
            var result = await _roleService.UpdateRoleClaimAsync(roleName, request.OldValue, request.NewValue);

            if (!result)
                return BadRequest("Erro ao atualizar claim da role");

            return Ok();
        }
    }
}
