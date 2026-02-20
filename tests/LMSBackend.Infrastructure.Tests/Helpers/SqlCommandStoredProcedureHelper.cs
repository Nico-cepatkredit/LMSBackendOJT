using System;
using System.Text;
using LMSBackend.Infrastructure.Persistence.SQLDesign.Interceptor;

namespace LMSBackend.Infrastructure.Tests.Helpers
{
    public static class SqlCommandStoredProcedureHelper
    {
        public static string WrapAsStoredProcedure(
            this SqlCaptureInterceptor interceptor,
            string schema,
            string procedureName,
            string parameters)
        {
            var body = new StringBuilder();

            foreach (var cmd in interceptor.Commands)
            {
                body.AppendLine(cmd);
                body.AppendLine();
            }

            return $"""
            =============================================================
            -- STORED PROCEDURE TEMPLATE (EF COMMAND)
            =============================================================

            CREATE OR ALTER PROCEDURE {schema}.{procedureName}
                {parameters}
            AS
            BEGIN
                SET NOCOUNT ON;
                BEGIN TRAN;

            {Indent(body.ToString(), 16)}

                COMMIT;
            END
            GO
            =============================================================
            """;
        }

        private static string Indent(string sql, int spaces)
        {
            var pad = new string(' ', spaces);
            var lines = sql.Split(
                new[] { "\r\n", "\n" },
                System.StringSplitOptions.None);

            var sb = new StringBuilder();
            foreach (var line in lines)
                sb.AppendLine(string.IsNullOrWhiteSpace(line) ? "" : pad + line);

            return sb.ToString().TrimEnd();
        }
    }
}
