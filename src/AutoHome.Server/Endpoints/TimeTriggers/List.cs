using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoHome.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.TimeTriggers;

public class List : EndpointBaseAsync
    .WithoutRequest
    .WithResult<IEnumerable<ListTimeTriggersResult>>
{
    private readonly IAsyncRepository<TimeTrigger> _repository;
    private readonly IMapper _mapper;

    public List(
        IAsyncRepository<TimeTrigger> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("api/timetriggers")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ListTimeTriggersResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesJson]
    [SwaggerOperation(
        Summary = "Lists time triggers",
        OperationId = "ListTimeTrigger",
        Tags = new[] { "Time Triggers" }
    )]
    public override async Task<IEnumerable<ListTimeTriggersResult>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.ListAsync(orderBy: o => o.OrderBy(o => o.Name), cancellationToken: cancellationToken).ConfigureAwait(false);
        var mappedResult = result!.AsEnumerable().Select(i => _mapper.Map<ListTimeTriggersResult>(i));
        return mappedResult;
    }
}
