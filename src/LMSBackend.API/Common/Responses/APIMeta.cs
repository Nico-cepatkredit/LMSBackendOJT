using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMSBackend.API.Common.Responses
{
    public class APIMeta
    {
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? Path { get; set; }
        public string? TraceId { get; set; }
        public object? Pagination { get; set; }
    }
}