using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LMSBackend.Infrastructure.Persistence.SQLDesign.Interceptor;

namespace LMSBackend.Infrastructure.Tests.Helpers
{
    public static class EfCommandStoredProcedureFormatter
    {
        public static string ToStoredProcedure(
            this SqlCaptureInterceptor interceptor,
            string schema,
            string procedureName,
            IReadOnlyList<(string Name, string SqlType)> parameters)
        {
            // 1️⃣ Extract only UPDATE / INSERT / DELETE batches
            var commandBodies = interceptor.Commands
                .Select(ExtractMutationSql)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();

            if (!commandBodies.Any())
                throw new InvalidOperationException(
                    "No mutation SQL found to generate stored procedure.");

            // 2️⃣ Normalize EF parameters → SP parameters
            var sqlBody = string.Join("\n\n", commandBodies);

            sqlBody = NormalizeParameters(sqlBody);

            // 3️⃣ Build parameter declaration
            var paramBlock = string.Join(",\n    ",
                parameters.Select(p => $"{p.Name} {p.SqlType}"));

            return $"""
            =============================================================
            -- STORED PROCEDURE (GENERATED FROM EF CORE)
            =============================================================

            CREATE OR ALTER PROCEDURE {schema}.{procedureName}
                {paramBlock}
            AS
            BEGIN
                SET NOCOUNT ON;

                BEGIN TRAN;

            {Indent(sqlBody, 16)}

                COMMIT;
            END
            GO
            =============================================================
            """;
        }

        // =============================================================
        // Helpers
        // =============================================================

        private static string ExtractMutationSql(string raw)
        {
            var lines = raw
                .Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)
                .Select(l => l.TrimEnd())
                .ToList();

            var sb = new StringBuilder();

            foreach (var line in lines)
            {
                if (line.StartsWith("UPDATE ", StringComparison.OrdinalIgnoreCase) ||
                    line.StartsWith("INSERT ", StringComparison.OrdinalIgnoreCase) ||
                    line.StartsWith("DELETE ", StringComparison.OrdinalIgnoreCase) ||
                    line.StartsWith("SET NOCOUNT ON", StringComparison.OrdinalIgnoreCase))
                {
                    sb.AppendLine(line);
                }
                else if (sb.Length > 0 &&
                         !line.StartsWith("--") &&
                         !line.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase) &&
                         !line.StartsWith("OUTPUT", StringComparison.OrdinalIgnoreCase))
                {
                    sb.AppendLine(line);
                }
            }

            return sb.ToString().Trim();
        }

        private static string NormalizeParameters(string sql)
        {
            // Replace EF @p0, @p1, @__xxx with named parameters
            return Regex.Replace(
                sql,
                @"@\w+",
                match =>
                {
                    return match.Value switch
                    {
                        "@p0" => "@UserId",
                        "@p1" => "@GeneratedDate",
                        "@p2" => "@AssignmentId",
                        "@p3" => "@PNNumber",
                        "@p4" => "@LMSLoanAppId",
                        "@p5" => "@StatusId",
                        "@p6" => "@RemarksId",
                        _ => match.Value
                    };
                });
        }

        private static string Indent(string sql, int spaces)
        {
            var pad = new string(' ', spaces);
            return string.Join(
                "\n",
                sql.Split('\n').Select(l =>
                    string.IsNullOrWhiteSpace(l) ? "" : pad + l));
        }
    }
}
