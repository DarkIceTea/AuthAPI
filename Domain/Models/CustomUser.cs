using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class CustomUser : IdentityUser<Guid>
    {
        public string RefreshToken { get; set; }
    }
}
