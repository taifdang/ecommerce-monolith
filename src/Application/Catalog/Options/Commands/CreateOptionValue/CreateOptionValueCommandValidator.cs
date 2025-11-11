using FluentValidation;

namespace Application.Catalog.Options.Commands.CreateOptionValue;

public class CreateOptionValueCommandValidator : AbstractValidator<CreateOptionValueCommand>
{
    public CreateOptionValueCommandValidator()
    {
        RuleFor(x => x.ProductOptionId).NotEmpty();
        RuleFor(x => x.Value).NotEmpty().WithMessage("Option value is required");
    }
}
