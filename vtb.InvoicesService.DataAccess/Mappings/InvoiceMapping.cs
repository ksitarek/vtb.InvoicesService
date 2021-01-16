using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vtb.InvoicesService.Domain;

namespace vtb.InvoicesService.DataAccess.Mappings
{
    public class InvoiceMapping : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(x => x.InvoiceId);
            builder.OwnsMany(x => x.InvoicePositions, builder =>
            {
                builder.HasKey(x => x.InvoicePositionId);
                builder.Property(x => x.Value).HasPrecision(17, 4);
                builder.Property(x => x.Quantity).HasPrecision(17, 4);
                builder.Property(x => x.Summary).HasMaxLength(200);
                builder.Property(x => x.Description).HasMaxLength(500);
                builder.Property(x => x.Description).HasMaxLength(50);
                builder.OwnsOne(x => x.TaxInfo, b =>
                {
                    b.Property(x => x.TaxMultiplier).HasPrecision(4, 3).HasColumnName(nameof(TaxInfo.TaxMultiplier));
                    b.Property(x => x.TaxLabel).IsRequired().HasColumnName(nameof(TaxInfo.TaxLabel));
                });
            });

            builder.OwnsOne(x => x.InvoiceNumber, b =>
            {
                b.Property(x => x.FormattedNumber).IsRequired().HasMaxLength(50);
            });

            builder.Property(x => x.Currency).HasConversion(new EnumToStringConverter<Currency>());
            builder.Property(x => x.CalculationDirection).HasConversion(new EnumToStringConverter<CalculationDirection>());
            builder.Property(x => x.RowVersion).IsRowVersion();

            builder.Ignore(x => x.TaxSummaries);
        }
    }
}