using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.Variables;

public class Get : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<GetVariableResult>
{
    private readonly IRepository<Variable> _repository;
    private readonly IMapper _mapper;

    public Get(
        IRepository<Variable> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("api/variables/{id}", Name = "GetVariables")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetVariableResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesJson]
    [SwaggerOperation(
        Summary = "Gets a variable",
        OperationId = "GetVariable",
        Tags = new[] { "Variables" }
    )]
    public override async Task<ActionResult<GetVariableResult>> HandleAsync(
        Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);

        if (entity is null) return NotFound(id);

        var result = _mapper.Map<GetVariableResult>(entity);

        return result;
    }
}
