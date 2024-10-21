using Domain.Models;

namespace Application.Abstractions
{
    public interface IAccessTokenService
    {
        public string CreateAccessToken(CustomUser user);
    }
}
