using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.Variables;

public class Add : EndpointBaseAsync
    .WithRequest<AddVariableRequest>
    .WithActionResult<AddVariableResult>
{
    private readonly IRepository<Variable> _repository;
    private readonly IMapper _mapper;

    public Add(
        IRepository<Variable> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPost("api/variables")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AddVariableResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesJson]
    [ConsumesJson]
    [SwaggerOperation(
        Summary = "Adds a variable",
        OperationId = "AddVariable",
        Tags = new[] { "Variables" }
    )]
    public override async Task<ActionResult<AddVariableResult>> HandleAsync(
        [FromBody] AddVariableRequest request, CancellationToken cancellationToken = default)
    {
        var entity = new Variable();
        _mapper.Map(request, entity);
        await _repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);

        var result = _mapper.Map<AddVariableResult>(entity);
        return CreatedAtRoute("GetVariables", new { id = result.Id }, result);
    }
}
