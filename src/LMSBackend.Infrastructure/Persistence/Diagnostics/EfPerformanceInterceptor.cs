using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace LMSBackend.Infrastructure.Persistence.Diagnostics
{
    public class EfPerformanceInterceptor : DbCommandInterceptor
    {
        private readonly ILogger<EfPerformanceInterceptor> _logger;

        public EfPerformanceInterceptor(ILogger<EfPerformanceInterceptor> logger)
        {
            _logger = logger;
        }

        public override ValueTask<DbDataReader> ReaderExecutedAsync(
            DbCommand command,
            CommandExecutedEventData eventData,
            DbDataReader result,
            CancellationToken cancellationToken = default)
        {
            LogQuery(command, eventData);
            return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
        }

        // INSERT / UPDATE / DELETE / EXEC
        public override ValueTask<int> NonQueryExecutedAsync(
            DbCommand command,
            CommandExecutedEventData eventData,
            int result,
            CancellationToken cancellationToken = default)
        {
            LogQuery(command, eventData);
            return base.NonQueryExecutedAsync(command, eventData, result, cancellationToken);
        }

        private void LogQuery(
            DbCommand command,
            CommandExecutedEventData eventData)
        {
            var durationMs = eventData.Duration.TotalMilliseconds;

            if (durationMs < 200)
                return;

            _logger.LogWarning(
                """
                [EF][SLOW QUERY] {Duration} ms | DbContext={Context}
                  SQL:
                  {Sql}
                  Parameters:
                  {Parameters}
                """,
                durationMs,
                eventData.Context?.GetType().Name,
                command.CommandText,
                FormatParameters(command));
        }

        private static string FormatParameters(DbCommand command)
        {
            if (command.Parameters.Count == 0)
                return "  (none)";

            return string.Join(
                Environment.NewLine,
                command.Parameters
                    .Cast<DbParameter>()
                    .Select(p =>
                        $"  {p.ParameterName} = {p.Value}"));
        }
    }
}