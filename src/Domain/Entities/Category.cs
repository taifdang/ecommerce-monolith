namespace Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public int ProductTypeId { get; set; }
    public string Title { get; set; }
    public string? Label { get; set; }
    public ProductType ProductType { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
