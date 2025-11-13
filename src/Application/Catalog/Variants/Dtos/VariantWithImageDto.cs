namespace Application.Catalog.Variants.Dtos;

public class VariantWithImageDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public VariantImageDto Image { get; set; }
}
