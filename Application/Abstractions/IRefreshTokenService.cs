using Domain.Models;

namespace Application.Abstractions
{
    public interface IRefreshTokenService
    {
        public Task<RefreshToken> CreateRefreshTokenAsync(CustomUser customUser, CancellationToken cancellationToken);
        public Task<RefreshToken> GetRefreshTokenByIdAsync(Guid guid, CancellationToken cancellationToken);
        public Task<RefreshToken> GetRefreshTokenByUserIdAsync(Guid guid, CancellationToken cancellationToken);
        public Task<RefreshToken> GetRefreshTokenByValueIdAsync(string refreshTokenValue, CancellationToken cancellationToken);
        public Task DeleteRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
        public Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
