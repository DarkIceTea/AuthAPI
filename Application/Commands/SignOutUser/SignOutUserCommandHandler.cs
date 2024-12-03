using Application.Abstractions;
using MediatR;

namespace Application.Commands.SignOutUser
{
    public class SignOutUserCommandHandler(IRefreshTokenService refreshTokenService) : IRequestHandler<SignOutUserCommand>
    {
        public async Task Handle(SignOutUserCommand request, CancellationToken cancellationToken)
        {
            var token = await refreshTokenService.GetRefreshTokenByValueIdAsync(request.RefreshToken, cancellationToken);
            refreshTokenService.DeleteRefreshTokenAsync(token, cancellationToken);
            refreshTokenService.SaveChangesAsync(cancellationToken);
        }
    }
}
