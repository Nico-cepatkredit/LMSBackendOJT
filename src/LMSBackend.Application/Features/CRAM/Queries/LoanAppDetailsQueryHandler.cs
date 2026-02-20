using AutoMapper;
using LMSBackend.Application.Common.Interfaces.IRepository;
using LMSBackend.Application.Features.CRAM.Dtos;
using MediatR;

namespace LMSBackend.Application.Features.CRAM.Queries
{
    public class LoanAppDetailsQueryHandler : IRequestHandler<LoanAppDetailsQuery, LoanAppDetailsDto>
    {
        private readonly ICRAMRepository _CRAMRepository;
        private readonly IMapper _mapper;

        public LoanAppDetailsQueryHandler(ICRAMRepository CRAMRepository, IMapper mapper)
        {
            _CRAMRepository = CRAMRepository;
            _mapper = mapper;
        }

        public async Task<LoanAppDetailsDto> Handle(
            LoanAppDetailsQuery request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.loanAppCode))
            {
                throw new ArgumentNullException(nameof(request.loanAppCode), "Loan App Code cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(request.status))
            {
                throw new ArgumentNullException(nameof(request.status), "Status cannot be null or empty.");
            }
            var rawData = await _CRAMRepository.GetLoanAppDetailsAsync(request.loanAppCode, request.status, request.pnnumber);

            var formattedData = _mapper.Map<LoanAppDetailsDto>(rawData);

            return formattedData;
        }
    }
}