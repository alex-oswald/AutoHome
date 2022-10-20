namespace AutoHome.Server.Endpoints.Devices;

public class GetDeviceResult
{
    public Guid Id { get; set; }
    public Guid IntegrationDeviceId { get; set; }
    public string Type { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Uri { get; set; } = null!;
}
