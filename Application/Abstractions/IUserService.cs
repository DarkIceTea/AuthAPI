using Application.Results;
using Domain.Models;

namespace Application.Abstractions
{
    public interface IUserService
    {
        Task<Tokens> RegisterUserAsync(string email, string UserName, string password, string role, CancellationToken cancelationToken);
        Task<Tokens> LoginUserAsync(string email, string password, CancellationToken cancellationToken);
        Task<CustomUser> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IList<string>> GetUserRoles(CustomUser user, CancellationToken cancellationToken);
    }
}
