using Application.Results;
using MediatR;

namespace Application.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<Tokens>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
