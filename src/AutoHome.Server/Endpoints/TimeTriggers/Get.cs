using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoHome.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.TimeTriggers;

public class Get : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<GetTimeTriggerResult>
{
    private readonly IAsyncRepository<TimeTrigger> _repository;
    private readonly IMapper _mapper;

    public Get(
        IAsyncRepository<TimeTrigger> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("api/timetriggers/{id}", Name = "GetTimeTriggers")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTimeTriggerResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesJson]
    [SwaggerOperation(
        Summary = "Gets a time trigger",
        OperationId = "GetTimeTrigger",
        Tags = new[] { "Time Triggers" }
    )]
    public override async Task<ActionResult<GetTimeTriggerResult>> HandleAsync(
        Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);

        if (entity is null) return NotFound(id);

        var result = _mapper.Map<GetTimeTriggerResult>(entity);

        return result;
    }
}
