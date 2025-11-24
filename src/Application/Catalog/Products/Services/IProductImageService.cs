using Application.Catalog.Products.Commands.UploadFile;
using Application.Catalog.Products.Queries.GetProductImage;
using Application.Common.Models;

namespace Application.Catalog.Products.Services;

public interface IProductImageService
{
    Task<ProductImageResult> GetOrderedImagesAsync(
        Guid productId, 
        Guid? optionValueId = null,
        CancellationToken ct = default);
    Task<ImageLookupDto?> GetPrimaryImageAsync(
        Guid productId, 
        Guid? optionValueId = null,
        CancellationToken ct = default);

    Task<bool> BeValidImageRules(
        UploadFileCommand cmd,
        CancellationToken ct);


}
