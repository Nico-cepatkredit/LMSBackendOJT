using Microsoft.EntityFrameworkCore;

namespace LMSBackend.Infrastructure.Persistence.Seed
{
    public interface IEntitySeed
    {
        void Seed(ModelBuilder modelBuilder);
    }
}