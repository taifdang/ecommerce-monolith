using Application.Catalog.Images.Commands.DeleteFile;
using Application.Catalog.Images.Commands.UploadFile;
using Shared.Web;
using Microsoft.AspNetCore.Mvc;
using Application.Catalog.Images.Queries.GetProductImage;

namespace Api.Controllers.Catalog;

[Route(BaseApiPath + "/product/images")]
public class ProductImagesController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetImage([FromQuery]GetProductImageQuery command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddImage(UploadFileCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteImage(DeleteFileCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
