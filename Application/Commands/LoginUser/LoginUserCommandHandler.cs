using Application.Abstractions;
using Application.Results;
using MediatR;

namespace Application.Commands.LoginUser
{
    public class LoginUserCommandHandler(IUserService userService) : IRequestHandler<LoginUserCommand, Tokens>
    {
        public async Task<Tokens> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            return await userService.LoginUserAsync(command.Email, command.Password, cancellationToken);
        }
    }
}
