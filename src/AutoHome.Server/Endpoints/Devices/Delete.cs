using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoHome.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AutoHome.Server.Endpoints.Devices;

public class Delete : EndpointBaseAsync
    .WithRequest<DeleteDeviceRequest>
    .WithActionResult
{
    private readonly IAsyncRepository<Device> _repository;

    public Delete(
        IAsyncRepository<Device> repository)
    {
        _repository = repository;
    }

    [HttpDelete("api/devices/{id}")]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute] DeleteDeviceRequest request, CancellationToken cancellationToken = default)
    {
        var device = await _repository.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);

        if (device is null) return NotFound();

        await _repository.DeleteAsync(device, cancellationToken).ConfigureAwait(false);

        return NoContent();
    }
}
