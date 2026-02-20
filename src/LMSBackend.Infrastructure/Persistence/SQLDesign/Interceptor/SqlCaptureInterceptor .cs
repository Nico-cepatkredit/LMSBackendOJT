using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LMSBackend.Infrastructure.Persistence.SQLDesign.Interceptor
{
    public sealed class SqlCaptureInterceptor : DbCommandInterceptor
    {
        private readonly List<string> _commands = new();
        public IReadOnlyList<string> Commands => _commands;

        // ===============================
        // SYNC
        // ===============================

        public override InterceptionResult<int> NonQueryExecuting(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<int> result)
        {
            Capture(command);
            return base.NonQueryExecuting(command, eventData, result);
        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            Capture(command);
            return base.ReaderExecuting(command, eventData, result);
        }

        // ===============================
        // ASYNC (THIS IS WHAT YOU MISSED)
        // ===============================

        public override ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            Capture(command);
            return base.NonQueryExecutingAsync(
                command, eventData, result, cancellationToken);
        }

        public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result,
            CancellationToken cancellationToken = default)
        {
            Capture(command);
            return base.ReaderExecutingAsync(
                command, eventData, result, cancellationToken);
        }

        // ===============================
        // CAPTURE
        // ===============================

        private void Capture(DbCommand command)
        {
            var sb = new StringBuilder();

            sb.AppendLine("--------------------------------------------------");
            sb.AppendLine(command.CommandText);

            foreach (DbParameter p in command.Parameters)
            {
                sb.AppendLine($"-- {p.ParameterName} = {p.Value}");
            }

            _commands.Add(sb.ToString());
        }
    }
}
