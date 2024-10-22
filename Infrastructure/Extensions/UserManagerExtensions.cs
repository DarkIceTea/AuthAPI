using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Extensions
{
    public static class UserManagerExtensions
    {
        public static Task<IdentityResult> ChangeRefeshTokenAsync(CustomUser customUser, RefreshToken refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
