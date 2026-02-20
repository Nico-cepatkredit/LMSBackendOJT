
using LMSBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSBackend.Infrastructure.Persistence.Configurations
{
    public class BranchListConfiguration : IEntityTypeConfiguration<BranchList>
    {
        public void Configure(EntityTypeBuilder<BranchList> builder)
        {
            builder.ToTable("BranchList", "REF");

            builder.HasKey(x => x.Code);

            builder.Property(x => x.Code)
                    .ValueGeneratedNever()
                    .IsRequired();

            builder.Property(x => x.Name)
                   .HasColumnType("varchar(100)")
                   .IsRequired(false);

            builder.Property(x => x.Address)
                   .HasColumnType("varchar(200)")
                   .IsRequired(false);

            builder.Property(x => x.Description)
                   .HasColumnType("varchar(255)")
                   .IsRequired(false);

            builder.Property(x => x.Status)
                   .IsRequired();

            builder.Property(x => x.RecUser)
                   .HasColumnName("RecUser")
                   .HasColumnType("varchar(50)")
                   .HasConversion(
                       v => v.ToString(),
                       v => Guid.Parse(v)
                   )
                   .IsRequired();

            builder.Property(x => x.RecDate)
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(x => x.ModUser)
                   .HasColumnType("varchar(50)")
                   .HasConversion(
                       v => v.HasValue ? v.Value.ToString() : null,
                       v => string.IsNullOrEmpty(v) ? null : Guid.Parse(v)
                   )
                   .IsRequired(false);

            builder.Property(x => x.ModDate)
                   .HasColumnType("datetime")
                   .IsRequired(false);
        }
    }
}