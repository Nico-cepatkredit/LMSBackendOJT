using System;
using LMSBackend.Infrastructure.Persistence.SQLDesign.Interceptor;

namespace LMSBackend.Infrastructure.Tests.Helpers
{
    public static class SqlCaptureDumpHelper
    {
        public static void DumpCapturedSql(SqlCaptureInterceptor interceptor)
        {
            Console.WriteLine();
            Console.WriteLine("=================================");
            Console.WriteLine("=== GENERATED SQL COMMANDS ======");
            Console.WriteLine("=================================");

            int i = 1;
            foreach (var sql in interceptor.Commands)
            {
                Console.WriteLine($"--- Command #{i++} ---");
                Console.WriteLine(sql);
            }
        }
    }
}
