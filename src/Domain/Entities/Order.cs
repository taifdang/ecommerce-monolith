using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Order : Entity<Guid>
{
    public Guid CustomerId { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    //public string Phone { get; set; }
    public string ShippingAddress { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
