using Application.Results;

namespace Application.Abstractions
{
    public interface IUserService
    {
        Task<Tokens> RegisterUserAsync(string email, string UserName, string password, CancellationToken cancelationToken);
        Task<Tokens> LoginUserAsync(string email, string password, CancellationToken cancellationToken);
    }
}
