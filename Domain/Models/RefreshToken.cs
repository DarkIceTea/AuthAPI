namespace Domain.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset ExpirationTime { get; set; }
        public Guid UserId { get; set; }
        public CustomUser User { get; set; }
    }
}