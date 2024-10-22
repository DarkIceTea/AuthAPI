using Domain.Models;

namespace Application.Abstractions
{
    public interface IRefreshTokenService
    {
        public RefreshToken CreateRefreshToken(CustomUser customUser);
        public RefreshToken GetRefreshToken(CustomUser customUser);
        public void DeleteRefreshToken(RefreshToken refreshToken);
    }
}
