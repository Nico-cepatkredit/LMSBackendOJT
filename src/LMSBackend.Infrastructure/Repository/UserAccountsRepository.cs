using LMSBackend.Application.Common.Interfaces.IRepository;
using LMSBackend.Domain.Entities;
using LMSBackend.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using LMSBackend.Application.Features.TokenRotation.Dtos;

namespace LMSBackend.Infrastructure.Repository
{
    public class UserAccountsRepository : IUserAccountsRepository
    {
        private readonly LMSDbContext _context;
        public UserAccountsRepository(LMSDbContext context)
        {
            _context = context;
        }

        public async Task<UserAccounts?> GetUserById(Guid userId)
        {
            var param = new SqlParameter("@UserId", userId);

            return _context.Set<UserAccounts>()
                .FromSqlRaw("EXEC [USR].[UserAccount_GetById] @UserId", param)
                .AsNoTracking()
                .AsEnumerable()
                .FirstOrDefault() ?? null;
        }

        public async Task<IEnumerable<UserAccounts?>> GetUserByRole(int role)
        {
            var param = new SqlParameter("@Role", role);

            var result = await _context.Set<UserAccounts>()
                .FromSqlRaw("EXEC [USR].[UserAccount_GetByRole] @Role", param)
                .AsNoTracking()
                .ToListAsync();

            return result;
        }
    }
}