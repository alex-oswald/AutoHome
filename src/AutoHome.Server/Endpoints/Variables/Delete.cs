using Ardalis.ApiEndpoints;
using AutoHome.Data;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.Variables;

public class Delete : EndpointBaseAsync
    .WithRequest<string>
    .WithActionResult
{
    private readonly IRepository<Variable> _repository;

    public Delete(
        IRepository<Variable> repository)
    {
        _repository = repository;
    }

    [HttpDelete("api/variables/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Deletes a variable",
        OperationId = "DeleteVariable",
        Tags = new[] { "Variables" }
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
