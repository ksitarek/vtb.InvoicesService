namespace vtb.InvoicesService.DataAccess.Mappings
{
    //public class InvoicePositionMapping : IEntityTypeConfiguration<InvoicePosition>
    //{
    //    public void Configure(EntityTypeBuilder<InvoicePosition> builder)
    //    {
    //        builder.HasKey(x => x.InvoicePositionId);
    //        builder.Property(x => x.Value).HasPrecision(17, 4);
    //        builder.Property(x => x.Quantity).HasPrecision(17, 4);
    //        builder.Property(x => x.Summary).HasMaxLength(200);
    //        builder.Property(x => x.Description).HasMaxLength(500);
    //        builder.Property(x => x.Description).HasMaxLength(50);
    //        builder.OwnsOne(x => x.TaxInfo, b =>
    //        {
    //            b.Property(x => x.TaxMultiplier).HasPrecision(4, 3).HasColumnName(nameof(TaxInfo.TaxMultiplier));
    //            b.Property(x => x.TaxLabel).IsRequired().HasColumnName(nameof(TaxInfo.TaxLabel));
    //        });
    //    }
    //}
}