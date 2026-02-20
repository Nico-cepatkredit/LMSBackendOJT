using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace LMSBackend.Application.Features.TokenRotation.Commands
{
    public record PingSessionCommand(Guid UserId, Guid DeviceId) : IRequest<Unit>;
}
