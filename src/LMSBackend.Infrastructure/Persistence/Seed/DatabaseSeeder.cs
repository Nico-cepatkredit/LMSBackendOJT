namespace LMSBackend.Infrastructure.Persistence.Seed
{
    public class DatabaseSeeder
    {
        private readonly IEnumerable<IRuntimeSeeder> _seeders;

        public DatabaseSeeder(IEnumerable<IRuntimeSeeder> seeders)
        {
            _seeders = seeders;
        }

        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            foreach (var seeder in _seeders)
            {
                await seeder.SeedAsync(cancellationToken);
            }
        }
    }
}
