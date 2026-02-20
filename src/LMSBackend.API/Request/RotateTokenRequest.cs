namespace LMSBackend.API.Request
{
    public class RotateTokenRequest
    {
        public Guid UserId { get; set; }
        public Guid DeviceId { get; set; }
    }
}