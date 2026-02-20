using LMSBackend.Infrastructure.Persistence.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LMSBackend.Infrastructure.Persistence.Extensions
{
    public static class DbContextExtensions
    {
        public static async Task ExecuteStoredProcAsync(
            this DbContext context,
            string sql,
            CancellationToken ct,
            params SqlParameter[] parameters)
        {
            try
            {
                await context.Database.ExecuteSqlRawAsync(sql, parameters, ct);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(
                    "A database error occurred while executing a stored procedure.",
                    ex);
            }
        }

        public static async Task ExecuteStoredProcAsync(
            this DbContext context,
            string sql,
            SqlParameter outputParameter,
            CancellationToken ct,
            params SqlParameter[] parameters)
        {
            try
            {
                var allParams = parameters.Append(outputParameter).ToArray();

                await context.Database.ExecuteSqlRawAsync(sql, allParams, ct);
            }
            catch (SqlException ex)
            {
                throw new DataAccessException(
                    "A database error occurred while executing a stored procedure.",
                    ex);
            }
        }

    }
}