using Application.Common.Interfaces;
using Application.Common.Specifications;
using Domain.Entities;
using MediatR;

namespace Application.Catalog.Options.Commands.CreateOption;

public record CreateOptionCommand(Guid ProductId, string OptionName, bool AllowImage = false) : IRequest<Unit>;

public class CreateOptionCommandHandler : IRequestHandler<CreateOptionCommand, Unit>
{
    private readonly IRepository<ProductOption> _productOptionRepository;

    public CreateOptionCommandHandler(IRepository<ProductOption> productOptionrepository)
    {
        _productOptionRepository = productOptionrepository;
    }

    public async Task<Unit> Handle(CreateOptionCommand request, CancellationToken cancellationToken)
    {
        var existing = await _productOptionRepository.AnyAsync(new ProductOptionsWithImageAllowedSpec(request.ProductId));

        if (existing && request.AllowImage)
        {
            throw new Exception("Only one product option can allow images per product.");
        }

        var productOption = new ProductOption
        {
            ProductId = request.ProductId,
            OptionName = request.OptionName,
            AllowImage = request.AllowImage,
        };

        await _productOptionRepository.AddAsync(productOption, cancellationToken);

        return Unit.Value;
    }
}