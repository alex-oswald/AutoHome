using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.TriggerEvents;

public class List : EndpointBaseAsync
    .WithoutRequest
    .WithResult<IEnumerable<ListTriggerEventsResult>>
{
    private readonly IAsyncRepository<Trigger> _repository;
    private readonly IMapper _mapper;

    public List(
        IAsyncRepository<Trigger> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("api/triggerevents")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ListTriggerEventsResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesJson]
    [SwaggerOperation(
        Summary = "Lists triggers events",
        OperationId = "ListTriggerEvents",
        Tags = new[] { "Trigger Events" }
    )]
    public override async Task<IEnumerable<ListTriggerEventsResult>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.ListAsync(orderBy: o => o.OrderBy(o => o.Name), cancellationToken: cancellationToken).ConfigureAwait(false);
        var mappedResult = result!.AsEnumerable().Select(i => _mapper.Map<ListTriggerEventsResult>(i));
        return mappedResult;
    }
}
