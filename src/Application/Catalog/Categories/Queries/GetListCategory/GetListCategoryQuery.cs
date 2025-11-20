using Application.Common.Interfaces;
using Application.Common.Specifications;
using Domain.Entities;
using MediatR;

namespace Application.Catalog.Categories.Queries.GetListCategory;

public record GetListCategoryQuery : IRequest<List<CategoryDto>>;

public class GetListCategoryQueryHandler : IRequestHandler<GetListCategoryQuery, List<CategoryDto>>
{
    private readonly IRepository<Category> _categoryRepository;
    public GetListCategoryQueryHandler(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<List<CategoryDto>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _categoryRepository.ListAsync(new CategoryListSpec());
    }
}
