using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMSBackend.Infrastructure.Persistence.Exceptions
{
    public sealed class DataAccessException : Exception
    {
        public DataAccessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}