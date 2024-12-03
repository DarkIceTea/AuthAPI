using Application.Abstractions;
using Application.Results;
using MediatR;

namespace Application.Commands.RefreshTokens
{
    public class RefreshTokensCommandHandler(IUserService userService, IAccessTokenService accessTokenService, IRefreshTokenService refreshTokenService) : IRequestHandler<RefreshTokensCommand, Tokens>
    {
        public async Task<Tokens> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await refreshTokenService.GetRefreshTokenByValueIdAsync(request.RefreshToken, cancellationToken);
            var user = await userService.GetUserByIdAsync(refreshToken.UserId, cancellationToken);

            var tokens = new Tokens()
            {
                RefreshToken = request.RefreshToken,
                AccessToken = accessTokenService.CreateAccessToken(user, cancellationToken)
            };
            return tokens;
        }
    }
}
