using Ardalis.ApiEndpoints;
using AutoHome.Data;
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

    [HttpDelete("api/devices/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Deletes a device",
        OperationId = "DeleteDevice",
        Tags = new[] { "Devices" }
    )]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute] string id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(Guid.Parse(id), cancellationToken).ConfigureAwait(false);

        if (entity is null) return NotFound();

        await _repository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);

        return NoContent();
    }
}
