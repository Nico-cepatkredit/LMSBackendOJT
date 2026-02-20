using LMSBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AddressConfiguration : IEntityTypeConfiguration<BorrowerAddress>
{
    public void Configure(EntityTypeBuilder<BorrowerAddress> builder)
    {
        builder.ToTable("BorrowerAddress", "CL");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(x => x.BorrowersId)
            .IsRequired();

        builder.Property(x => x.BorrowersType)
            .IsRequired();

        builder.Property(x => x.Province)
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(x => x.Municipality)
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(x => x.Barangay)
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(x => x.Street)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.LandMark)
            .HasMaxLength(150)
            .IsRequired(false);

        builder.Property(x => x.StayYears)
            .IsRequired(false);

        builder.Property(x => x.StayMonths)
            .IsRequired(false);

        builder.Property(x => x.CollectionArea)
            .HasMaxLength(80)
            .IsRequired(false);

        builder.Property(x => x.ProofOfBilling)
            .HasMaxLength(80)
            .IsRequired(false);

        builder.HasIndex(x => new { x.BorrowersId, x.BorrowersType });
        builder.Property(x => x.TypeOfResidence).IsRequired();
        builder.Property(x => x.ResidenceAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired(false);
    }
}