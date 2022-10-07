using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoHome.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.Devices;

public class Delete : EndpointBaseAsync
    .WithRequest<string>
    .WithActionResult
{
    private readonly IAsyncRepository<Device> _repository;

    public Delete(
        IAsyncRepository<Device> repository)
    {
        _repository = repository;
    }

    /// <response code="204" nullable="true">The order.</response>
    [HttpDelete("api/devices/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(NoContentResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorResult))]
    [SwaggerOperation(
        Summary = "Deletes a device",
        OperationId = "DeleteDevice",
        Tags = new[] { "Devices" }
    )]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute] string id, CancellationToken cancellationToken = default)
    {
        var device = await _repository.GetByIdAsync(Guid.Parse(id), cancellationToken).ConfigureAwait(false);

        if (device is null) return NotFound();

        await _repository.DeleteAsync(device, cancellationToken).ConfigureAwait(false);

        return NoContent();
    }
}
