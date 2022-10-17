using Ardalis.ApiEndpoints;
using AutoHome.Data;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.TimeTriggers;

public class Delete : EndpointBaseAsync
    .WithRequest<string>
    .WithActionResult
{
    private readonly IAsyncRepository<TimeTrigger> _repository;

    public Delete(
        IAsyncRepository<TimeTrigger> repository)
    {
        _repository = repository;
    }

    [HttpDelete("api/timetriggers/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Deletes a time trigger",
        OperationId = "DeleteTimeTrigger",
        Tags = new[] { "Time Triggers" }
    )]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute] string id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(Guid.Parse(id), cancellationToken).ConfigureAwait(false);

        if (entity is null) return NotFound();

        await _repository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);

        return NoContent();
    }
}
