using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMSBackend.Application.Common.Interfaces.IService
{
    public interface IRefreshTokenService
    {
        Task<(string NewAccessToken, string NewRefreshToken)> RotateRefreshToken(Guid userId, Guid deviceId);
    }
}