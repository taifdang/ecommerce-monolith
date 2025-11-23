using Application.Catalog.Products.Queries.GetProductById;
using Application.Common.Models;
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ProductDetailSpec : Specification<Product, ProductItemDto>
{
    public ProductDetailSpec(Guid productId)
    {
        Query.Where(x => x.Id == productId).AsNoTracking();

        Query.Select(p => new ProductItemDto
        {
            Id = p.Id,
            Title = p.Title,
            MinPrice = p.ProductVariants.Min(pv => pv.Price),
            MaxPrice = p.ProductVariants.Max(pv => pv.Price),
            Description = p.Description ?? "",
            Category = p.Category.Title,
            MainImage = null,
            Images = null,
            Options = p.ProductOptions.Select(po => new ProductOptionDto
            {
                Title = po.OptionName,
                OptionValues = po.OptionValues.Select(ov => new ProductOptionValueDto
                {
                    Id = ov.Id,
                    Value = ov.Value,
                    Image = null
                }).ToList()
            }).ToList()
        });

        //Query.Select(p => new ProductItemDto
        //{
        //    Id = p.Id,
        //    Title = p.Title,
        //    MinPrice = p.ProductVariants.Min(pv => pv.Price),
        //    MaxPrice = p.ProductVariants.Max(pv => pv.Price),
        //    Description = p.Description ?? "",
        //    Category = p.Category.Title,
        //    Images = null,
        //    Options = p.ProductOptions.Select(po => new ProductOptionDto
        //    {
        //        Title = po.OptionName,
        //        OptionValues = po.OptionValues.Select(ov => new ProductOptionValueDto
        //        {
        //            Value = ov.Value,
        //            Image = null
        //        }).ToList()
        //    }).ToList()
        //});


        //Query.Where(x => x.Id == productId).AsNoTracking().AsSplitQuery();

        //Query.Select(x => new ProductItemDto
        //{
        //    Id = x.Id,
        //    Title = x.Title,
        //    MinPrice = x.ProductVariants.Min(x => x.Price),
        //    MaxPrice = x.ProductVariants.Max(x => x.Price),
        //    Description = x.Description ?? "",
        //    Category = x.Category.Title,
        //    Images = x.ProductImages.Select(img => new ImageLookupDto
        //    {
        //        Id = img.Id,
        //        Url = img.ImageUrl,
        //    }).ToList(),

        //    Options = x.ProductOptions.Select(po => new ProductOptionDto
        //    {
        //        Title = po.OptionName,
        //        OptionValues = po.OptionValues.Select(x => new ProductOptionValueDto
        //        {
        //            Value = x.Value,
        //            Image = x.ProductImages.Select(img => new ImageLookupDto
        //            {
        //                Id = img.Id,
        //                Url = img.ImageUrl,
        //            }).FirstOrDefault()
        //        }).ToList(),
        //    }).ToList(),
        //});
    }
}
//Query
//            .Select(x => new ProductItemDto
//            {
//                Id = x.Id,
//                Title = x.Title,
//                MinPrice = x.ProductVariants.Min(x => x.Price),
//                MaxPrice = x.ProductVariants.Max(x => x.Price),
//                Description = x.Description ?? string.Empty,
//                Category = x.Category.Title,
//                Images = x.ProductImages.Select(img => new ImageLookupDto
//                {
//                    Id = img.Id,
//                    Url = img.ImageUrl,
//                }).ToList(),
//                Options = x.ProductOptions.Select(po => new ProductOptionDto
//                {
//                    Title = po.OptionName,
//                    OptionValues = po.OptionValues.Select(v => v.Value).ToList()
//                }).ToList(),
//                OptionValues = x.ProductOptions.Select(po => new ProductOptionValueDto
//                {
//                    Title = po.OptionName,
//                    OptionValues = po.OptionValues.Select(v => v.Value).ToList(),
//                    Variants = po.OptionValues.Select(ov => new ProductOptionVariantDto
//                    {
//                        Title = ov.Value,
//                        Label = ov.Label,
//                        Images = ov.ProductImages!.Select(pi => new ImageLookupDto
//                        {
//                            Id = pi.Id,
//                            Url = pi.ImageUrl
//                        }).ToList()
//                    }).ToList()
//                }).ToList(),
//            });

//Query
//    .Where(x => x.Id == productId)
//    .Include(x => x.Category)
//    .Include(x => x.ProductVariants)
//    .Include(x => x.ProductImages)
//    .Include(x => x.ProductOptions)
//        .ThenInclude(x => x.OptionValues)
//            .ThenInclude(ov => ov.ProductImages)
//    .AsSplitQuery()
//    .AsNoTracking();

//Query
//    .Where(x => x.Id == productId)
//    .Include(x => x.ProductOptions)
//        .ThenInclude(x => x.OptionValues);


//Query
//           .Where(x => x.Id == productId)
//            .Include(x => x.Category)
//            .Include(x => x.ProductOptions)
//                .ThenInclude(x => x.OptionValues)
//            .Include(p => p.ProductVariants)
//            .AsNoTracking();