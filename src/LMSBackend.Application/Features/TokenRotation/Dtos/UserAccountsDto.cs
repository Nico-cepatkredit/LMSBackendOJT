namespace LMSBackend.Application.Features.TokenRotation.Dtos
{
    public class UserAccountsDto
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string MiddleName { get; set; }
        public required string LastName { get; set; }
        public required string Suffix { get; set; }
        public required string Birthdate { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Department { get; set; }
        public int Role { get; set; }
        public required string Branch { get; set; }
        public string? MobileNo { get; set; }
        public string? FbProfile { get; set; }
        public string? AffiliateLink { get; set; }
        public int Stat { get; set; }
        public string? AssignedUser { get; set; }
        public string? RecUser { get; set; }
        public string? RecDate { get; set; }
        public string? ModUser { get; set; }
        public string? ModDate { get; set; }
        public int StatLock { get; set; }
        public string? PasswordDate { get; set; }
        public string? SessionKeys { get; set; }
        public bool IsOnline { get; set; }
        public int SessionTimeout { get; set; }
        public string? UpTime { get; set; }
        public string? UrlKey { get; set; }
        public string? Otp { get; set; }
        public int? OTPLock { get; set; }
        public string? OTPTimeStamp { get; set; }
        public int OtpRequired { get; set; }
        public string? Company { get; set; }
        public string? SessionPingDate { get; set; }
    }
}