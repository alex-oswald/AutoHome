using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.Triggers;

public class Update : EndpointBaseAsync
    .WithRequest<UpdateTriggerRequest>
    .WithActionResult<UpdateTriggerResult>
{
    private readonly IAsyncRepository<Trigger> _repository;
    private readonly IMapper _mapper;

    public Update(
        IAsyncRepository<Trigger> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPut("api/triggers")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateTriggerResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesJson]
    [ConsumesJson]
    [SwaggerOperation(
        Summary = "Updates a trigger",
        OperationId = "UpdateTrigger",
        Tags = new[] { "Triggers" }
    )]
    public override async Task<ActionResult<UpdateTriggerResult>> HandleAsync(
        [FromBody] UpdateTriggerRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);

        if (entity is null) return NotFound();

        _mapper.Map(request, entity);
        await _repository.UpdateAsync(entity, cancellationToken).ConfigureAwait(false);

        UpdateTriggerResult result = new();
        _mapper.Map(entity, result);
        return result;
    }
}
