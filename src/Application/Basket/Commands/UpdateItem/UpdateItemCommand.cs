using Application.Catalog.Variants.Queries.GetVariantById;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Specifications;
using Application.Customer.Queries.GetCustomerByUserId;
using Domain.Entities;
using MediatR;
using Shared.Web;

namespace Application.Basket.Commands.UpdateItem;
public record UpdateItemCommand(Guid Id, int Quantity) : IRequest<Guid>;

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Guid>
{
    private readonly IRepository<Domain.Entities.Basket> _basketRepository;
    private readonly IMediator _mediator;
    private readonly ICurrentUserProdvider _currentUserProdvider;
    public UpdateItemCommandHandler(
        IMediator mediator,
        IRepository<Domain.Entities.Basket> basketRepository,
        ICurrentUserProdvider currentUserProdvider)
    {
        _mediator = mediator;
        _basketRepository = basketRepository;
        _currentUserProdvider = currentUserProdvider;
    }
    public async Task<Guid> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserProdvider.GetCurrentUserId();

        if(userId == null)
            throw new ArgumentException("User is required");
        if (request.Quantity < 0)
            throw new ArgumentException("Quantity cannot be negative.");

        // Directly call the GetCustomerByUserIdQuery handler instead of gRPC
        var customer = await _mediator.Send(new GetCustomerByUserIdQuery(userId!.Value))
                ?? throw new EntityNotFoundException("Customer not found");

        // Validate ProductVariant exists
        var variant = await _mediator.Send(new GetVariantByIdQuery(request.Id))
                ?? throw new EntityNotFoundException("Product variant not found");

        // ProductVariant quantity not enough
        if(request.Quantity > variant.Quantity)
        {
            throw new EntityNotFoundException("Not enough product variant quanity");
        }

        // Get or create basket
        var basket = await _basketRepository.FirstOrDefaultAsync(new BasketWithItemsByCustomerIdSpec(customer.Id));
        if (basket == null)
        {
            basket = new Domain.Entities.Basket()
            {
                CustomerId = customer.Id,
                Items = new List<BasketItem>(),
                CreatedAt = DateTime.UtcNow,
            };
            await _basketRepository.AddAsync(basket);
        }
        // Update basket items
        var existingItem = basket.Items.FirstOrDefault(x => x.ProductVariantId == request.Id);
      
        if (existingItem == null)
        {
            if (request.Quantity > 0)
            {
                basket.Items.Add(new BasketItem()
                {
                    ProductVariantId = request.Id,
                    Quantity = request.Quantity,
                });
            }       
        }
        else
        {
            if (request.Quantity > 0)
            {
                existingItem.Quantity = request.Quantity;
            }
            else
            {
                basket.Items.Remove(existingItem);
            }
        }

        basket.LastModified = DateTime.UtcNow;

        await _basketRepository.SaveChangesAsync(cancellationToken);

        return basket.Id;
    }
}