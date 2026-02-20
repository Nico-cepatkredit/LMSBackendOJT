
using LMSBackend.Application.Features.CRAM.Dtos;
using MediatR;

namespace LMSBackend.Application.Features.CRAM.Queries
{
    public class LoanAppDetailsQuery : IRequest<LoanAppDetailsDto>
    {
        public string? loanAppCode { get; set; }
        public string? status { get; set; }
        public string? pnnumber { get; set; } = null;
    }
}