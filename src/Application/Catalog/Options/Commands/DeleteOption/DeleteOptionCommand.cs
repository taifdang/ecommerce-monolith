using MediatR;
using Application.Common.Exceptions;
using Domain.Entities;
using Application.Common.Interfaces;
using Application.Common.Specifications;

namespace Application.Catalog.Options.Commands.DeleteOption;

public record DeleteOptionCommand : IRequest<Unit>
{
    public int ProductOptionId { get; init; }// option id
    public int ProductId { get; set; }
}

public class DeleteOptionCommandHandler : IRequestHandler<DeleteOptionCommand, Unit>
{
    private readonly IRepository<ProductOption> _productOptionRepository;
    public DeleteOptionCommandHandler(IRepository<ProductOption> productOptionRepository)
    {
        _productOptionRepository = productOptionRepository;
    }

    public async Task<Unit> Handle(DeleteOptionCommand request, CancellationToken cancellationToken)
    {
        var productOption = await _productOptionRepository.FirstOrDefaultAsync(new ProductOptionFilterSpec(request.ProductId, request.ProductOptionId))
            ?? throw new EntityNotFoundException(nameof(ProductOption), request.ProductOptionId);

        await _productOptionRepository.DeleteAsync(productOption, cancellationToken);
        return Unit.Value;
    }
}


