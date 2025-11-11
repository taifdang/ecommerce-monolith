using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ProductOptionConfiguration : IEntityTypeConfiguration<ProductOption>
{
    public void Configure(EntityTypeBuilder<ProductOption> builder)
    {
        builder.ToTable(nameof(ProductOption));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.OptionName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => x.ProductId)
          .IsUnique()
          .HasFilter("[AllowImage] = 1");

        // Relationships
        builder.HasOne(x => x.Product)
            .WithMany(p => p.ProductOptions)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.OptionValues)
            .WithOne(y => y.ProductOption)
            .HasForeignKey(ov => ov.ProductOptionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
