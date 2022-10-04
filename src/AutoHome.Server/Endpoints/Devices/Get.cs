using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoHome.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("api/devices/{id}", Name = "Devices_Get")]
    public override async Task<ActionResult<GetDeviceResult>> HandleAsync(
        Guid id, CancellationToken cancellationToken = default)
    {
        var device = await _repository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);

        if (device is null) return NotFound(id);

        var result = _mapper.Map<GetDeviceResult>(device);

        return result;
    }
}
