using LMSBackend.Application.Common.Interfaces.IRepository;
using LMSBackend.Application.Features.LoansTable.Dtos;
using MediatR;

namespace LMSBackend.Application.Features.LoansTable.Queries
{
    public class GetLoansQueryHandler : IRequestHandler<GetLoansTableQuery, IEnumerable<LoansListDto>>
    {
        private readonly ILoansTableRepository _loansTableRepository;

        public GetLoansQueryHandler(ILoansTableRepository loansTableRepository)
        {
            _loansTableRepository = loansTableRepository;
        }

        public async Task<IEnumerable<LoansListDto>> Handle(
            GetLoansTableQuery request,
            CancellationToken cancellationToken)
        {
            return await _loansTableRepository
                .GetLoansListAsync(request.Status);
        }
    }
}