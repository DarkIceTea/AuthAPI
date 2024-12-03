using MediatR;

namespace Application.Commands.SignOutUser
{
    public class SignOutUserCommand : IRequest
    {
        public string RefreshToken { get; set; }
    }
}
