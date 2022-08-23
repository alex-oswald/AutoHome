using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoHome.Data.EndpointObjects.Devices;
using AutoHome.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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
    public override async Task<ActionResult<AddDeviceResult>> HandleAsync(
        [FromBody] AddDeviceRequest request, CancellationToken cancellationToken = default)
    {
        var device = new Device();
        _mapper.Map(request, device);
        await _repository.AddAsync(device, cancellationToken).ConfigureAwait(false);

        var result = _mapper.Map<AddDeviceResult>(device);
        return CreatedAtRoute("Devices_Get", new { id = result.Id }, result);
    }
}
