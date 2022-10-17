using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.Devices;

public class Add : EndpointBaseAsync
    .WithRequest<AddDeviceRequest>
    .WithActionResult<AddDeviceResult>
{
    private readonly IAsyncRepository<Device> _repository;
    private readonly IMapper _mapper;

    public Add(
        IAsyncRepository<Device> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPost("api/devices")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AddDeviceResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesJson]
    [ConsumesJson]
    [SwaggerOperation(
        Summary = "Adds a device",
        OperationId = "AddDevice",
        Tags = new[] { "Devices" }
    )]
    public override async Task<ActionResult<AddDeviceResult>> HandleAsync(
        [FromBody] AddDeviceRequest request, CancellationToken cancellationToken = default)
    {
        var entity = new Device();
        _mapper.Map(request, entity);
        await _repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);

        var result = _mapper.Map<AddDeviceResult>(entity);
        return CreatedAtRoute("GetDevices", new { id = result.Id }, result);
    }
}
