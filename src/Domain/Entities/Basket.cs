using Domain.Common;

namespace Domain.Entities;

public class Basket : Entity<int>
{
    public int CustomerId { get; set; }
    public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();
}
