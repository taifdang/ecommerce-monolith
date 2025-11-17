using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(Product));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.CategoryId)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>();

        //builder.Property(x => x.MinPrice)
        //    .IsRequired()
        //    .HasColumnType("decimal(18,2)");

        //builder.Property(x => x.MaxPrice)
        //    .IsRequired()
        //    .HasColumnType("decimal(18,2)");

        //builder.Property(x => x.Quantity)
        //    .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(3000);

        //builder.Property(x => x.Status)
        //    .IsRequired()
        //    .HasConversion<string>();

        builder.Property(x => x.Version)
            .IsConcurrencyToken();

        // Relationships
        builder.HasOne(x => x.Category)
            .WithMany(y => y.Products)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.ProductOptions)
            .WithOne()
            .HasForeignKey(po => po.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ProductVariants)
            .WithOne()
            .HasForeignKey(pv => pv.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ProductImages)
            .WithOne()
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
