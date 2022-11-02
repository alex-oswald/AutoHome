namespace AutoHome.Server.Endpoints.Devices;

public class ListDevicesRequest : IPagedRequest
{
    public int? PageIndex { get; set; }
    public int? PageSize { get; set; }
}
