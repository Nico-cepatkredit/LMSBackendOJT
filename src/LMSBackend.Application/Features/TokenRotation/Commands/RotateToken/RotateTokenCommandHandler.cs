using LMSBackend.Application.Common.Interfaces.IService;
using MediatR;

namespace LMSBackend.Application.Features.TokenRotation.Commands
{
    public class RotateTokenCommandHandler : IRequestHandler<RotateTokenCommand, (string NewAccessToken, string NewRefreshToken)>
    {
        private readonly IRefreshTokenService _refreshTokenService;

        public RotateTokenCommandHandler(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }

        public async Task<(string NewAccessToken, string NewRefreshToken)> Handle(RotateTokenCommand request, CancellationToken cancellationToken)
        {
            // Delegate token rotation logic to the service layer
            return await _refreshTokenService.RotateRefreshToken(request.UserId, request.DeviceId);
        }
    }
}
