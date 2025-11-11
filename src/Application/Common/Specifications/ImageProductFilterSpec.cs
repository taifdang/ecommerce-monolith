using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ImageProductFilterSpec : Specification<ProductImage>
{
    public ImageProductFilterSpec(int productId, int Id)
    {
        Query
            .Where(x => x.ProductId == productId && x.Id == Id);
    }
}
