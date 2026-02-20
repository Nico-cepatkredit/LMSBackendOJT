using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMSBackend.API.Common.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LMSBackend.API.Common.Filters
{
    public class APIResponseFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult &&
                objectResult.Value is not APIResponse<object>)
            {
                var httpContext = context.HttpContext;

                context.Result = new ObjectResult(new APIResponse<object>
                {
                    Success = true,
                    Message = "Request successful",
                    Data = objectResult.Value,
                    Meta =
                {
                    Path = httpContext.Request.Path,
                    TraceId = httpContext.TraceIdentifier
                }
                })
                {
                    StatusCode = objectResult.StatusCode
                };
            }
        }
    }
}