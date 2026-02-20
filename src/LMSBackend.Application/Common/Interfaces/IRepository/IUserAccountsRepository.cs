using LMSBackend.Application.Features.TokenRotation.Dtos;
using LMSBackend.Domain.Entities;

namespace LMSBackend.Application.Common.Interfaces.IRepository
{
    public interface IUserAccountsRepository
    {
        Task<UserAccounts?> GetUserById(Guid userId);

        Task<IEnumerable<UserAccounts?>> GetUserByRole(int role);
    }
}