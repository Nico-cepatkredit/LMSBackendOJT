using LMSBackend.Application.Common.Interfaces.IRepository;
using LMSBackend.Domain.Entities;
using LMSBackend.Infrastructure.Persistence.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LMSBackend.Infrastructure.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly LMSDbContext _context;

        public RefreshTokenRepository(LMSDbContext context)
        {
            _context = context;
        }
        public async Task<RefreshToken?> GetRefreshTokenByUserIdAndDeviceIdAsync(Guid userId, Guid deviceId)
        {
            var parameters = new[]
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@DeviceId", deviceId)
            };

            var result = await _context.Set<RefreshToken>()
                .FromSqlRaw("EXEC USR.RefreshToken_GetByUserIdAndDeviceId @UserId=@UserId, @DeviceId=@DeviceId", parameters)
                .ToListAsync();

            var refreshToken = result?.FirstOrDefault();

            return refreshToken;
        }

        public async Task RevokeRefreshTokenAsync(Guid userId, Guid deviceId)
        {
            var parameters = new[]
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@DeviceId", deviceId)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC USR.RefreshToken_Revoke @UserId, @DeviceId", parameters);
        }

        public async Task SaveRefreshTokenAsync(RefreshToken refreshToken)
        {
            var parameters = new[]
            {
                new SqlParameter("@UserId", refreshToken.UserId),
                new SqlParameter("@Token", refreshToken.Token),
                new SqlParameter("@ExpiryDate", refreshToken.ExpiryDate),
                new SqlParameter("@IsRevoked", refreshToken.IsRevoked),
                new SqlParameter("@DeviceId", refreshToken.DeviceId)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC USR.RefreshToken_Insert @UserId, @Token, @ExpiryDate, @IsRevoked, @DeviceId", parameters);
        }

        public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken)
        {
            var parameters = new[]
            {
                new SqlParameter("@UserId", refreshToken.UserId),
                new SqlParameter("@Token", refreshToken.Token),
                new SqlParameter("@ExpiryDate", refreshToken.ExpiryDate),
                new SqlParameter("@IsRevoked", refreshToken.IsRevoked),
                new SqlParameter("@DeviceId", refreshToken.DeviceId)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC USR.RefreshToken_Update @UserId, @Token, @ExpiryDate, @IsRevoked, @DeviceId", parameters);
        }

        public async Task UpdateSessionPingDateAsync(Guid userId, Guid deviceId)
        {
            var parameters = new[]
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@DeviceId", deviceId)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC USR.UpdateSessionPingDate @UserId, @DeviceId", parameters);
        }
    }
}