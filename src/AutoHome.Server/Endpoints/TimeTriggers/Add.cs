using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.TimeTriggers;

public class Add : EndpointBaseAsync
    .WithRequest<AddTimeTriggerRequest>
    .WithActionResult<AddTimeTriggerResult>
{
    private readonly IAsyncRepository<TimeTrigger> _repository;
    private readonly IMapper _mapper;

    public Add(
        IAsyncRepository<TimeTrigger> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPost("api/timetriggers")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AddTimeTriggerResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesJson]
    [ConsumesJson]
    [SwaggerOperation(
        Summary = "Adds a time trigger",
        OperationId = "AddTimeTrigger",
        Tags = new[] { "Time Triggers" }
    )]
    public override async Task<ActionResult<AddTimeTriggerResult>> HandleAsync(
        [FromBody] AddTimeTriggerRequest request, CancellationToken cancellationToken = default)
    {
        var entity = new TimeTrigger();
        _mapper.Map(request, entity);
        await _repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);

        var result = _mapper.Map<AddTimeTriggerResult>(entity);
        return CreatedAtRoute("GetTimeTriggers", new { id = result.Id }, result);
    }
}
