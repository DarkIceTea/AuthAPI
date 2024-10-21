using Application.Abstractions;
using Application.Results;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class UserService(UserManager<CustomUser> userManager, IAccessTokenService accessTokenService, IRefreshTokenService refreshTokenService) : IUserService
    {
        public async Task<Tokens> RegisterUser(string email, string userName, string password)
        {
            var a = await userManager.FindByEmailAsync(email);
            if (a is not null)
                throw new Exception("User with same email exist");

            CustomUser user = new CustomUser() { Id = Guid.NewGuid(), UserName = userName, Email = email };
            RefreshToken refreshToken = refreshTokenService.CreateRefreshToken(user);
            user.RefreshToken = refreshToken;

            var res = await userManager.CreateAsync(user, password);

            var tokens = new Tokens()
            {
                AccessToken = accessTokenService.CreateAccessToken(user),
                RefreshToken = refreshToken.Token
            };
            return tokens;
        }
    }
}
