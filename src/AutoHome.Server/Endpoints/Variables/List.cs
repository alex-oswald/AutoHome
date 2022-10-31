using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.Variables;

public class List : EndpointBaseAsync
    .WithoutRequest
    .WithResult<IEnumerable<ListVariablesResult>>
{
    private readonly IAsyncRepository<Variable> _repository;
    private readonly IMapper _mapper;

    public List(
        IAsyncRepository<Variable> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("api/variables")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ListVariablesResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesJson]
    [SwaggerOperation(
        Summary = "Lists variables",
        OperationId = "ListVariable",
        Tags = new[] { "Variables" }
    )]
    public override async Task<IEnumerable<ListVariablesResult>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAllAsync(
            orderBy: o => o.OrderBy(o => o.Key),
            cancellationToken: cancellationToken).ConfigureAwait(false);
        var mappedResult = result!.AsEnumerable().Select(i => _mapper.Map<ListVariablesResult>(i));
        return mappedResult;
    }
}
