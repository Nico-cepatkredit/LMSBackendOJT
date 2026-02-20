using LMSBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSBackend.Infrastructure.Persistence.Configurations
{
    public class UserProcessTypeConfiguration : IEntityTypeConfiguration<UserProcessType>
    {
        public void Configure(EntityTypeBuilder<UserProcessType> builder)
        {
            builder.ToTable("UserProcessType", "USR");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.User)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Process)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Type)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.RecBy)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.RecDate)
                .HasColumnType("datetime2(7)")
                .IsRequired();

            builder.Property(u => u.ModBy)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(u => u.ModDate)
                .HasColumnType("datetime2(7)")
                .IsRequired(false);
        }
    }
}