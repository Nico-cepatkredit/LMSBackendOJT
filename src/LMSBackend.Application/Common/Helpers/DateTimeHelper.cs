using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMSBackend.Application.Common.Helpers
{
    public class DateTimeHelper
    {
        public static string? FormatDate(DateTime? date, bool includeTime = true)
        {
            if (!date.HasValue) return null;

            string format = includeTime
                ? "MM/dd/yyyy hh:mm:ss tt"
                : "MM/dd/yyyy";

            return date.Value.ToString(format);
        }
    }
}