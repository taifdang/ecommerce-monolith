using Application.Common.Interfaces;
using Application.Common.Specifications;
using Ardalis.GuardClauses;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Catalog.Variants.Queries.GetVariantById;

public record GetVariantByIdQuery(Guid Id) : IRequest<VariantDto>;

public class GetVariantByIdQueryHandler : IRequestHandler<GetVariantByIdQuery, VariantDto>
{
    private readonly IRepository<ProductVariant> _productVariantRepo;
    private readonly IRepository<ProductImage> _productImageRepo;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GetVariantByIdQueryHandler(
        IRepository<ProductVariant> productVariantRepository,
        IApplicationDbContext context,
        IMapper mapper,
        IMediator mediator,
        IRepository<ProductImage> productImageRepo)
    {
        _productVariantRepo = productVariantRepository;
        _context = context;
        _mapper = mapper;
        _mediator = mediator;
        _productImageRepo = productImageRepo;
    }
    public async Task<VariantDto> Handle(GetVariantByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new ProductVariantByIdSpec(request.Id);
        var variant = await _productVariantRepo.FirstOrDefaultAsync(spec, cancellationToken);
       
        if (variant is null)
            Guard.Against.NotFound(nameof(variant), variant);

        var optionValueIdSeleted = variant.Options
            .Where(x => x.IsImage)
            .Select(x => x.OptionValueId).FirstOrDefault();

        var imageOrderSpec = 
            new ProductImagesOrderedSpec(variant.ProductId, optionValueIdSeleted).WithTakeOne();
        var image = await _productImageRepo.FirstOrDefaultAsync(imageOrderSpec, cancellationToken);

        variant.Image = image;  

        return _mapper.Map<VariantDto>(variant);
    }
}
// linq order with priority 0 -> 1 -> 2
//var image = await _context.ProductImages
//    .Where(x => x.ProductId == variant.ProductId)
//    .OrderByDescending(x => x.OptionValueId == optionValueSeleted)
//    .ThenByDescending(x => x.IsMain && x.OptionValueId == null)
//    .ThenByDescending(x => x.OptionValueId == null)
//    .ThenBy(x => x.Id)
//    .Select(x => new ImageLookupDto { Id = x.Id, Url = x.ImageUrl })
//    .FirstOrDefaultAsync();