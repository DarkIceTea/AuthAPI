using Application.Abstractions;
using Application.Results;
using MediatR;

namespace Application.Commands.RegisterUser
{
    public class RegisterUserCommandHandler(IUserService service) : IRequestHandler<RegisterUserCommand, Tokens>
    {
        public async Task<Tokens> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return await service.RegisterUserAsync(request.Email, request.UserName, request.Password, cancellationToken);
        }
    }
}
