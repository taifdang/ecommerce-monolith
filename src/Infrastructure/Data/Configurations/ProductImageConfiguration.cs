using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable(nameof(ProductImage));  

        builder.HasKey(pi => pi.Id);
        builder.Property(pi => pi.Id)
            .ValueGeneratedOnAdd();

        builder.Property(pi => pi.ImageUrl)
            .HasMaxLength(255);

        builder.HasIndex(pi => new { pi.ProductId})
            .IsUnique()
            .HasFilter("[IsMain] = 1 AND [OptionValueId] IS NULL"); // This ensures only one main image per product without option value
        
        builder.HasIndex(pi => new { pi.ProductId, pi.OptionValueId })
            .IsUnique()
            .HasFilter("[OptionValueId] IS NOT NULL"); // This ensures only one image per product-option value combination


        // Relationships
        builder.HasOne(pi => pi.Product)
            .WithMany(p => p.ProductImages)
            .HasForeignKey(pi => pi.ProductId);
            //.OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(pi => pi.OptionValue)
            .WithMany(ov => ov.ProductImages)
            .HasForeignKey(pi => pi.OptionValueId);
            //.OnDelete(DeleteBehavior.SetNull);

    }
}
