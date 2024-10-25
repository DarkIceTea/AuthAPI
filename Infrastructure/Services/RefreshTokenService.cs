using Application.Abstractions;
using Domain.Models;
using System.Security.Cryptography;

namespace Application.Services
{
    public class RefreshTokenService(IRefreshTokenRepository refreshTokenRepository) : IRefreshTokenService
    {
        public async Task<RefreshToken> CreateRefreshTokenAsync(CustomUser customUser, CancellationToken cancellationToken)
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
            await refreshTokenRepository.CreateTokenAsync(refreshToken, cancellationToken);
            return refreshToken;
        }

        public async Task DeleteRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
        {
            refreshTokenRepository.DeleteTokenAsync(refreshToken, cancellationToken);
        }

        public async Task<RefreshToken> GetRefreshTokenByIdAsync(Guid guid, CancellationToken cancellationToken)
        {
            return await refreshTokenRepository.GetRefreshTokenByIdAsync(guid, cancellationToken);
        }

        public async Task<RefreshToken> GetRefreshTokenByUserIdAsync(Guid guid, CancellationToken cancellationToken)
        {
            return await refreshTokenRepository.GetRefreshTokenByUserIdAsync(guid, cancellationToken);
        }

        public async Task<RefreshToken> GetRefreshTokenByValueIdAsync(string refreshTokenValue, CancellationToken cancellationToken)
        {
            return await refreshTokenRepository.GetRefreshTokenByValueAsync(refreshTokenValue, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await refreshTokenRepository.SaveChangesAsync(cancellationToken);
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
