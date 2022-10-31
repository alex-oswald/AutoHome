using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.Variables;

public class Update : EndpointBaseAsync
    .WithRequest<UpdateVariableRequest>
    .WithActionResult<UpdateVariableResult>
{
    private readonly IAsyncRepository<Variable> _repository;
    private readonly IMapper _mapper;

    public Update(
        IAsyncRepository<Variable> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPut("api/variables")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateVariableResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesJson]
    [ConsumesJson]
    [SwaggerOperation(
        Summary = "Updates a variable",
        OperationId = "UpdateVariable",
        Tags = new[] { "Variables" }
    )]
    public override async Task<ActionResult<UpdateVariableResult>> HandleAsync(
        [FromBody] UpdateVariableRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);

        if (entity is null) return NotFound();

        _mapper.Map(request, entity);
        await _repository.UpdateAsync(entity, cancellationToken).ConfigureAwait(false);

        UpdateVariableResult result = new();
        _mapper.Map(entity, result);
        return result;
    }
}
