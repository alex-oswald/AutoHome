using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoHome.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.TriggerEvents;

public class List : EndpointBaseAsync
    .WithoutRequest
    .WithResult<IEnumerable<ListTriggerEventsResult>>
{
    private readonly IAsyncRepository<TriggerEvent> _triggerEventsRepo;
    private readonly IMapper _mapper;

    public List(
        IAsyncRepository<TriggerEvent> triggerEventsRepo,
        IMapper mapper)
    {
        _triggerEventsRepo = triggerEventsRepo;
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
        var result = await _triggerEventsRepo.Set().OrderByDescending(o => o.TimeStamp).Take(10).ToListAsync();
        //cancellationToken: cancellationToken,
        //orderBy: o => o.OrderBy(o => o.Name).Take).ConfigureAwait(false);
        var mappedResult = result!.AsEnumerable().Select(i => _mapper.Map<ListTriggerEventsResult>(i));
        return mappedResult;
    }
}
