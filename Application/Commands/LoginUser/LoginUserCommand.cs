using Application.Results;
using MediatR;

namespace Application.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<Tokens>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
