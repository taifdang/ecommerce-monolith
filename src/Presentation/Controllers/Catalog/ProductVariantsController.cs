using Application.Catalog.Variants.Commands.DeleteVariant;
using Application.Catalog.Variants.Commands.GenerateVariant;
using Application.Catalog.Variants.Commands.UpdateManyVariant;
using Application.Catalog.Variants.Commands.UpdateVariant;
using Application.Catalog.Variants.Queries.GetVariantById;
using Application.Catalog.Variants.Queries.GetVariantByOption;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Catalog;

[Route(BaseApiPath + "/product/variants")]
public class ProductVariantsController : BaseController
{
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById([FromQuery]GetVariantByIdQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("by-option-values")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByOption([FromQuery] GetVariantByOptionQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost("generate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GenerateVariants([FromBody] GenerateVariantCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateVariant(UpdateVariantCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    [HttpPut("update-many")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateManyVariant(UpdateManyVariantCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteVariant(DeleteVariantCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
