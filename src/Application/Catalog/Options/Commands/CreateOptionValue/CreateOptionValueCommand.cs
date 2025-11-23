using Application.Common.Interfaces;
using Application.Common.Specifications;
using Ardalis.GuardClauses;
using Domain.Entities;
using MediatR;

namespace Application.Catalog.Options.Commands.CreateOptionValue;

public record CreateOptionValueCommand(Guid ProductOptionId, string Value, string? Label) : IRequest<Unit>;

public class CreateOptionValueCommandHandler : IRequestHandler<CreateOptionValueCommand, Unit>
{
    private readonly IRepository<OptionValue> _optionValueRepository;

    public CreateOptionValueCommandHandler(IRepository<OptionValue> optionValueRepository)
    {
        _optionValueRepository = optionValueRepository;
    }

    public async Task<Unit> Handle(CreateOptionValueCommand request, CancellationToken cancellationToken)
    { 
        var optionExist = await _optionValueRepository.AnyAsync(new OptionValueFilterSpec(null, request.ProductOptionId));
        Guard.Against.NotFound(request.ProductOptionId, optionExist);

        var optionValue = new OptionValue
        {
            ProductOptionId = request.ProductOptionId,
            Value = request.Value,
            Label = request.Label,         
        };

        await _optionValueRepository.AddAsync(optionValue, cancellationToken);

        return Unit.Value;
    }
}
