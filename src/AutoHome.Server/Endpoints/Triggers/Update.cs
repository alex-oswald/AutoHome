using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoHome.Server.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.Triggers;

public class Update : EndpointBaseAsync
    .WithRequest<UpdateTriggerRequest>
    .WithActionResult<UpdateTriggerResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Trigger> _triggersRepo;
    private readonly ITriggersService _triggersService;

    public Update(
        IMapper mapper,
        IRepository<Trigger> triggersRepo,
        ITriggersService triggersService)
    {
        _mapper = mapper;
        _triggersRepo = triggersRepo;
        _triggersService = triggersService;
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
        var entity = await _triggersRepo.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);

        if (entity is null) return NotFound();

        _mapper.Map(request, entity);
        await _triggersService.UpdateTriggerAsync(entity, cancellationToken).ConfigureAwait(false);

        UpdateTriggerResult result = new();
        _mapper.Map(entity, result);
        return result;
    }
}
