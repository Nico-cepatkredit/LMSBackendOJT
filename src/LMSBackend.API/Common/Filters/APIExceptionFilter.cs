using LMSBackend.API.Common.Responses;
using LMSBackend.Domain.Exceptions;
using LMSBackend.Infrastructure.Persistence.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;

public class APIExceptionFilter : IExceptionFilter
{
    private readonly ILogger<APIExceptionFilter> _logger;
    public APIExceptionFilter(ILogger<APIExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var traceId = context.HttpContext.TraceIdentifier;
        var path = context.HttpContext.Request.Path;

        _logger.LogError(
            context.Exception,
            "Unhandled API exception. Path: {Path}, TraceId: {TraceId}",
            path,
            traceId
        );

        APIResponse<object> response;

        switch (context.Exception)
        {
            case AlreadyMigratedException ex:
                response = new APIResponse<object>
                {
                    Success = false,
                    Message = "Loan already migrated",
                    Errors = new { loanAppId = ex.LoanAppId },
                    Code = "ALREADY_MIGRATED",
                    Meta = new APIMeta
                    {
                        Path = path,
                        TraceId = traceId
                    }
                };

                context.Result = new ConflictObjectResult(response);
                break;

            case LOSLoanAppIdNotFoundException ex:
                response = new APIResponse<object>
                {
                    Success = false,
                    Message = ex.Message,
                    Errors = null,
                    Code = ex.Code,
                    Meta = new APIMeta
                    {
                        Path = path,
                        TraceId = traceId
                    }
                };

                context.Result = new NotFoundObjectResult(response);
                break;
            case DomainException ex:
                response = new APIResponse<object>
                {
                    Success = false,
                    Message = ex.Message,
                    Code = ex.Code,
                    Meta = new APIMeta
                    {
                        Path = path,
                        TraceId = traceId
                    }
                };

                context.Result = new BadRequestObjectResult(response);
                break;
            /* ---------------- DATA ACCESS / STORED PROC ERRORS ---------------- */

            case DataAccessException ex
                when ex.InnerException is SqlException sqlEx:

                // 🔴 LOG THE REAL SQL ERROR
                _logger.LogError(
                    sqlEx,
                    "Stored procedure error. SPCode: {SPCode}, Path: {Path}, TraceId: {TraceId}",
                    sqlEx.Message,
                    path,
                    traceId);

                response = new APIResponse<object>
                {
                    Success = false,
                    Message = sqlEx.Message,
                    Code = sqlEx.Number.ToString(),
                    Meta = new APIMeta
                    {
                        Path = path,
                        TraceId = traceId
                    }
                };

                context.Result = new ObjectResult(response)
                {
                    StatusCode = sqlEx.Number == 51001 ? StatusCodes.Status409Conflict : StatusCodes.Status400BadRequest
                };
                break;

            default:
                response = new APIResponse<object>
                {
                    Success = false,
                    Message = "Unexpected server error",
                    Code = "INTERNAL_SERVER_ERROR",
                    Meta = new APIMeta
                    {
                        Path = path,
                        TraceId = traceId
                    }
                };

                context.Result = new ObjectResult(response)
                {
                    StatusCode = 500
                };
                break;
        }

        context.ExceptionHandled = true;
    }
}