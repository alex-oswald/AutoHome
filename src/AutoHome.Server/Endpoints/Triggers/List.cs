using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.Triggers;

public class List : EndpointBaseAsync
    .WithoutRequest
    .WithResult<IEnumerable<ListTriggersResult>>
{
    private readonly IAsyncRepository<Trigger> _repository;
    private readonly IMapper _mapper;

    public List(
        IAsyncRepository<Trigger> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("api/triggers")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ListTriggersResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesJson]
    [SwaggerOperation(
        Summary = "Lists triggers",
        OperationId = "ListTrigger",
        Tags = new[] { "Triggers" }
    )]
    public override async Task<IEnumerable<ListTriggersResult>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.ListAsync(orderBy: o => o.OrderBy(o => o.Name), cancellationToken: cancellationToken).ConfigureAwait(false);
        var mappedResult = result!.AsEnumerable().Select(i => _mapper.Map<ListTriggersResult>(i));
        return mappedResult;
    }
}
