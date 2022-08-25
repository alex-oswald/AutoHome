using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoHome.Data.EndpointObjects.Devices;
using AutoHome.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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
