using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ListDevicesResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesJson]
    [SwaggerOperation(
        Summary = "Lists devices",
        OperationId = "ListDevice",
        Tags = new[] { "Devices" }
    )]
    public override async Task<IEnumerable<ListDevicesResult>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAllAsync(
            orderBy: o => o.OrderBy(o => o.Name),
            cancellationToken: cancellationToken).ConfigureAwait(false);
        var mappedResult = result!.AsEnumerable().Select(i => _mapper.Map<ListDevicesResult>(i));
        return mappedResult;
    }
}
