namespace LMSBackend.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string? Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsRevoked { get; set; }
        public Guid DeviceId { get; set; }
        public DateTime? SessionPingDate { get; set; } 
        public UserAccounts? User { get; set; }
    }
}