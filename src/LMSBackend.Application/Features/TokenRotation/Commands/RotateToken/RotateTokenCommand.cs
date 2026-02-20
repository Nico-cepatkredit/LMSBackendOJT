using MediatR;

namespace LMSBackend.Application.Features.TokenRotation.Commands
{
    public class RotateTokenCommand : IRequest<(string NewAccessToken, string NewRefreshToken)>
    {
        public Guid UserId { get; set; }
        public Guid DeviceId { get; set; }
    }
}