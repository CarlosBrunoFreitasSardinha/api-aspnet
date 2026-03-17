using CB.BackDefault.Api.Controllers.AuthAggregate.Request;
using CB.BackDefault.Application.Aggregates.IdentityAggregate.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CB.BackDefault.Api.Controllers.AuthAggregate
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly IClaimsService _claimService;

        public ClaimsController(IClaimsService claimService)
        {
            _claimService = claimService;
        }

        // ================= ROLE =================

        [HttpPost("roles/{roleName}/claims")]
        public async Task<IActionResult> AddClaimToRoleAsync(string roleName, [FromBody] ClaimRequest request)
        {
            var result = await _claimService.AddClaimToRoleAsync(roleName, request.Type, request.Value);

            if (!result) return BadRequest("Claim já existe");

            return Ok();
        }

        [HttpGet("roles/{roleName}/claims")]
        public async Task<IActionResult> GetClaimsFromRole(string roleName)
        {
            var claims = await _claimService.GetClaimFromRoleAsync(roleName);

            return Ok(claims);
        }

        [HttpPost("roles/{roleName}/claims/remove")]
        public async Task<IActionResult> RemoveClaimFromRole(string roleName, [FromBody] ClaimRequest request)
        {
            var result = await _claimService.RemoveClaimFromRoleAsync(roleName, request.Type, request.Value);

            if (!result)
                return NotFound("Role ou claim não encontrada");

            return Ok("Claim removida da role");
        }

        // ================= USER =================

        [HttpPost("/users/{userId}/claims")]
        public async Task<IActionResult> AddClaimToUserAsync(string userId, [FromBody] ClaimRequest request)
        {
            var result = await _claimService.AddClaimToUserAsync(userId, request.Type, request.Value);

            if (!result) return BadRequest("Claim já existe");

            return Ok();
        }

        [HttpGet("users/{userId}/claims")]
        public async Task<IActionResult> GetClaimsFromUser(string userId)
        {
            var claims = await _claimService.GetClaimFromUserAsync(userId);

            return Ok(claims);
        }

        [HttpPost("users/{userId}/claims/remove")]
        public async Task<IActionResult> RemoveClaimFromUser(string userId, [FromBody] ClaimRequest request)
        {
            var result = await _claimService.RemoveClaimFromUserAsync(userId, request.Type, request.Value);
            if (!result)
                return NotFound("Usuário ou claim não encontrada");

            return Ok("Claim removida do usuário");
        }
    }
}
