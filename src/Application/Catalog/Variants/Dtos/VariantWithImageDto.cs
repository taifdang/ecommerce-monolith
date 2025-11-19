namespace Application.Catalog.Variants.Dtos;

public record VariantWithImageDto(
    Guid Id, 
    Guid ProductId,
    VariantImageDto? Image);
//{
//    public Guid Id { get; set; }
//    public Guid ProductId { get; set; }
//    public VariantImageDto Image { get; set; }
//}
