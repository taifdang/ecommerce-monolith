using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Catalog.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Title, string? Label) : IRequest<Guid>;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly IRepository<Category> _categoryRepository;
    public CreateCategoryCommandHandler(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Title = request.Title,
            Label = request.Label,
        };

        await _categoryRepository.AddAsync(category, cancellationToken);

        return category.Id;
    }
}
