using Application.Common.Models;
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ProductImagesOrderedSpec : Specification<ProductImage, ImageWithOptionValueLookupDto>
{
    public ProductImagesOrderedSpec(Guid ProductId, Guid? OptionValueId)
    {
        Query.Where(x => x.ProductId == ProductId);

        Query
            .OrderBy(x =>
                  x.OptionValueId == OptionValueId ? 0 :
                  x.IsMain && x.OptionValueId == null ? 1 :
                  x.OptionValueId == null ? 2 : 3)
            .ThenBy(x => x.Id);

        Query.Select(x => new ImageWithOptionValueLookupDto
        {
            LookupDto = new ImageLookupDto
            {
                Id = x.Id,
                Url = x.ImageUrl
            },
            OptionValueId = x.OptionValueId
        });
    }

    public ProductImagesOrderedSpec ApplyOrdering(Guid? OptionValueId)
    {
        Query
             .OrderBy(x =>
                  x.OptionValueId == OptionValueId ? 0 :
                  x.IsMain && x.OptionValueId == null ? 1 :
                  x.OptionValueId == null ? 2 : 3)
            .ThenBy(x => x.Id);
        return this;
    }

    public ProductImagesOrderedSpec ApplyProjection()
    {
        Query.Select(x => new ImageWithOptionValueLookupDto
        {
            LookupDto = new ImageLookupDto
            {
                Id = x.Id,
                Url = x.ImageUrl
            },
            OptionValueId = x.OptionValueId
        });

        return this;
    }

    public ProductImagesOrderedSpec WithTakeOne()
    {
        Query.Take(1);
        return this;
    }
}

//Query
//    .OrderByDescending(x => x.OptionValueId == OptionValueId) // image with optionvalue
//    .ThenByDescending(x => x.IsMain && x.OptionValueId == null) // main image
//    .ThenByDescending(x => x.OptionValueId == null) // common image