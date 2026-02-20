using LMSBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSBackend.Infrastructure.Persistence.Configurations
{
    public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccounts>
    {
        public void Configure(EntityTypeBuilder<UserAccounts> builder)
        {
            builder.ToTable("UserAccounts", "USR");
            // Primary Key
            builder.HasKey(u => u.Id);

            // Required Fields with correct data types
            builder.Property(u => u.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(u => u.MiddleName).HasMaxLength(50).IsRequired();
            builder.Property(u => u.LastName).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Suffix).HasMaxLength(5).IsRequired();
            builder.Property(u => u.Username).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Password).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(u => u.Role).IsRequired();
            builder.Property(u => u.Branch).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Department).HasMaxLength(50).IsRequired();
            builder.Property(u => u.MobileNo).HasMaxLength(11).IsRequired(false);
            builder.Property(u => u.FbProfile).HasMaxLength(200).IsRequired(false);
            builder.Property(u => u.AffiliateLink).HasMaxLength(15).IsRequired(false);
            builder.Property(u => u.AssignedUser).HasMaxLength(50).IsRequired(false);
            builder.Property(u => u.RecUser).HasMaxLength(100).IsRequired(false);

            // Date and Time Fields
            builder.Property(u => u.RecDate).HasColumnType("datetime").IsRequired(false);
            builder.Property(u => u.ModDate).HasColumnType("datetime").IsRequired(false);
            builder.Property(u => u.PasswordDate).HasColumnType("datetime").IsRequired(false);
            builder.Property(u => u.SessionPingDate).HasColumnType("datetime").IsRequired(false);

            // Other Properties
            builder.Property(u => u.ModUser).HasMaxLength(50).IsRequired(false);
            builder.Property(u => u.StatLock).IsRequired();
            builder.Property(u => u.SessionKeys).HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property(u => u.IsOnline).IsRequired();
            builder.Property(u => u.SessionTimeout).IsRequired();
            builder.Property(u => u.UpTime).HasMaxLength(10).IsRequired(false);
            builder.Property(u => u.UrlKey).HasMaxLength(25).IsRequired(false);
            builder.Property(u => u.Otp).HasMaxLength(6).IsRequired(false);
            builder.Property(u => u.OTPLock).IsRequired(false);
            builder.Property(u => u.OTPTimeStamp).HasColumnType("datetime").IsRequired(false);
            builder.Property(u => u.OtpRequired).IsRequired();
            builder.Property(u => u.Company).HasMaxLength(50).IsRequired(false);
            builder.Property(u => u.Birthdate).HasColumnType("varchar(50)").IsRequired(false);
        }
    }
}