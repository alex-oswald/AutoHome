using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoHome.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.Devices;

public class Update : EndpointBaseAsync
    .WithRequest<UpdateDeviceRequest>
    .WithActionResult<UpdateDeviceResult>
{
    private readonly IAsyncRepository<Device> _repository;
    private readonly IMapper _mapper;

    public Update(
        IAsyncRepository<Device> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPut("api/devices")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateDeviceResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesJson]
    [ConsumesJson]
    [SwaggerOperation(
        Summary = "Updates a device",
        OperationId = "UpdateDevice",
        Tags = new[] { "Devices" }
    )]
    public override async Task<ActionResult<UpdateDeviceResult>> HandleAsync(
        [FromBody] UpdateDeviceRequest request, CancellationToken cancellationToken = default)
    {
        var device = await _repository.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);

        if (device is null) return NotFound();

        _mapper.Map(request, device);
        await _repository.UpdateAsync(device, cancellationToken).ConfigureAwait(false);

        UpdateDeviceResult result = new();
        _mapper.Map(device, result);
        return result;
    }
}
