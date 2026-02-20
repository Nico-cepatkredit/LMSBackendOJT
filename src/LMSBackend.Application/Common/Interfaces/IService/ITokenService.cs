namespace LMSBackend.Application.Common.Interfaces.IService
{
    public interface ITokenService
    {
        string GenerateJwtToken(string role, string branch, string department, Guid deviceId, Guid userId);
        string GenerateRefreshToken();
    }
}