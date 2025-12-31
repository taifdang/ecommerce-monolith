namespace Infrastructure.Data.Seed;

public static class InitialData
{
    public static List<Domain.Entities.Category> Categories { get; set; }
    public static List<Domain.Entities.Product> Products { get; set; }
    public static List<Domain.Entities.ProductOption> ProductOptions { get; set; }
    public static List<Domain.Entities.OptionValue> OptionValues { get; set; }
    public static List<Domain.Entities.ProductImage> ProductImages { get; set; }
    public static List<Domain.Entities.Image> Images { get; set; }
    public static List<Domain.Entities.Variant> Variants { get; set; }
    public static List<Domain.Entities.VariantOption> VariantOptions { get; set; }

    public static decimal PriceRnd = Random.Shared.Next(10000, 100000);
    public static int QuantityRnd = Random.Shared.Next(10, 100);

    static InitialData()
    {
        // category
        Categories = new List<Domain.Entities.Category>
        {
           new Domain.Entities.Category { Id = new Guid("3c5c0000-97c6-fc34-a0cb-08db322230c8"), Name = "Shirt", UrlSlug = "shirt"},
           new Domain.Entities.Category { Id = new Guid("3c5c0000-97c6-fc34-a0cb-08db322230c9"), Name = "Pants", UrlSlug = "pants"}
        };

        // product
        Products = new List<Domain.Entities.Product>
        {
            new Domain.Entities.Product
            {
                Id = new Guid("3c5c0000-97c6-fc34-a0cb-08db322230d8"),
                Name = "T-Shirt Blu",
                UrlSlug = "t-shirt-blue",
                Description = "Lorem ipsum 1",
                CategoryId = Categories.First().Id,
                IsActive = true,
                IsDeleted = false
            },
            new Domain.Entities.Product
            {
                Id = new Guid("3c5c0000-97c6-fc34-a0cb-08db322230d9"),
                Name = "T-Shirt Coolman",
                UrlSlug = "t-shirt-coolman",
                Description = "Lorem ipsum 2",
                CategoryId = Categories.First().Id,
                IsActive = true,
                IsDeleted = false
            },
            new Domain.Entities.Product
            {
                Id = new Guid("3c5c0000-97c6-fc34-a0cb-08db32223010"),
                Name = "T-Shirt Seed-1",
                UrlSlug = "t-shirt-seed-1",
                Description = "Lorem ipsum 2",
                CategoryId = Categories.First().Id,
                IsActive = true,
                IsDeleted = false
            },
            new Domain.Entities.Product
            {
                Id = new Guid("3c5c0000-97c6-fc34-a0cb-08db32223011"),
                Name = "T-Shirt Seed-2",
                UrlSlug = "t-shirt-seed-2",
                Description = "Lorem ipsum 2",
                CategoryId = Categories.First().Id,
                IsActive = true,
                IsDeleted = false
            },
            new Domain.Entities.Product
            {
                Id = new Guid("3c5c0000-97c6-fc34-a0cb-08db32223012"),
                Name = "T-Shirt Seed-3",
                UrlSlug = "t-shirt-seed-3",
                Description = "Lorem ipsum 2",
                CategoryId = Categories.First().Id,
                IsActive = true,
                IsDeleted = false
            },
            new Domain.Entities.Product
            {
                Id = new Guid("3c5c0000-97c6-fc34-a0cb-08db32223013"),
                Name = "T-Shirt Seed-4",
                UrlSlug = "t-shirt-seed-4",
                Description = "Lorem ipsum 2",
                CategoryId = Categories.First().Id,
                IsActive = true,
                IsDeleted = false
            },
            new Domain.Entities.Product
            {
                Id = new Guid("3c5c0000-97c6-fc34-a0cb-08db32223014"),
                Name = "T-Shirt Seed-5",
                UrlSlug = "t-shirt-seed-5",
                Description = "Lorem ipsum 2",
                CategoryId = Categories.First().Id,
                IsActive = true,
                IsDeleted = false
            },
            new Domain.Entities.Product
            {
                Id = new Guid("3c5c0000-97c6-fc34-a0cb-08db32223015"),
                Name = "T-Shirt Seed-6",
                UrlSlug = "t-shirt-seed-6",
                Description = "Lorem ipsum 2",
                CategoryId = Categories.First().Id,
                IsActive = true,
                IsDeleted = false
            },
            new Domain.Entities.Product
            {
                Id = new Guid("3c5c0000-97c6-fc34-a0cb-08db32223016"),
                Name = "T-Shirt Seed-7",
                UrlSlug = "t-shirt-seed-7",
                Description = "Lorem ipsum 2",
                CategoryId = Categories.First().Id,
                IsActive = true,
                IsDeleted = false
            },
            new Domain.Entities.Product
            {
                Id = new Guid("3c5c0000-97c6-fc34-a0cb-08db32223017"),
                Name = "T-Shirt Seed-8",
                UrlSlug = "t-shirt-seed-8",
                Description = "Lorem ipsum 2",
                CategoryId = Categories.First().Id,
                IsActive = true,
                IsDeleted = false
            },
            new Domain.Entities.Product
            {
                Id = new Guid("3c5c0000-97c6-fc34-a0cb-08db32223018"),
                Name = "T-Shirt Seed-9",
                UrlSlug = "t-shirt-seed-9",
                Description = "Lorem ipsum 2",
                CategoryId = Categories.First().Id,
                IsActive = true,
                IsDeleted = false
            },
            new Domain.Entities.Product
            {
                Id = new Guid("3c5c0000-97c6-fc34-a0cb-08db32223019"),
                Name = "T-Shirt Seed-10",
                UrlSlug = "t-shirt-seed-10",
                Description = "Lorem ipsum 2",
                CategoryId = Categories.First().Id,
                IsActive = true,
                IsDeleted = false
            }
        };

        // *product option: product last() has option
        ProductOptions = new List<Domain.Entities.ProductOption>
        {
            new Domain.Entities.ProductOption
            {
               Id = new Guid("3c5c0000-97c6-fc34-a0cb-08db322230e1"),
               ProductId = Products.Last().Id,
               Name = "Color",
               AllowImage = true,
            },
            new Domain.Entities.ProductOption
            {
               Id = new Guid("3c5c0000-97c6-fc34-a0cb-08db322230e2"),
               ProductId = Products.Last().Id,
               Name = "Size",
               AllowImage = false,
            }
        };

        OptionValues = new List<Domain.Entities.OptionValue>
        {
            new Domain.Entities.OptionValue
            {
               Id = new Guid("3c5c0000-97c6-fc34-a0cb-08db322230f1"),
               OptionId = ProductOptions.First().Id,
               Value = "Black",
               Image = new Domain.Entities.Image
               {
                   Id = Guid.CreateVersion7(),
                   BaseUrl = "https://localhost:7129",
                   FileName = "/image/black1.jpg",
                   AllText = $"seo all text - {Products.Last().Id}"
               }
            },
            new Domain.Entities.OptionValue
            {
               Id = new Guid("3c5c0000-97c6-fc34-a0cb-08db322230f2"),
               OptionId = ProductOptions.First().Id,
               Value = "White",
               Image = new Domain.Entities.Image
               {
                   Id = Guid.CreateVersion7(),
                   BaseUrl = "https://localhost:7129",
                   FileName = "/image/white1.jpg",                
                   AllText = $"seo all text - {Products.Last().Id}"
               }
            },
            new Domain.Entities.OptionValue
            {
               Id = new Guid("3c5c0000-97c6-fc34-a0cb-08db322230f3"),
               OptionId = ProductOptions.Last().Id,
               Value = "S",
            },
            new Domain.Entities.OptionValue
            {
               Id = new Guid("3c5c0000-97c6-fc34-a0cb-08db322230f4"),
               OptionId = ProductOptions.Last().Id,
               Value = "M",
            },
        };

        ProductImages = new List<Domain.Entities.ProductImage>
        {
#if (!HasManyOptionValue)
            // mainImage
            new Domain.Entities.ProductImage
            {
               Id = Guid.CreateVersion7(),
               ProductId = Products.First().Id,
               Image = new Domain.Entities.Image
               {
                   BaseUrl = "https://localhost:7129",
                   FileName = "/image/brown2.jpg",
                   AllText = $"seo all text - {Products.First().Id}"
               },
               SortOrder = 0,
               IsMain = true,
            },
            // common Image
            new Domain.Entities.ProductImage
            {
               Id = Guid.CreateVersion7(),
               ProductId = Products.First().Id,
               Image = new Domain.Entities.Image
               {
                   BaseUrl = "https://localhost:7129",
                   FileName = "/image/brown1.jpg",
                   AllText = $"seo all text - {Products.First().Id}"
               },
               SortOrder = 1,
               IsMain = false,
            },
#endif
            // *main image: only one main image
            new Domain.Entities.ProductImage
            {
               Id = Guid.CreateVersion7(),
               ProductId = Products.Last().Id,
               Image = new Domain.Entities.Image
               {
                   BaseUrl = "https://localhost:7129",
                   FileName = "/image/black1.jpg",
                   AllText = $"seo all text - {Products.Last().Id}"
               },
               SortOrder = 0,
               IsMain = true,
            },
            // common image
            new Domain.Entities.ProductImage
            {
               Id = Guid.CreateVersion7(),
               ProductId = Products.Last().Id,
               Image = new Domain.Entities.Image
               {
                   BaseUrl = "https://localhost:7129",
                   FileName = "/image/white1.jpg",
                   AllText = $"seo all text - {Products.Last().Id}"
               },
               SortOrder = 1,
               IsMain = false,
            },
            new Domain.Entities.ProductImage
            {
               Id = Guid.CreateVersion7(),
               ProductId = Products.Last().Id,
               Image = new Domain.Entities.Image
               {
                   BaseUrl = "https://localhost:7129",
                   FileName = "/image/white2.jpg",
                   AllText = $"seo all text - {Products.Last().Id}"
               },
               SortOrder = 2,
               IsMain = false,
            },
            new Domain.Entities.ProductImage
            {
               Id = Guid.CreateVersion7(),
               ProductId = Products.Last().Id,
               Image = new Domain.Entities.Image
               {
                   BaseUrl = "https://localhost:7129",
                   FileName = "/image/black2.jpg",
                   AllText = $"seo all text - {Products.Last().Id}"
               },
               SortOrder = 1,
               IsMain = false,
            },
        };

        // *variant: variant with no option -> get variant -> image : null -> fallback mainImage
        Variants = new List<Domain.Entities.Variant>
        {
            new Domain.Entities.Variant
            {
                Id = Guid.CreateVersion7(),
                ProductId = Products.First().Id,
                Price = 90000,
                Quantity = 10,
                IsActive = true,
                IsDeleted = false,
            },
            // Black-S
            new Domain.Entities.Variant
            {
                Id = Guid.CreateVersion7(),
                ProductId = Products.Last().Id,
                Title = "Black-S",
                Price = 35000,
                Quantity = 10,
                VariantOptions = new List<Domain.Entities.VariantOption>
                {
                    new Domain.Entities.VariantOption
                    {
                        OptionValueId = new Guid("3c5c0000-97c6-fc34-a0cb-08db322230f1")
                    },
                    new Domain.Entities.VariantOption
                    {
                        OptionValueId = new Guid("3c5c0000-97c6-fc34-a0cb-08db322230f3")
                    }
                },
                IsActive = true,
                IsDeleted = false,
            },
            // Black-M
            new Domain.Entities.Variant
            {
                Id = Guid.CreateVersion7(),
                ProductId = Products.Last().Id,              
                Title = "Black-M",
                Price = 40000,
                Quantity = 10,
                VariantOptions = new List<Domain.Entities.VariantOption>
                {
                    new Domain.Entities.VariantOption
                    {
                        OptionValueId = new Guid("3c5c0000-97c6-fc34-a0cb-08db322230f1")
                    },
                    new Domain.Entities.VariantOption
                    {
                        OptionValueId = new Guid("3c5c0000-97c6-fc34-a0cb-08db322230f4")
                    }
                },
                IsActive = true,
                IsDeleted = false,
            },
            // White-S
            new Domain.Entities.Variant
            {
                Id = Guid.CreateVersion7(),
                ProductId = Products.Last().Id,
                Title = "White-S",
                Price = 35000,
                Quantity = 10,
                VariantOptions = new List<Domain.Entities.VariantOption>
                {
                    new Domain.Entities.VariantOption
                    {
                        OptionValueId = new Guid("3c5c0000-97c6-fc34-a0cb-08db322230f2")
                    },
                    new Domain.Entities.VariantOption
                    {
                        OptionValueId = new Guid("3c5c0000-97c6-fc34-a0cb-08db322230f3")
                    }
                },
                IsActive = true,
                IsDeleted = false,
            },
            // White-M
            new Domain.Entities.Variant
            {
                Id = Guid.CreateVersion7(),
                ProductId = Products.Last().Id,
                Title = "White-M",
                Price = 40000,
                Quantity = 10,
                VariantOptions = new List<Domain.Entities.VariantOption>
                {
                    new Domain.Entities.VariantOption
                    {
                        OptionValueId = new Guid("3c5c0000-97c6-fc34-a0cb-08db322230f2")
                    },
                    new Domain.Entities.VariantOption
                    {
                        OptionValueId = new Guid("3c5c0000-97c6-fc34-a0cb-08db322230f4")
                    }
                },
                IsActive = true,
                IsDeleted = false,
            },
            // Seed
            new Domain.Entities.Variant
            {
                Id = Guid.CreateVersion7(),
                ProductId = Products[2].Id,
                Price = PriceRnd,
                Quantity = QuantityRnd,
                IsActive = true,
                IsDeleted = false,
            },
            new Domain.Entities.Variant
            {
                Id = Guid.CreateVersion7(),
                ProductId = Products[3].Id,
                Price = PriceRnd,
                Quantity = QuantityRnd,
                IsActive = true,
                IsDeleted = false,
            },
            new Domain.Entities.Variant
            {
                Id = Guid.CreateVersion7(),
                ProductId = Products[4].Id,
                Price = PriceRnd,
                Quantity = QuantityRnd,
                IsActive = true,
                IsDeleted = false,
            },
            new Domain.Entities.Variant
            {
                Id = Guid.CreateVersion7(),
                ProductId = Products[5].Id,
                Price = PriceRnd,
                Quantity = QuantityRnd,
                IsActive = true,
                IsDeleted = false,
            },
            new Domain.Entities.Variant
            {
                Id = Guid.CreateVersion7(),
                ProductId = Products[6].Id,
                Price = PriceRnd,
                Quantity = QuantityRnd,
                IsActive = true,
                IsDeleted = false,
            },
            new Domain.Entities.Variant
            {
                Id = Guid.CreateVersion7(),
                ProductId = Products[7].Id,
                Price = PriceRnd,
                Quantity = QuantityRnd,
                IsActive = true,
                IsDeleted = false,
            },
            new Domain.Entities.Variant
            {
                Id = Guid.CreateVersion7(),
                ProductId = Products[8].Id,
                Price = PriceRnd,
                Quantity = QuantityRnd,
                IsActive = true,
                IsDeleted = false,
            },
            new Domain.Entities.Variant
            {
                Id = Guid.CreateVersion7(),
                ProductId = Products[9].Id,
                Price = PriceRnd,
                Quantity = QuantityRnd,
                IsActive = true,
                IsDeleted = false,
            },
            new Domain.Entities.Variant
            {
                Id = Guid.CreateVersion7(),
                ProductId = Products[10].Id,
                Price = PriceRnd,
                Quantity = QuantityRnd,
                IsActive = true,
                IsDeleted = false,
            },
        };
    }
}
