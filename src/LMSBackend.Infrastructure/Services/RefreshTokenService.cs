using LMSBackend.Application.Common.Interfaces.IRepository;
using LMSBackend.Application.Common.Interfaces.IService;

namespace LMSBackend.Infrastructure.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokenService _tokenService;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository, ITokenService tokenService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _tokenService = tokenService;
        }

        public async Task<(string NewAccessToken, string NewRefreshToken)> RotateRefreshToken(Guid userId, Guid deviceId)
        {
            var existingRefreshToken = await _refreshTokenRepository.GetRefreshTokenByUserIdAndDeviceIdAsync(userId, deviceId);

            var phTimeThreshold = DateTime.UtcNow.AddHours(8).AddMinutes(-5);

            if (existingRefreshToken == null || existingRefreshToken.IsRevoked)
            {
                throw new UnauthorizedAccessException("Invalid or revoked refresh token.");
            }

            if (existingRefreshToken.SessionPingDate is null || existingRefreshToken.SessionPingDate.Value < phTimeThreshold)
            {
                await _refreshTokenRepository.RevokeRefreshTokenAsync(userId, deviceId);
                throw new UnauthorizedAccessException("User session is inactive.");
            }

            var newAccessToken = _tokenService.GenerateJwtToken(
                "userRole",
                "branch",
                "department",
                deviceId,
                userId
            );

            var newRefreshToken = _tokenService.GenerateRefreshToken();

            existingRefreshToken.Token = newRefreshToken;
            existingRefreshToken.ExpiryDate = DateTime.UtcNow.AddDays(1);
            existingRefreshToken.ModifiedAt = DateTime.UtcNow;
            existingRefreshToken.IsRevoked = false;

            await _refreshTokenRepository.UpdateRefreshTokenAsync(existingRefreshToken);

            return (newAccessToken, newRefreshToken);
        }
    }
}