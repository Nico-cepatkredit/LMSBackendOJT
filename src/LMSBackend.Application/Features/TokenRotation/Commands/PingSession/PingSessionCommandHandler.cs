using System.Text.Json;
using LMSBackend.Application.Common.Interfaces.IRepository;
using LMSBackend.Application.Features.TokenRotation.Commands;
using MediatR;

namespace LMSBackend.Application.Features.Session.Commands
{
    public class PingSessionCommandHandler : IRequestHandler<PingSessionCommand, Unit>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public PingSessionCommandHandler(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        // Handle method for PingSessionCommand that updates the SessionPingDate
        public async Task<Unit> Handle(PingSessionCommand request, CancellationToken cancellationToken)
        {

            Console.WriteLine($"UserId: {request.UserId} | Type: {request.UserId.GetType()}");
            Console.WriteLine($"DeviceId: {request.DeviceId} | Type: {request.DeviceId.GetType()}");
            var phTimeThreshold = DateTime.UtcNow.AddHours(8).AddMinutes(-5);

            var refreshToken = await _refreshTokenRepository.GetRefreshTokenByUserIdAndDeviceIdAsync(request.UserId, request.DeviceId);
            if (refreshToken == null || refreshToken.IsRevoked)
            {
                throw new UnauthorizedAccessException("Invalid or revoked refresh token.");
            }

            if (refreshToken.SessionPingDate is null || refreshToken.SessionPingDate < phTimeThreshold)
            {   
                await _refreshTokenRepository.RevokeRefreshTokenAsync(request.UserId, request.DeviceId);
                throw new UnauthorizedAccessException("Cannot ping anymore. User is inactive for too long.");
            }

            await _refreshTokenRepository.UpdateSessionPingDateAsync(request.UserId, request.DeviceId);
            return Unit.Value;
        }
    }
}
