using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LMSBackend.Infrastructure.Tests.Helpers
{
    public static class EfCommandInspectionHelper
    {
        public static void DumpPendingChanges(DbContext context)
        {
            Console.WriteLine();
            Console.WriteLine("=================================");
            Console.WriteLine("=== EF PENDING CHANGES ==========");
            Console.WriteLine("=================================");

            var entries = context.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged)
                .ToList();

            if (!entries.Any())
            {
                Console.WriteLine("No pending changes.");
                return;
            }

            foreach (var entry in entries)
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}");
                Console.WriteLine($"State : {entry.State}");

                foreach (var prop in entry.Properties)
                {
                    if (entry.State == EntityState.Added ||
                        (entry.State == EntityState.Modified && prop.IsModified))
                    {
                        Console.WriteLine(
                            $"  {prop.Metadata.Name} = {prop.CurrentValue}");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
