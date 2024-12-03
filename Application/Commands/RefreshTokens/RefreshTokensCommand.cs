using Application.Results;
using MediatR;

namespace Application.Commands.RefreshTokens
{
    public class RefreshTokensCommand : IRequest<Tokens>
    {
        public string RefreshToken { get; set; }
    }
}
