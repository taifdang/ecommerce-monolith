using Application.Basket.Dtos;
using Application.Catalog.Variants.Queries.GetVariantById;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Specifications;
using Application.Customer.Queries.GetCustomerByUserId;
using MediatR;

namespace Application.Basket.Queries.GetCartList;

public record GetBasketQuery(Guid UserId) : IRequest<BasketDto>;

public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, BasketDto>
{
    private readonly IRepository<Domain.Entities.Basket> _basketRepository;
    private readonly IMediator _mediator;
    public GetBasketQueryHandler(IRepository<Domain.Entities.Basket> baskerRepository, IMediator mediator)
    {
        _basketRepository = baskerRepository;
        _mediator = mediator;
    }
    public async Task<BasketDto> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {      
        // Directly call the GetCustomerByUserIdQuery handler instead of gRPC
        var customer = await _mediator.Send(new GetCustomerByUserIdQuery(request.UserId),cancellationToken)
                ?? throw new EntityNotFoundException("Customer not found");

        // Get basket
        var basket = await _basketRepository.FirstOrDefaultAsync(new BasketWithItemsByCustomerIdSpec(customer.Id), cancellationToken);

        // If basket doesn't exist, return an empty basket
        if (basket == null)
        {
            return new BasketDto(Guid.NewGuid(), customer.Id, new List<BasketItemDto>(), DateTime.UtcNow, null);
        }

        BasketDto basketDto = new BasketDto(basket.Id, customer.Id, new List<BasketItemDto>(), (DateTime)basket.CreatedAt, basket.LastModified);

        foreach (var item in basket.Items)
        {
            var productVariant = await _mediator.Send(new GetVariantByIdQuery(item.ProductVariantId));
            var cartItem = basket.Items.First(x => x.ProductVariantId == productVariant.Id);
            basketDto.Items.Add(new BasketItemDto(cartItem.ProductVariantId, productVariant.ProductName ,productVariant.Title, productVariant.RegularPrice, productVariant.Image.Url ?? "", cartItem.Quantity));
        }

        return basketDto;
    }
}
