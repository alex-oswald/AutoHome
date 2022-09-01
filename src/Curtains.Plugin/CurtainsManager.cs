using AutoHome.PluginCore;
using System.Net;
using System.Net.Http.Headers;

namespace Curtains.Plugin;

public interface ICurtainsManager
{
    Task CloseAsync(Device device, CancellationToken cancellationToken);
    Task OpenAsync(Device device, CancellationToken cancellationToken);
}

public class CurtainsManager : ICurtainsManager
{
    private readonly ITokenProvider _tokenProvider;

    public CurtainsManager(ITokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
    }

    public async Task OpenAsync(Device device, CancellationToken cancellationToken)
    {
        HttpClient client = new();

        var token = await _tokenProvider.GetTokenAsync(device, cancellationToken).ConfigureAwait(false);
        var request = new HttpRequestMessage(HttpMethod.Post, device.Uri + "/open");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            token = await _tokenProvider.RefreshTokenAsync(device, cancellationToken).ConfigureAwait(false);
            request = new HttpRequestMessage(HttpMethod.Post, device.Uri + "/open");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }

    public async Task CloseAsync(Device device, CancellationToken cancellationToken)
    {
        HttpClient client = new();

        var token = await _tokenProvider.GetTokenAsync(device, cancellationToken).ConfigureAwait(false);
        var request = new HttpRequestMessage(HttpMethod.Post, device.Uri + "/close");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            token = await _tokenProvider.RefreshTokenAsync(device, cancellationToken).ConfigureAwait(false);
            request = new HttpRequestMessage(HttpMethod.Post, device.Uri + "/close");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
