using Application.Abstractions;
using Application.Results;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class UserService(UserManager<CustomUser> userManager) : IUserService
    {
        public async Task<Tokens> RegisterUser(string email, string userName, string password)
        {
            CustomUser user = new CustomUser() { UserName = userName, Email = email };
            var res = await userManager.CreateAsync(user, password);
            return new Tokens();
        }
    }
}
