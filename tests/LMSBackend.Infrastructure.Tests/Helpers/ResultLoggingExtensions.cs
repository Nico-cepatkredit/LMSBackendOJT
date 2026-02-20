using System.Text.Json;

namespace LMSBackend.Infrastructure.Tests.Helpers
{
    public static class ResultLoggingExtensions
    {
        public static void LogResultPreview<T>(
            this IEnumerable<T> data,
            int maxRows = 5,
            string? title = null,
            JsonSerializerOptions? jsonOptions = null)
        {
            var rows = data.Take(maxRows).ToList();

            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine(
                title is null
                    ? $"RESULT PREVIEW ({rows.Count} row(s))"
                    : $"RESULT PREVIEW — {title} ({rows.Count} row(s))");
            Console.WriteLine("-------------------------------------------------");

            if (!rows.Any())
            {
                Console.WriteLine("<< NO DATA >>");
                Console.WriteLine("-------------------------------------------------");
                return;
            }

            var options = jsonOptions ?? new JsonSerializerOptions
            {
                WriteIndented = true
            };

            int i = 1;
            foreach (var row in rows)
            {
                Console.WriteLine($"Row {i++}:");
                Console.WriteLine(JsonSerializer.Serialize(row, options));
            }

            Console.WriteLine("-------------------------------------------------");
        }
    }
}