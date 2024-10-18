using Application.Results;

namespace Application.Abstractions
{
    public interface IUserService
    {
        Task<Tokens> RegisterUser(string email, string UserName, string password);
    }
}
