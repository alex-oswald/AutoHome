using Ardalis.ApiEndpoints;
using AutoHome.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoHome.Server.Endpoints.WeatherReading;

public class List : EndpointBaseAsync
    .WithRequest<ListWeatherReadingsRequest>
    .WithResult<IPagedResult<ListWeatherReadingsResult>>
{
    private readonly IRepository<Core.Entities.WeatherReading> _triggerEventsRepo;
    private readonly IMapper _mapper;

    public List(
        IRepository<Core.Entities.WeatherReading> triggerEventsRepo,
        IMapper mapper)
    {
        _triggerEventsRepo = triggerEventsRepo;
        _mapper = mapper;
    }

    [HttpGet("api/weatherreadings")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IPagedResult<ListWeatherReadingsResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesJson]
    [SwaggerOperation(
        Summary = "Lists weather readings",
        OperationId = "ListWeatherReadings",
        Tags = new[] { "Weather Readings" }
    )]
    public override async Task<IPagedResult<ListWeatherReadingsResult>> HandleAsync(
        [FromQuery] ListWeatherReadingsRequest request, CancellationToken cancellationToken = default)
    {
        IPagedResult<Core.Entities.WeatherReading> list = await _triggerEventsRepo.GetPageAsync(
            pagedRequest: request,
            orderBy: o => o.OrderByDescending(o => o.UtcDate),
            cancellationToken: cancellationToken).ConfigureAwait(false);
        var result = list.MapTo(_mapper.Map<ListWeatherReadingsResult>);
        return result;
    }
}