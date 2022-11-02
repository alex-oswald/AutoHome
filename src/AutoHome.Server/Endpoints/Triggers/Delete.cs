using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoHome.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.Triggers;

public class Delete : EndpointBaseAsync
    .WithRequest<string>
    .WithActionResult
{
    private readonly IRepository<Trigger> _triggersRepo;
    private readonly ITriggersService _triggersService;

    public Delete(
        IRepository<Trigger> triggersRepo,
        ITriggersService triggersService)
    {
        _triggersRepo = triggersRepo;
        _triggersService = triggersService;
    }

    [HttpDelete("api/triggers/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Deletes a trigger",
        OperationId = "DeleteTrigger",
        Tags = new[] { "Triggers" }
    )]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute] string id, CancellationToken cancellationToken = default)
    {
        var entity = await _triggersRepo.GetByIdAsync(Guid.Parse(id), cancellationToken).ConfigureAwait(false);

        if (entity is null) return NotFound();

        await _triggersService.RemoveTriggerAsync(entity, cancellationToken).ConfigureAwait(false);

        return NoContent();
    }
}
