using Application.Abstractions;
using Application.Results;
using Domain.Models;
using Infrastructure.Resources;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class UserService(UserManager<CustomUser> userManager, IAccessTokenService accessTokenService, IRefreshTokenService refreshTokenService) : IUserService
    {
        public async Task<Tokens> RegisterUserAsync(string email, string userName, string password, CancellationToken cancellationToken)
        {
            if (await userManager.FindByEmailAsync(email) is not null)
                throw new Exception(ExceptionMessages.UserExist);

            var user = new CustomUser() { Id = Guid.NewGuid(), UserName = userName, Email = email };
            var res = await userManager.CreateAsync(user, password);

            var refreshToken = await refreshTokenService.CreateRefreshTokenAsync(user, cancellationToken);
            user.RefreshToken = refreshToken;


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
                throw new Exception(ExceptionMessages.WrongUser);

            if (!await userManager.CheckPasswordAsync(user, password))
                throw new Exception(ExceptionMessages.WrongUser);

            if (user.RefreshTokenId is not null)
            {
                var a = await refreshTokenService.GetRefreshTokenByIdAsync((Guid)user.RefreshTokenId, cancellationToken);
                refreshTokenService.DeleteRefreshTokenAsync(a, cancellationToken);
            }

            var refreshToken = await refreshTokenService.CreateRefreshTokenAsync(user, cancellationToken);

            refreshTokenService.SaveChangesAsync(cancellationToken);

            var tokens = new Tokens()
            {
                AccessToken = accessTokenService.CreateAccessToken(user),
                RefreshToken = refreshToken.Token
            };
            return tokens;
        }
    }
}
