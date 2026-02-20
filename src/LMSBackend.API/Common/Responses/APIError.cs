using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMSBackend.API.Common.Responses
{
    public class APIError
    {    
        public string Field { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}