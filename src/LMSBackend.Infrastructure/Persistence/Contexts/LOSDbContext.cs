

using Microsoft.EntityFrameworkCore;

namespace LMSBackend.Infrastructure.Persistence.Contexts
{
    public class LOSDbContext : DbContext
    {
        public LOSDbContext(DbContextOptions<LOSDbContext> options)
            : base(options)
        {
        }

    }
}