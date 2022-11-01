namespace AutoHome.Server.Integrations.AmbientWeather;

public class AmbientWeatherApiOptions
{
    public bool BackgroundServiceEnabled { get; set; }
    public string? MacAddress { get; set; }
    public List<string> ApiKeys { get; set; } = null!;
    public string ApplicationKey { get; set; } = null!;
}
