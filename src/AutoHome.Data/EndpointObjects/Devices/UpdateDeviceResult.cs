namespace AutoHome.Data.EndpointObjects.Devices;

public class UpdateDeviceResult
{
    public Guid Id { get; set; }
    public Guid DeviceId { get; set; }
    public string Name { get; set; } = null!;
    public string Uri { get; set; } = null!;
}
