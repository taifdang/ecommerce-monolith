using Application.Basket.Specifications;
using Application.Common.Interfaces;
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
        var spec = new BasketSpec()
            .ByCustomerId(e.CustomerId)
            .WithItems();

        var basket = await _basketRepo.FirstOrDefaultAsync(spec);

        if (basket != null)
        {
            basket.ClearItems();
            await _basketRepo.SaveChangesAsync(cancellationToken);
        }
    }
}
