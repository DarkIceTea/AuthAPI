using Domain.Models;

namespace Application.Abstractions
{
    public interface IRefreshTokenService
    {
        public RefreshToken CreateRefreshToken(CustomUser customUser);
    }
}
