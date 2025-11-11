using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class VariantOptionValueConfiguration : IEntityTypeConfiguration<VariantOptionValue>
{
    public void Configure(EntityTypeBuilder<VariantOptionValue> builder)
    {
        builder.ToTable(nameof(VariantOptionValue));

        builder.HasKey(x => new { x.ProductVariantId, x.OptionValueId });

        builder.HasIndex(ci => ci.OptionValueId);
        builder.HasIndex(ci => ci.ProductVariantId);

        // Relationships
        builder.HasOne(x => x.ProductVariant)
            .WithMany(pv => pv.VariantOptionValues)
            .HasForeignKey(vov => vov.ProductVariantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.OptionValue)
            .WithMany(y => y.VariantOptionValues)
            .HasForeignKey(x => x.OptionValueId)
            //.HasForeignKey(vov => vov.OptionValueId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}
