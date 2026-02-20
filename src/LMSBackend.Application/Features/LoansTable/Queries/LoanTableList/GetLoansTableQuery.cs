using LMSBackend.Application.Features.LoansTable.Dtos;
using MediatR;

namespace LMSBackend.Application.Features.LoansTable.Queries
{
    public class GetLoansTableQuery : IRequest<IEnumerable<LoansListDto>>
    {
        public string? Status { get; init; }
    }
}