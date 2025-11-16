using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Catalog.Categories.Commands.CreateCategory;

public record CreateCategoryCommand : IRequest<int>
{
    public string Title { get; set; }
    public string? Label { get; set; }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly IRepository<Category> _categoryRepository;
    public CreateCategoryCommandHandler(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
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
