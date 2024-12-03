using Application.Abstractions;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Resources;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RefreshTokenRepository(AuthDbContext authDbContext) : IRefreshTokenRepository
    {
        public async Task CreateTokenAsync(RefreshToken token, CancellationToken cancellationToken)
        {
            authDbContext.RefreshTokens.Add(token);
        }

        public async Task DeleteTokenAsync(RefreshToken token, CancellationToken cancellationToken)
        {
            authDbContext.RefreshTokens.Remove(token);
        }

        public async Task<RefreshToken> GetRefreshTokenByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await authDbContext.RefreshTokens.FindAsync(id, cancellationToken);
        }
        public async Task<RefreshToken> GetRefreshTokenByUserIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await authDbContext.RefreshTokens.FirstAsync(x => x.UserId == id, cancellationToken);
        }
        public async Task<RefreshToken> GetRefreshTokenByValueAsync(string refreshTokenValue, CancellationToken cancellationToken)
        {
            try
            {
                return await authDbContext.RefreshTokens.FirstAsync(x => x.Token == refreshTokenValue);
            }
            catch (Exception)
            {
                throw new Exception(ExceptionMessages.InvalidRefresh);
            }
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await authDbContext.SaveChangesAsync();
        }
    }
}
