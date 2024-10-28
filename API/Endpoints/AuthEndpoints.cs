using Application.Commands.LoginUser;
using Application.Commands.RefreshTokens;
using Application.Commands.RegisterUser;
using Application.Commands.SignOutUser;
using Application.Results;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints
{
    public static class AuthEndpoints
    {
        private static ISender _sender;
        public static void SetSender(ISender sender)
        {
            _sender = sender;
        }
        public static async Task<Tokens> Registration([FromBody] RegisterUserCommand command, IValidator<RegisterUserCommand> validator, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new Exception("User Invalid");

            return await _sender.Send(command, cancellationToken);
        }

        public static async Task<Tokens> Login([FromBody] LoginUserCommand command, CancellationToken cancellationToken)
        {
            return await _sender.Send(command, cancellationToken);
        }

        public static async Task SignOut([FromBody] SignOutUserCommand command, CancellationToken cancellationToken)
        {
            await _sender.Send(command, cancellationToken);
        }
        public static async Task<Tokens> Refresh([FromBody] RefreshTokensCommand command, CancellationToken cancellationToken)
        {
            return await _sender.Send(command, cancellationToken);
        }
    }
}
