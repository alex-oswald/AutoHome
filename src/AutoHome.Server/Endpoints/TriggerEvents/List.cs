using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoHome.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.TriggerEvents;

public class List : EndpointBaseAsync
    .WithRequest<ListTriggerEventsRequest>
    .WithResult<IPagedResult<ListTriggerEventsResult>>
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IPagedResult<ListTriggerEventsResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesJson]
    [SwaggerOperation(
        Summary = "Lists triggers events",
        OperationId = "ListTriggerEvents",
        Tags = new[] { "Trigger Events" }
    )]
    public override async Task<IPagedResult<ListTriggerEventsResult>> HandleAsync(
        [FromQuery] ListTriggerEventsRequest request, CancellationToken cancellationToken = default)
    {
        IPagedResult<TriggerEvent> list = await _triggerEventsRepo.GetPageAsync(
            pagedRequest: request,
            orderBy: o => o.OrderByDescending(o => o.TimeStamp),
            cancellationToken: cancellationToken,
            includeProperties: string.Join(',', nameof(Trigger))).ConfigureAwait(false);
        var result = list.MapTo(_mapper.Map<ListTriggerEventsResult>);
        return result;
    }
}
