using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class OptionValueConfiguration : IEntityTypeConfiguration<OptionValue>
{
    public void Configure(EntityTypeBuilder<OptionValue> builder)
    {
        builder.ToTable(nameof(OptionValue));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ProductOptionId)
            .IsRequired();

        builder.Property(x => x.Value)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Label)
            .HasMaxLength(100);

        // Relationships
        builder.HasOne(x => x.ProductOption)
            .WithMany(po => po.OptionValues)
            .HasForeignKey(x => x.ProductOptionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.VariantOptionValues)
            .WithOne(po => po.OptionValue)
            .HasForeignKey(x => x.OptionValueId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.ProductImages)
          .WithOne(po => po.OptionValue)
          .HasForeignKey(x => x.OptionValueId)
          .OnDelete(DeleteBehavior.NoAction);
    }
}
