using LMSBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BorrowersBackgroundConfiguration
    : IEntityTypeConfiguration<BorrowerBackground>
{
    public void Configure(EntityTypeBuilder<BorrowerBackground> builder)
    {
        builder.ToTable("BorrowerBackground", "CL");

        /* =========================
           PRIMARY KEY
           ========================= */
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("uniqueidentifier")
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        /* =========================
           IDENTIFIERS
           ========================= */
        builder.Property(x => x.BorrowersId)
            .HasColumnType("uniqueidentifier")
            .IsRequired(false);

        builder.Property(x => x.BorrowersType)
            .HasConversion<int>()   // enum → int
            .IsRequired(false);

        /* =========================
           CORE FIELDS
           ========================= */
        builder.Property(x => x.BackgroundType)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(250);

        builder.Property(x => x.Suffix)
            .HasColumnType("varchar(50)")
            .IsRequired(false);

        builder.Property(x => x.Age);

        builder.Property(x => x.Relationship)
            .HasMaxLength(250);

        builder.Property(x => x.Remarks)
            .HasMaxLength(500);

        /* =========================
           EMPLOYMENT
           ========================= */
        builder.Property(x => x.Position)
            .HasMaxLength(250);

        builder.Property(x => x.StartDate)
            .HasColumnType("varchar(20)");
        builder.Property(x => x.EndDate)
            .HasColumnType("varchar(20)");

        /* =========================
           LOANS
           ========================= */
        builder.Property(x => x.LoanApproval)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.Amortization)
            .HasColumnType("decimal(18,2)");

        /* =========================
           ENUMS
           ========================= */
        builder.Property(x => x.Affiliation)
            .HasConversion<int>()   // enum → int
            .IsRequired(false);

        builder.Property(x => x.Category)
            .HasConversion<int>()   // enum → int
            .IsRequired(false);

        /* =========================
           REFERENCES
           ========================= */
        builder.Property(x => x.ReferenceName)
            .HasMaxLength(250);

        builder.Property(x => x.ReferenceYear)
            .HasColumnType("varchar(80)");

        builder.Property(x => x.ContactNumber)
            .HasMaxLength(80);

        builder.Property(x => x.Province)
            .HasMaxLength(80);

        /* =========================
           AUDIT FIELDS
           ========================= */
        builder.Property(x => x.EncodedBy)
            .HasColumnType("uniqueidentifier")
            .IsRequired();

        builder.Property(x => x.EncodedDate)
            .HasColumnType("datetime2(7)")
            .HasDefaultValueSql("SYSDATETIME()")
            .IsRequired();

        builder.Property(x => x.UpdatedBy)
            .HasColumnType("uniqueidentifier")
            .IsRequired(false);

        builder.Property(x => x.UpdatedDate)
            .HasColumnType("datetime2(7)")
            .IsRequired(false);

        /* =========================
           INDEXES
           ========================= */
        builder.HasIndex(x => new { x.BorrowersId, x.BorrowersType });
        builder.HasIndex(x => x.BackgroundType);
    }
}
