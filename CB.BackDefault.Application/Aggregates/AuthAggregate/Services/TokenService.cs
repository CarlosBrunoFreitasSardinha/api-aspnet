using CB.BackDefault.Application.Aggregates.AuthAggregate.Interfaces;
using CB.BackDefault.Application.Shared.Settings;
using CB.BackDefault.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CB.BackDefault.Application.Aggregates.AuthAggregate.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<UserApplication> _userManager;

        public TokenService(JwtSettings jwtSettings, UserManager<UserApplication> userManager)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
        }

        public async Task<string> GenerateAccessToken(UserApplication user)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            var tokenClaims = new List<Claim>(claims)
                                    {
                                        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                                issuer: _jwtSettings.Issuer,
                                audience: _jwtSettings.Audience,
                                claims: tokenClaims,
                                expires: DateTime.UtcNow.AddMinutes(15),
                                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}