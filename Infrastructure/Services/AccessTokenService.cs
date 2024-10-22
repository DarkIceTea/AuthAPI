using Application.Abstractions;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        public string CreateAccessToken(CustomUser user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }; //TODO: Add Claim Role

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
    }
}
