using Domain.Models;

namespace Application.Abstractions
{
    public interface IRefreshTokenRepository
    {
        public Task<RefreshToken> GetRefreshTokenByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<RefreshToken> GetRefreshTokenByUserIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<RefreshToken> GetRefreshTokenByValueAsync(string refreshTokenValue, CancellationToken cancellationToken);

        public Task CreateTokenAsync(RefreshToken token, CancellationToken cancellationToken);
        public Task DeleteTokenAsync(RefreshToken token, CancellationToken cancellationToken);
        public Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
