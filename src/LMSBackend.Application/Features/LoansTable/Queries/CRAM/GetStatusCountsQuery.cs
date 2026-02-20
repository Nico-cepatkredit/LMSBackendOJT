
using LMSBackend.Application.Features.LoansTable.Dtos;
using MediatR;

namespace LMSBackend.Application.Features.LoansTable.Queries
{
    public class GetStatusCountsQuery : IRequest<IEnumerable<StatusCountDto>>
    {  
    }
}