using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace AutoHome.PluginCore;

public interface ITokenProvider
{
    Task<string> GetTokenAsync(Device device, CancellationToken cancellationToken);
    Task<string> RefreshTokenAsync(Device device, CancellationToken cancellationToken);
}

public class TokenProvider : ITokenProvider
{
    private const string KEY = "token";
    private readonly MemoryCache _cache = new(Options.Create(new MemoryCacheOptions { SizeLimit = 1 }));
    private readonly MemoryCacheEntryOptions _cacheOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(365),
        Size = 1,
    };

    public Task<string> GetTokenAsync(Device device, CancellationToken cancellationToken) =>
        _cache.GetOrCreateAsync(KEY, async (entry) =>
        {
            var token = await GetNewTokenAsync(device, cancellationToken);
            entry.SetOptions(_cacheOptions);
            entry.SetValue(token);
            return token;
        });

    public async Task<string> RefreshTokenAsync(Device device, CancellationToken cancellationToken)
    {
        var token = await GetNewTokenAsync(device, cancellationToken);
        return _cache.Set(KEY, token, _cacheOptions);
    }

    private static async Task<string> GetNewTokenAsync(Device device, CancellationToken cancellationToken)
    {
        HttpClient client = new();
        var request = new TokenRequest { DeviceId = device.DeviceId.ToString() };
        var response = await client.PostAsJsonAsync(device.Uri + "/token", request, cancellationToken);
        var result = await response.Content.ReadFromJsonAsync<TokenResult>(cancellationToken: cancellationToken);
        return result?.Token ?? string.Empty;
    }

    class TokenRequest
    {
        public string DeviceId { get; set; } = null!;
    }
    class TokenResult
    {
        public string Token { get; set; } = null!;
    }
}
