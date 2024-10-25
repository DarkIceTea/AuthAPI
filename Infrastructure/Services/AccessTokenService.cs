using Application.Abstractions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AccessTokenService(UserManager<CustomUser> userManager) : IAccessTokenService
    {
        public string CreateAccessToken(CustomUser user, CancellationToken cancellationToken)
        {
            var roles = GetUserRoles(user, cancellationToken).Result;
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("role", roles[0])
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                issuer: "AuthApiServer",
                audience: "InnoclinicApi",
                signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("securitykeysecuritykeysecuritykey")),
                SecurityAlgorithms.HmacSha256Signature)
                );//TODO: to constants

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<IList<string>> GetUserRoles(CustomUser user, CancellationToken cancellationToken)
        {
            return await userManager.GetRolesAsync(user);
        }
    }
}
