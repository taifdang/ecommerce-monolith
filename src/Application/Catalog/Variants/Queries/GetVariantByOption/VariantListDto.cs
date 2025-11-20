using Application.Catalog.Variants.Queries.GetVariantById;
using Application.Common.Models;

namespace Application.Catalog.Variants.Queries.GetVariantByOption;

public class VariantListDto
{
    public IList<VariantDto> Variants { get; set; }
    public ImageLookupDto? Image { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int TotalStock { get; set; }
}
