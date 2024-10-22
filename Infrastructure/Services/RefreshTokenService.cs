using Application.Abstractions;
using Domain.Models;
using Infrastructure.Data;
using System.Security.Cryptography;

namespace Application.Services
{
    public class RefreshTokenService(AuthDbContext authDbContext) : IRefreshTokenService
    {
        public RefreshToken CreateRefreshToken(CustomUser customUser)
        {
            var refreshToken = new RefreshToken()
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTime.UtcNow,
                ExpirationTime = DateTime.UtcNow.AddDays(14),
                User = customUser,
                UserId = customUser.Id,
                Token = GenerateRefreshToken()
            };
            return refreshToken;
        }

        public void DeleteRefreshToken(RefreshToken refreshToken)
        {
            authDbContext.RefreshTokens.Remove(refreshToken);
            authDbContext.SaveChanges();
        }

        public RefreshToken GetRefreshToken(CustomUser customUser)
        {
            return authDbContext.RefreshTokens.Find(customUser.RefreshTokenId);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
