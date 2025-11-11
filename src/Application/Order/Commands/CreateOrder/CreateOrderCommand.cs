using Application.Basket.Queries.GetBasketByCustomerId;
using Application.Catalog.Variants.Queries.GetVariantById;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Order.Commands.CreateOrder;

public record CreateOrderCommand : IRequest<int>
{
    public int CustomerId { get; init; }
    public string ShippingAddress { get; init; }
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IRepository<Domain.Entities.Order> _orderRepository;
    private readonly IMediator _mediator;
    public CreateOrderCommandHandler(IRepository<Domain.Entities.Order> orderRepository, IMediator mediator)
    {
        _orderRepository = orderRepository;
        _mediator = mediator;
    }
    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var basket = await _mediator.Send(new GetBasketByCustomerIdQuery(request.CustomerId));

        if(basket?.Items == null || !basket.Items.Any())
        {
            throw new Exception("Basket is empty. Cannot create order.");
        }

        var orderItems = new List<OrderItem>();
        decimal totalAmount = 0;

        foreach (var basketItem in basket.Items)
        {
            var productVariant = await _mediator.Send(new GetVariantByIdQuery { Id = basketItem.ProductVariantId });

            if (productVariant == null)
            {
                throw new EntityNotFoundException();
            }

            var orderItem = new OrderItem
            {
                ProductVariantId = productVariant.Id,     
                ProductName = productVariant.ProductName,
                VariantName = productVariant.Title,
                UnitPrice = productVariant.RegularPrice,
                Quantity = basketItem.Quantity,
                ImageUrl = productVariant.Image.Url
            };

            orderItems.Add(orderItem);
            totalAmount += orderItem.TotalPrice;
        }

        var order = new Domain.Entities.Order
        {
            CustomerId = request.CustomerId,
            Status = OrderStatus.Pending,
            TotalAmount = totalAmount,
            ShippingAddress = request.ShippingAddress,
            Items = orderItems,
            OrderDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        await _orderRepository.AddAsync(order, cancellationToken);

        // Clear the basket after creating the order
        await _mediator.Send(new Basket.Commands.ClearBasket.ClearBasketCommand(request.CustomerId));

        return order.Id;
    }
}
