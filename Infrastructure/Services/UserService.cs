using Application.Abstractions;
using Application.Results;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class UserService(UserManager<CustomUser> userManager, IAccessTokenService accessTokenService, IRefreshTokenService refreshTokenService) : IUserService
    {
        public async Task<Tokens> RegisterUserAsync(string email, string userName, string password, CancellationToken cancellationToken)
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

        public async Task<Tokens> LoginUserAsync(string email, string password, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null)
                throw new Exception("Wrong email or password");

            if (await userManager.CheckPasswordAsync(user, password))
                throw new Exception("Wrong email or password");

            var refreshToken = refreshTokenService.CreateRefreshToken(user);
            //userManager.

            var tokens = new Tokens()
            {
                AccessToken = accessTokenService.CreateAccessToken(user),
                RefreshToken = refreshToken.Token
            };
            return tokens;
        }
    }
}
