namespace AutoHome.Server.Endpoints.WeatherReading;

public class ListWeatherReadingsRequest : IPagedRequest
{
    public int? PageIndex { get; set; }
    public int? PageSize { get; set; }
}
