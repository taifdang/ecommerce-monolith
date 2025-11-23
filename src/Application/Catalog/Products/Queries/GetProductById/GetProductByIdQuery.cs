using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Specifications;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Catalog.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductItemDto>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductItemDto>
{
    private readonly IRepository<Domain.Entities.Product> _productRepository;
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public GetProductByIdQueryHandler(
        IRepository<Domain.Entities.Product> productRepository,
        IMapper mapper,
        IApplicationDbContext context)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _context = context;
    }

    public async Task<ProductItemDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        // *When user click product, we will load all options, option values, images for product to client side
        // *Then user select option values, we will generate variant on client side (based on option values selected) to show Price, sku, quantity, image...
        var products = await _productRepository.FirstOrDefaultAsync(new ProductDetailSpec(request.Id), cancellationToken);
        if (products == null)
        {
            return null!;
        }

        var rawImages = await _context.ProductImages
            .Where(x => x.ProductId == request.Id)
            .AsNoTracking()
            .Select(x => new ProductImageLookupDto
            {
                Id = x.Id,
                Url = x.ImageUrl,
                OptionValueId = x.OptionValueId,
                IsMain = x.IsMain,
            })
            .ToListAsync();

        var imageLookup = rawImages.Select(x => new ImageLookupDto
        {
            Id = x.Id,
            Url = x.Url
        });

        products.MainImage = rawImages
            .Where(x => x.OptionValueId == null &&  x.IsMain)
            .Select(x => new ImageLookupDto
            {
                Id = x.Id,
                Url = x.Url
            }).FirstOrDefault();

        products.Images = rawImages
            .Where(x => x.OptionValueId == null && !x.IsMain)
            .Select(x => new ImageLookupDto
            {
                Id = x.Id,
                Url = x.Url,
            })
            .ToList();

        var optionValueImages = rawImages
                .Where(i => i.OptionValueId != null)
                .ToDictionary(
                    i => i.OptionValueId!.Value,
                    i => imageLookup.First(x => x.Id == i.Id)
                );

        var options = products.Options.Select(po => new ProductOptionDto
        {
            OptionValues = po.OptionValues.Select(x => new ProductOptionValueDto
            {
                Image = optionValueImages.TryGetValue(x.Id, out var img) 
                        ? img
                        : null
            }).ToList()
        }).ToList();

        return products;
    }
}
//var productItemDtos = _mapper.Map<ProductItemDto>(products);

#region
//var productVm = await _unitOfWork.ProductRepository.GetByIdAsync(
//    filter: x => x.ProductOptionId == request.ProductOptionId,
//    selector: x => new ProductDetailVm
//    {
//        ProductOptionId = x.ProductOptionId,
//        Title = x.Title,
//        MinPrice = x.ProductVariants.Min(x => x.Price),
//        MaxPrice = x.ProductVariants.Max(x => x.Price),
//        Description = x.Description ?? string.Empty,
//        Category = x.Category.Title,
//        ProductType = x.Category.ProductType.Title,
//        Images = x.ProductImages.Select(img => new ImageLookupDto
//        {
//            ProductOptionId = img.ProductOptionId,
//            Url = img.Image,
//        }).ToList(),
//        Options = x.ProductOptions.Select(po => new OptionLookupDto 
//        { 
//            Title = po.OptionName,
//            OptionValues = po.OptionValues.Select(v => v.Value).ToList()
//        }).ToList(),
//        OptionValues = x.ProductOptions.Select(po => new OptionValueDto
//        {
//            Title = po.OptionName,
//            OptionValues = po.OptionValues.Select(v => v.Value).ToList(),
//            Options = po.OptionValues.Select(ov => new OptionValueImageDto
//            {
//                Title = ov.Value,
//                Label = ov.Label,
//                Image = ov.ProductImages!.Select(pi => new ImageLookupDto
//                {
//                    ProductOptionId = pi.ProductOptionId,
//                    Url = pi.Image
//                }).ToList()
//            }).ToList()
//        }).ToList(),
//    });
#endregion