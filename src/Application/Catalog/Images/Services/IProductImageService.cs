using Application.Catalog.Images.Commands.UploadFile;
using Application.Catalog.Images.Queries.GetProductImage;
using Application.Common.Models;

namespace Application.Catalog.Images.Services;

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
