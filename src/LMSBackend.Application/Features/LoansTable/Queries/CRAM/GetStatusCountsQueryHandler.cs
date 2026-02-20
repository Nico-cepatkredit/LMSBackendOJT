
using LMSBackend.Application.Common.Interfaces.IRepository;
using LMSBackend.Application.Features.LoansTable.Dtos;
using LMSBackend.Application.Features.LoansTable.Queries;
using MediatR;

namespace LMSBackend.Application.Features.StatusNav.Queries
{
    public class GetStatusCountsQueryHandler : IRequestHandler<GetStatusCountsQuery, IEnumerable<StatusCountDto>>
    {
        private readonly ILoansTableRepository _loansTableRepository;

        public GetStatusCountsQueryHandler(ILoansTableRepository loansTableRepository)
        {
            _loansTableRepository = loansTableRepository;
        }

        public async Task<IEnumerable<StatusCountDto>> Handle(GetStatusCountsQuery request,
            CancellationToken cancellationToken)
        {
            return await _loansTableRepository.GetStatusCountsAsync();
        }
        
    }
}