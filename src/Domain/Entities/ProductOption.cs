namespace Domain.Entities;

public class ProductOption
{
    public int Id { get; set; } 
    public int ProductId { get; set; }
    public string OptionName { get; set; }
    public bool AllowImage { get; set; } = false;
    public Product Product { get; set; }
    public ICollection<OptionValue> OptionValues { get; set; } = new List<OptionValue>();
}
