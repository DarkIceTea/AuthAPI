using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Data
{
    internal class AuthDbContext : IdentityDbContext<CustomUser, CustomRole, Guid>
    {

    }
}
