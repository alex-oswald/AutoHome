using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoHome.Data.EndpointObjects.Devices;
using AutoHome.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AutoHome.Server.Endpoints.Devices;

public class List : EndpointBaseAsync
    .WithoutRequest
    .WithResult<IEnumerable<ListDevicesResult>>
{
    private readonly IAsyncRepository<Device> _repository;
    private readonly IMapper _mapper;

    public List(
        IAsyncRepository<Device> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("api/devices")]
    public override async Task<IEnumerable<ListDevicesResult>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.ListAsync(orderBy: o => o.OrderBy(o => o.Name), cancellationToken: cancellationToken).ConfigureAwait(false);
        var mappedResult = result!.AsEnumerable().Select(i => _mapper.Map<ListDevicesResult>(i));
        return mappedResult;
    }
}
