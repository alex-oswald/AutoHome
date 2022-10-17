using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.Triggers;

public class Add : EndpointBaseAsync
    .WithRequest<AddTriggerRequest>
    .WithActionResult<AddTriggerResult>
{
    private readonly IAsyncRepository<Trigger> _repository;
    private readonly IMapper _mapper;

    public Add(
        IAsyncRepository<Trigger> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
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
        await _repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);

        var result = _mapper.Map<AddTriggerResult>(entity);
        return CreatedAtRoute("GetTriggers", new { id = result.Id }, result);
    }
}
