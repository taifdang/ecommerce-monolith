using FluentValidation;

namespace Application.Catalog.Options.Commands.CreateOption;

public class CreateOptionCommandValidator : AbstractValidator<CreateOptionCommand>
{
    public CreateOptionCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.OptionName).NotEmpty().WithMessage("Option name is required");
    }
}
