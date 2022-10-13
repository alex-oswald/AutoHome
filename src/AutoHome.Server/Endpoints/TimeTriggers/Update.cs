using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoHome.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.TimeTriggers;

public class Update : EndpointBaseAsync
    .WithRequest<UpdateTimeTriggerRequest>
    .WithActionResult<UpdateTimeTriggerResult>
{
    private readonly IAsyncRepository<TimeTrigger> _repository;
    private readonly IMapper _mapper;

    public Update(
        IAsyncRepository<TimeTrigger> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPut("api/timetriggers")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateTimeTriggerResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesJson]
    [ConsumesJson]
    [SwaggerOperation(
        Summary = "Updates a time trigger",
        OperationId = "UpdateTimeTrigger",
        Tags = new[] { "Time Triggers" }
    )]
    public override async Task<ActionResult<UpdateTimeTriggerResult>> HandleAsync(
        [FromBody] UpdateTimeTriggerRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);

        if (entity is null) return NotFound();

        _mapper.Map(request, entity);
        await _repository.UpdateAsync(entity, cancellationToken).ConfigureAwait(false);

        UpdateTimeTriggerResult result = new();
        _mapper.Map(entity, result);
        return result;
    }
}
