using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AuthDbContext(DbContextOptions options) : IdentityDbContext<CustomUser, CustomRole, Guid>(options)
    {
    }
}
