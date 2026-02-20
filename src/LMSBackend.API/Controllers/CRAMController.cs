using LMSBackend.API.Common.Responses;
using LMSBackend.Application.Features.CRAM.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LMSBackend.API.Controllers
{
    [ApiController]
    [Route("api/CRAM")]
    public class CRAMController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CRAMController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{status}/{loanAppCode}/{pnnumber?}")]
        public async Task<IActionResult> GetLoanAppDetails(
            string status, 
            string loanAppCode, 
            string? pnnumber = null
        )
        {
            var query = new LoanAppDetailsQuery 
            { 
                status = status, 
                loanAppCode = loanAppCode, 
                pnnumber = pnnumber 
            };

            var result = await _mediator.Send(query);
            
            return Ok(new APIResponse<object>{
                Message = "Records retrieved successfully",
                Code = "200",              
                Data = result
            });
        }
    }
}