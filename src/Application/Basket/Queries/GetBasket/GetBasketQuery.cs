using Application.Basket.Queries.GetBasket;
using Application.Catalog.Variants.Queries.GetVariantById;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Specifications;
using Application.Customer.Queries.GetCustomerByUserId;
using MediatR;
using Shared.Web;

namespace Application.Basket.Queries.GetCartList;

public record GetBasketQuery(Guid? CustomerId) : IRequest<BasketDto>;

public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, BasketDto>
{
    private readonly IRepository<Domain.Entities.Basket> _basketRepository;
    private readonly IMediator _mediator;
    private readonly ICurrentUserProdvider _currentUserProdvider;
    public GetBasketQueryHandler(
        IRepository<Domain.Entities.Basket> baskerRepository, 
        IMediator mediator, 
        ICurrentUserProdvider currentUserProdvider)
    {
        _basketRepository = baskerRepository;
        _mediator = mediator;
        _currentUserProdvider = currentUserProdvider;
    }
    public async Task<BasketDto> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        Guid? customerId;

        if (!request.CustomerId.HasValue)
        {
            var userId = _currentUserProdvider.GetCurrentUserId();
            if (userId.HasValue) 
                throw new EntityNotFoundException("User not found");

            // Directly call the GetCustomerByUserIdQuery handler instead of gRPC
            customerId = await _mediator.Send(new GetCustomerByUserIdQuery(userId.Value), cancellationToken);  
        }
        else
        {
            customerId = request.CustomerId.Value;
        }

        if (customerId is null) 
            throw new EntityNotFoundException("User not found");
     
        // Get basket
        var basket = await _basketRepository.FirstOrDefaultAsync(new BasketWithItemsByCustomerIdSpec(customerId.Value), cancellationToken);

        // If basket doesn't exist, return an empty basket
        if (basket == null)
        {
            return new BasketDto(Guid.NewGuid(), customerId.Value, new List<BasketItemDto>(), DateTime.UtcNow, null);
        }

        BasketDto basketDto = new BasketDto(basket.Id, customerId.Value, new List<BasketItemDto>(), (DateTime)basket.CreatedAt, basket.LastModified);

        foreach (var item in basket.Items)
        {
            var productVariant = await _mediator.Send(new GetVariantByIdQuery(item.ProductVariantId));
            var cartItem = basket.Items.First(x => x.ProductVariantId == productVariant.Id);
            basketDto.Items.Add(new BasketItemDto(cartItem.ProductVariantId, productVariant.ProductName ,productVariant.Title, productVariant.RegularPrice, productVariant.Image.Url ?? "", cartItem.Quantity));
        }

        return basketDto;
    }
}
