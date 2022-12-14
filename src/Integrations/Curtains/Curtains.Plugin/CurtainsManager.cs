using AutoHome.Core.Entities;

namespace Curtains.Plugin;

public interface ICurtainsManager
{
    Task CloseAsync(Device device, CancellationToken cancellationToken);
    Task OpenAsync(Device device, CancellationToken cancellationToken);
}

public class CurtainsManager : ICurtainsManager
{
    private readonly Func<HttpClient> _httpClientFactory;

    public CurtainsManager()
    {
        _httpClientFactory = () => new HttpClient();
    }

    public CurtainsManager(Func<HttpClient> httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task OpenAsync(Device device, CancellationToken cancellationToken)
    {
        HttpClient client = _httpClientFactory();

        var request = new HttpRequestMessage(HttpMethod.Post, device.Uri + "/open");
        request.Headers.Add("ApiKey", new string[] { device.IntegrationDeviceId.ToString() });
        var response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }

    public async Task CloseAsync(Device device, CancellationToken cancellationToken)
    {
        HttpClient client = _httpClientFactory();

        var request = new HttpRequestMessage(HttpMethod.Post, device.Uri + "/close");
        request.Headers.Add("ApiKey", new string[] { device.IntegrationDeviceId.ToString() });
        var response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}
