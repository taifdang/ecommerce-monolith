namespace Application.Catalog.Variants.Dtos;

public record VariantListDto(
    IList<VariantDto> Variants,
    VariantImageDto? Image,
    decimal? MinPrice, 
    decimal? MaxPrice, 
    int TotalStock);
//{
//    public IList<VariantDto> Variants { get; set; }
//    public VariantImageDto? Image { get; set; }
//    public decimal? MinPrice { get; set; }
//    public decimal? MaxPrice { get; set; }
//    public int TotalStock { get; set; }
//}
