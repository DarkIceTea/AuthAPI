using Application.Abstractions;
using Application.Results;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class UserService(UserManager<CustomUser> userManager) : IUserService
    {
        Tokens IUserService.RegisterUser(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
