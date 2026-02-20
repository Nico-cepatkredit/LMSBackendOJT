using LMSBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSBackend.Infrastructure.Persistence.Configurations
{
    public class StatusListConfiguration : IEntityTypeConfiguration<StatusList>
    {
        public void Configure(EntityTypeBuilder<StatusList> builder)
        {
            builder.ToTable("StatusList", "ILP");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(x => x.Name)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnType("bit")
                .HasDefaultValue(false)
                .IsRequired();
        }
    }
}