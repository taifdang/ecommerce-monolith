namespace Application.Catalog.Products.Dtos;

public class ProductOptionDto
{
    public string Title { get; set; }
    //public List<ProductOptionValueDto> Options { get; set; } = new List<ProductOptionValueDto>();
    public List<string> Values { get; set; }
}
