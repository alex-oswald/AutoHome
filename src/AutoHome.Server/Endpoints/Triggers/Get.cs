using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.Triggers;

public class Get : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<GetTriggerResult>
{
    private readonly IAsyncRepository<Trigger> _repository;
    private readonly IMapper _mapper;

    public Get(
        IAsyncRepository<Trigger> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("api/triggers/{id}", Name = "GetTriggers")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTriggerResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesJson]
    [SwaggerOperation(
        Summary = "Gets a trigger",
        OperationId = "GetTrigger",
        Tags = new[] { "Triggers" }
    )]
    public override async Task<ActionResult<GetTriggerResult>> HandleAsync(
        Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);

        if (entity is null) return NotFound(id);

        var result = _mapper.Map<GetTriggerResult>(entity);

        return result;
    }
}
