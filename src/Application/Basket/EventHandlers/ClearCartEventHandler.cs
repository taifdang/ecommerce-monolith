using Application.Basket.Commands.ClearBasket;
using Application.Common.Interfaces;
using Application.Common.Specifications;
using MediatR;

namespace Application.Basket.EventHandlers;

public class ClearCartEventHandler
{
    private readonly IRepository<Domain.Entities.Basket> _basketRepository;
    public ClearCartEventHandler(IRepository<Domain.Entities.Basket> basketRepository)
    {
        _basketRepository = basketRepository;
    }
    public async Task<Unit> Handle(ClearBasketCommand request, CancellationToken cancellationToken)
    {
        var basket = await _basketRepository.FirstOrDefaultAsync(new BasketWithItemsByCustomerIdSpec(request.CustomerId));
        if (basket != null)
        {
            await _basketRepository.DeleteAsync(basket);
        }
        return Unit.Value;
    }
}
