using LMSBackend.Domain.Entities;
using LMSBackend.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BorrowerDetailsConfiguration : IEntityTypeConfiguration<BorrowerDetails>
{
    public void Configure(EntityTypeBuilder<BorrowerDetails> builder)
    {
        builder.ToTable("BorrowerDetails", "CL");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(x => x.LMSLoanAppID)
            .IsRequired();

        builder.Property(x => x.BorrowersType).IsRequired();
        builder.Property(x => x.Suffix).IsRequired();
        builder.Property(x => x.Gender).IsRequired();
        builder.Property(x => x.Relationships).IsRequired();
        builder.Property(x => x.MaritalStatus).IsRequired();
        builder.Property(x => x.Religion).IsRequired();
        builder.Property(x => x.EducationStatus).IsRequired(false);

        builder.Property(x => x.BorrowersCode)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.FirstName)
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(x => x.MiddleName)
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(x => x.MobileNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.MobileNumber2)
            .HasMaxLength(50);

        builder.Property(x => x.SocialMedia)
            .HasMaxLength(150);

        builder.Property(x => x.GroupChat)
            .HasMaxLength(150);

        builder.Property(x => x.School)
            .HasMaxLength(250)
            .IsRequired(false);

        builder.Property(x => x.Course)
            .HasMaxLength(150)
            .IsRequired(false);

        builder.Property(x => x.PEP)
            .IsRequired();

        builder.Property(x => x.IsOFW)
            .IsRequired();

        builder.Property(x => x.isPBCBMarried)
            .IsRequired(false);

        builder.Property(x => x.RelationshipsToBene)
            .IsRequired(false);

        builder.Property(x => x.RelationshipsToACB)
            .IsRequired(false);

        builder.Property(x => x.SpouseName)
            .HasColumnType("varchar(80)")
            .IsRequired(false);

        builder.Property(x => x.SpouseBirthdate)
            .IsRequired(false);

        builder.Property(x => x.SpouseSourceOfIncome)
            .IsRequired(false);

        builder.Property(x => x.CBACBSourceOfIncome)
            .HasColumnType("decimal(18,2)")
            .IsRequired(false);

        builder.Property(x => x.SpouseIncome)
            .HasPrecision(18, 2)
            .IsRequired(false);

        builder.Property(x => x.AddressId)
            .HasColumnType("varchar(50)")
            .IsRequired(false);

        // Relationship + constraint
        builder.HasOne(x => x.Loan)
            .WithMany(x => x.Borrowers)
            .HasForeignKey(x => x.LMSLoanAppID)
            .HasConstraintName("FK_BorrowerDetails_LoanDetails")
            .OnDelete(DeleteBehavior.Restrict);

        // Index
        builder.HasIndex(x => x.LMSLoanAppID)
            .HasDatabaseName("IX_BorrowerDetails_LMSLoanAppID");
    }
}
