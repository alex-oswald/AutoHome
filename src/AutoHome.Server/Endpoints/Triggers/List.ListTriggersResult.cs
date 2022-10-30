using AutoHome.Server.Endpoints.Devices;

namespace AutoHome.Server.Endpoints.Triggers;

public class ListTriggersResult
{
    public Guid Id { get; set; }
    public Guid DeviceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Interval { get; set; }

    public GetDeviceResult Device { get; set; } = null!;
}