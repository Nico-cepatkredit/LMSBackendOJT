using LMSBackend.Domain.Entities;
using MediatR;

namespace LMSBackend.Application.Features.TokenRotation.Queries
{
    public class GetRefreshTokenQuery : IRequest<RefreshToken>
    {
        public Guid UserId { get; set; }
        public Guid DeviceId { get; set; }
    }
}