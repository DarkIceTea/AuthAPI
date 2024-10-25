using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AuthDbContext(DbContextOptions options) : IdentityDbContext<CustomUser, IdentityRole<Guid>, Guid>(options)
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomUser>()
                .HasOne(x => x.RefreshToken)
                .WithOne(r => r.User)
                .HasForeignKey<CustomUser>(u => u.RefreshTokenId);
        }
    }
}
