using Application.Results;

namespace Application.Abstractions
{
    public interface IUserService
    {
        public Tokens RegisterUser(string email, string password);
    }
}
