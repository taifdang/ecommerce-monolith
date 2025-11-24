using Application.Common.Interfaces;
using Application.Common.Specifications;
using MediatR;

namespace Application.Basket.Commands.ClearBasket;

public record ClearBasketCommand(Guid CustomerId) : IRequest<Unit>;

public class ClearCartCommandHandler : IRequestHandler<ClearBasketCommand, Unit>
{
    private readonly IRepository<Domain.Entities.Basket> _basketRepo;

    public ClearCartCommandHandler(IRepository<Domain.Entities.Basket> basketRepo)
    {
        _basketRepo = basketRepo;
    }

    public async Task<Unit> Handle(ClearBasketCommand request, CancellationToken cancellationToken)
    {
        var spec = new BasketWithItemsByCustomerIdSpec(request.CustomerId);
        var basket = await _basketRepo.FirstOrDefaultAsync(spec);

        if (basket != null)
        {
            basket.ClearItems();
            await _basketRepo.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
        return Unit.Value;
    }
}

