using Application.Basket.Dtos;
using Application.Catalog.Variants.Queries.GetVariantById;
using Application.Common.Interfaces;
using Application.Common.Specifications;
using MediatR;

namespace Application.Basket.Queries.GetBasketByCustomerId;

public record GetBasketByCustomerIdQuery(Guid CustomerId) : IRequest<BasketDto>;

public class GetBasketByCustomerIdQueryHandler : IRequestHandler<GetBasketByCustomerIdQuery, BasketDto>
{
    private readonly IMediator _mediator;
    private readonly IRepository<Domain.Entities.Basket> _basketRepository;
    public GetBasketByCustomerIdQueryHandler(IMediator mediator, IRepository<Domain.Entities.Basket> basketRepository)
    {
        _mediator = mediator;
        _basketRepository = basketRepository;
    }
    public async Task<BasketDto> Handle(GetBasketByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        // Get basket
        var specification = new BasketWithItemsByCustomerIdSpec(request.CustomerId);
        var basket = await _basketRepository.FirstOrDefaultAsync(specification);

        // If basket doesn't exist, return an empty basket
        if (basket == null)
        {
            return new BasketDto(basket.Id, request.CustomerId, new List<BasketItemDto>(), DateTime.UtcNow, null);
        }

        BasketDto vm = new BasketDto(basket.Id, request.CustomerId, new List<BasketItemDto>(), (DateTime)basket.CreatedAt, basket.LastModified);

        foreach (var item in basket.Items)
        {
            var productVariant = await _mediator.Send(new GetVariantByIdQuery(item.ProductVariantId));
            var cartItem = basket.Items.FirstOrDefault(x => x.ProductVariantId == productVariant.Id);

            vm.Items.Add(new BasketItemDto(cartItem.ProductVariantId, productVariant.ProductName , productVariant.Title, productVariant.RegularPrice, productVariant.Image.Url ?? "", cartItem.Quantity));
        }

        return vm;
    }
}