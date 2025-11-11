namespace Application.Catalog.Variants.Dtos;

public class VariantListDto
{
    public IList<VariantDto> Variants { get; set; }
    public VariantImageDto? Image { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int TotalStock { get; set; }
}
