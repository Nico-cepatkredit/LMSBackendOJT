using LMSBackend.Application.Common.Interfaces.IRepository;
using LMSBackend.Application.Features.TokenRotation.Queries;
using LMSBackend.Domain.Entities;
using MediatR;

namespace LMSBackend.Application.Features.Token.Queries
{
    public class GetRefreshTokenQueryHandler : IRequestHandler<GetRefreshTokenQuery, RefreshToken>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public GetRefreshTokenQueryHandler(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<RefreshToken> Handle(GetRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            // Fetch the refresh token for the user and device
            var refreshToken = await _refreshTokenRepository.GetRefreshTokenByUserIdAndDeviceIdAsync(request.UserId, request.DeviceId);
            
            if (refreshToken == null || refreshToken.IsRevoked)
            {
                throw new UnauthorizedAccessException("No valid refresh token found.");
            }

            return refreshToken;
        }
    }
}
