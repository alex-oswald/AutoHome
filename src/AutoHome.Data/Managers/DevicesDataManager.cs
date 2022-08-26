using AutoHome.Data.EndpointObjects.Devices;
using System.Net.Http.Json;

namespace AutoHome.Data.Managers;

public interface IDevicesManager
{
    Task<AddDeviceResult?> AddAsync(AddDeviceRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(DeleteDeviceRequest request, CancellationToken cancellationToken);
    Task<GetDeviceResult?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<ListDevicesResult>?> ListAsync(CancellationToken cancellationToken);
    Task<UpdateDeviceResult?> UpdateAsync(UpdateDeviceRequest request, CancellationToken cancellationToken);
}

public class DevicesManager : IDevicesManager
{
    private readonly HttpClient _client;

    public DevicesManager(HttpClient client)
    {
        _client = client;
    }

    public async Task<AddDeviceResult?> AddAsync(AddDeviceRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync("api/devices", request, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<AddDeviceResult>(cancellationToken: cancellationToken).ConfigureAwait(false);
        return result;
    }

    public async Task DeleteAsync(DeleteDeviceRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.DeleteAsync($"api/devices/{request.Id}", cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
    }

    public async Task<GetDeviceResult?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"api/devices/{id}", cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<GetDeviceResult>(cancellationToken: cancellationToken).ConfigureAwait(false);
        return result;
    }

    public async Task<IEnumerable<ListDevicesResult>?> ListAsync(CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync("api/devices", cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<IEnumerable<ListDevicesResult>>(cancellationToken: cancellationToken).ConfigureAwait(false);
        return result;
    }

    public async Task<UpdateDeviceResult?> UpdateAsync(UpdateDeviceRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PutAsJsonAsync("api/devices", request, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<UpdateDeviceResult>(cancellationToken: cancellationToken).ConfigureAwait(false);
        return result;
    }
}