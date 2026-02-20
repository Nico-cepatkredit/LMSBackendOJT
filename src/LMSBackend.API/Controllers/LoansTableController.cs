using LMSBackend.API.Common.Responses;
using LMSBackend.API.Request;
using LMSBackend.Application.Features.LoansTable.Dtos;
using LMSBackend.Application.Features.LoansTable.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSBackend.API.Controllers
{
    [ApiController]
    [Route("api/loans-table")]
    public class LoansTableController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public LoansTableController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        private static string? FormatDate(DateTime? date)
        {
            return date.HasValue
                ? date.Value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss tt")
                : null;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetLoansTable([FromQuery] string? status)
        {
            var result = await _mediator.Send(
                new GetLoansTableQuery
                {
                    Status = status
                });

            if (!result.Any())
            {
                return Ok(new APIResponse<object>
                {
                    Success = true,
                    Message = "No records found",
                    Code = "TABLE_DATA_NOT_FOUND",
                });
            }

            var pagedData = result.Select(x => new
            {
                x.LMSLoanAppId,
                x.LoanAppCode,
                x.PNNumber,
                x.ProductId,
                x.BranchId,
                x.Origin,
                x.BorrowerFullName,
                x.RecUser,
                x.RecDate,
                x.ModUser,

                ModDate = FormatDate(x.ModDate),

                x.StatusName,

                actions = new LoanActionsDto
                {
                    GeneratePN = new LoanActionDto
                    {
                        By = x.PNGeneratedBy,
                        Date = FormatDate(x.PNGeneratedDate)
                    },
                    CancelRequest = new LoanActionDto
                    {
                        By = x.PNCancelRequestBy,
                        Date = FormatDate(x.PNCancelRequestDate)
                    },
                    CancelApproved = new LoanActionDto
                    {
                        By = x.PNCancelApprovedBy,
                        Date = FormatDate(x.PNCancelApprovedDate)
                    },
                    CancelDeclined = new LoanActionDto
                    {
                        By = x.PNCancelDeclinedBy,
                        Date = FormatDate(x.PNCancelDeclinedDate)
                    }
                }
            }).ToList();

            return Ok(new APIResponse<object>
            {
                Success = true,
                Message = "Successfully Get Data",
                Code = "200",
                Data = pagedData
            });
        }

        [HttpGet("status-count")]
        public async Task<IActionResult> GetStatusCounts()
        {
            var result = await _mediator.Send(
                new GetStatusCountsQuery());

            return Ok(new APIResponse<object>
            {
                Success = true,
                Message = "Records retrieved successfully",
                Code = "200",
                Data = result
            });
        }
    }
}