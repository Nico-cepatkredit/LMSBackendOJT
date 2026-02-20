using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace LMSBackend.Infrastructure.Persistence.Seed
{
    public static class SeedExtensions
    {
        public static void ApplyAllEntitySeeds(this ModelBuilder modelBuilder)
        {
            var seedTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(IEntitySeed).IsAssignableFrom(t)
                            && !t.IsInterface
                            && !t.IsAbstract);

            foreach (var seedType in seedTypes)
            {
                var seedInstance = (IEntitySeed)Activator.CreateInstance(seedType)!;
                seedInstance.Seed(modelBuilder);
            }
        }
    }
}