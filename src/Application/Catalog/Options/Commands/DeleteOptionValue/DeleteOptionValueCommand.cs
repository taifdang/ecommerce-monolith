using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Specifications;
using Domain.Entities;
using MediatR;

namespace Application.Catalog.Options.Commands.DeleteOptionValue;

public record DeleteOptionValueCommand(Guid OptionValueId, Guid ProductOptionId) : IRequest<Unit>;

public class DeleteOptionValueCommandHandler : IRequestHandler<DeleteOptionValueCommand, Unit>
{
    private readonly IRepository<OptionValue> _optionValueRepository;
    public DeleteOptionValueCommandHandler(IRepository<OptionValue> optionValueRepository)
    {
        _optionValueRepository = optionValueRepository;
    }
    public async Task<Unit> Handle(DeleteOptionValueCommand request, CancellationToken cancellationToken)
    {
        var optionValue = await _optionValueRepository.FirstOrDefaultAsync(new OptionValueFilterSpec(request.OptionValueId, request.ProductOptionId))
            ?? throw new EntityNotFoundException(nameof(OptionValue), request.OptionValueId);

        await _optionValueRepository.DeleteAsync(optionValue, cancellationToken);
        return Unit.Value;
    }
}
