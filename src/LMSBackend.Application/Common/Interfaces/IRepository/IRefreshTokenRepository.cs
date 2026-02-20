using LMSBackend.Domain.Entities;

namespace LMSBackend.Application.Common.Interfaces.IRepository
{
    public interface IRefreshTokenRepository
    {
        Task SaveRefreshTokenAsync(RefreshToken refreshToken);
        Task UpdateRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken?> GetRefreshTokenByUserIdAndDeviceIdAsync(Guid userId, Guid deviceId);
        Task RevokeRefreshTokenAsync(Guid userId, Guid deviceId);
        Task UpdateSessionPingDateAsync(Guid userId, Guid deviceId);
    }
}