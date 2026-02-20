using LMSBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSBackend.Infrastructure.Persistence.Configurations
{
    public class LoanChargesDetailsConfiguration : IEntityTypeConfiguration<LoanChargesDetails>
    {
        public void Configure(EntityTypeBuilder<LoanChargesDetails> builder)
        {
            builder.ToTable("LoanChargesDetails", "ILP");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Code)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100);

            builder.Property(x => x.Amount)
                .HasPrecision(18, 2);

            builder.Property(x => x.Type)
                .HasMaxLength(50);

            builder.Property(x => x.IsDefault)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(x => x.IsDropdown)
                .HasDefaultValue(0)
                .IsRequired();

            builder.HasIndex(x => x.Code)
                .IsUnique();
        }
    }
}