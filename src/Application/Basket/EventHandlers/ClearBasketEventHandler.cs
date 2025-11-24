using Application.Common.Interfaces;
using Application.Common.Specifications;
using Domain.Events;
using MediatR;

namespace Application.Basket.EventHandlers;

public class ClearBasketEventHandler : INotificationHandler<BasketShouldBeClearedEvent>
{
    private readonly IRepository<Domain.Entities.Basket> _basketRepo;

    public ClearBasketEventHandler(IRepository<Domain.Entities.Basket> basketRepo)
    {
        _basketRepo = basketRepo;
    }

    public async Task Handle(BasketShouldBeClearedEvent e, CancellationToken cancellationToken)
    {
        var spec = new BasketWithItemsByCustomerIdSpec(e.CustomerId);
        var basket = await _basketRepo.FirstOrDefaultAsync(spec);

        if (basket != null)
        {
            basket.ClearItems();
            await _basketRepo.SaveChangesAsync(cancellationToken);
            //await _basketRepo.DeleteAsync(basket);
        }
    }
}
