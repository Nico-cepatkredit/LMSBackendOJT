using MediatR;
using System;

namespace LMSBackend.Application.Features.TokenRotation.Commands
{
    public class CreateTokenCommand : IRequest<(string AccessToken, string RefreshToken)>
    {
        public Guid UserId { get; set; }
        public Guid DeviceId { get; set; }
    }
}
