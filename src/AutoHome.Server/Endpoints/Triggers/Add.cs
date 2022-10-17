using Ardalis.ApiEndpoints;
using AutoHome.Server.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.Triggers;

public class Add : EndpointBaseAsync
    .WithRequest<AddTriggerRequest>
    .WithActionResult<AddTriggerResult>
{
    private readonly IMapper _mapper;
    private readonly ITriggersService _triggersService;

    public Add(
        IMapper mapper,
        ITriggersService triggersService)
    {
        _mapper = mapper;
        _triggersService = triggersService;
    }

    [HttpPost("api/triggers")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AddTriggerResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesJson]
    [ConsumesJson]
    [SwaggerOperation(
        Summary = "Adds a trigger",
        OperationId = "AddTrigger",
        Tags = new[] { "Triggers" }
    )]
    public override async Task<ActionResult<AddTriggerResult>> HandleAsync(
        [FromBody] AddTriggerRequest request, CancellationToken cancellationToken = default)
    {
        var entity = new Trigger();
        _mapper.Map(request, entity);

        // Add the trigger to the trigger service
        await _triggersService.AddTriggerAsync(entity, cancellationToken).ConfigureAwait(false);

        var result = _mapper.Map<AddTriggerResult>(entity);
        return CreatedAtRoute("GetTriggers", new { id = result.Id }, result);
    }
}
