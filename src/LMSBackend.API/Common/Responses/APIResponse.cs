using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMSBackend.API.Common.Responses
{
    public class APIResponse<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public object? Errors { get; set; }
        public string? Code { get; set; }
        public string? SpCode { get; set; }
        public APIMeta Meta { get; set; } = new();
    }
}