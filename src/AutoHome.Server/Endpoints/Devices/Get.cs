using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.Devices;

public class Get : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<GetDeviceResult>
{
    private readonly IAsyncRepository<Device> _repository;
    private readonly IMapper _mapper;

    public Get(
        IAsyncRepository<Device> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("api/devices/{id}", Name = "GetDevices")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetDeviceResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesJson]
    [SwaggerOperation(
        Summary = "Gets a device",
        OperationId = "GetDevice",
        Tags = new[] { "Devices" }
    )]
    public override async Task<ActionResult<GetDeviceResult>> HandleAsync(
        Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);

        if (entity is null) return NotFound(id);

        var result = _mapper.Map<GetDeviceResult>(entity);

        return result;
    }
}
