namespace LMSBackend.Infrastructure.Persistence.Seed
{
    public interface IRuntimeSeeder
    {
        Task SeedAsync(CancellationToken cancellationToken = default);
    }
}