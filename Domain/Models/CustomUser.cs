using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class CustomUser : IdentityUser<Guid>
    {
        public Guid? RefreshTokenId { get; set; }
        public RefreshToken? RefreshToken { get; set; }
    }
}
