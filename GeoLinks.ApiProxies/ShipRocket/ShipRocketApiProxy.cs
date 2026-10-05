using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace GeoLinks.ApiProxies.ShipRocket;

public sealed class ShipRocketApiProxy : IShipRocketApiProxy
{
    private const string LoginPath = "auth/login";
    private readonly HttpClient _http;
    private readonly ShipRocketOptions _options;
    private readonly SemaphoreSlim _tokenLock = new(1, 1);
    private string? _token;

    public ShipRocketApiProxy(HttpClient http, IOptions<ShipRocketOptions> options)
    {
        _http = http;
        _options = options.Value;
    }

    public async Task<T> GetServiceabilityAsync<T>(ShipRocketServiceabilityQuery query, CancellationToken cancellationToken = default)
    {
        var values = new Dictionary<string, string>
        {
            ["pickup_postcode"] = query.PickupPostcode.ToString(CultureInfo.InvariantCulture),
            ["delivery_postcode"] = query.DeliveryPostcode.ToString(CultureInfo.InvariantCulture),
            ["weight"] = query.Weight.ToString(CultureInfo.InvariantCulture),
            ["cod"] = query.Cod ? "1" : "0"
        };
        Add(values, "order_id", query.OrderId);
        Add(values, "length", query.Length);
        Add(values, "breadth", query.Breadth);
        Add(values, "height", query.Height);
        Add(values, "delivery_type", query.DeliveryType);
        var url = "courier/serviceability/?" + string.Join("&", values.Select(pair =>
            Uri.EscapeDataString(pair.Key) + "=" + Uri.EscapeDataString(pair.Value)));
        return await SendAsync<T>(HttpMethod.Get, url, null, cancellationToken);
    }

    public Task<T> CreateOrderAsync<T>(object order, CancellationToken cancellationToken = default) =>
        SendAsync<T>(HttpMethod.Post, "orders/create/adhoc", order, cancellationToken);

    public Task<T> AssignAwbAsync<T>(long shipmentId, string? courierId = null, CancellationToken cancellationToken = default)
    {
        var body = new Dictionary<string, object?> { ["shipment_id"] = shipmentId };
        if (!string.IsNullOrWhiteSpace(courierId)) body["courier_id"] = courierId;
        return SendAsync<T>(HttpMethod.Post, "courier/assign/awb", body, cancellationToken);
    }

    public Task<T> GeneratePickupAsync<T>(IReadOnlyCollection<long> shipmentIds, CancellationToken cancellationToken = default) =>
        SendAsync<T>(HttpMethod.Post, "courier/generate/pickup", new ShipRocketIdsRequest(shipmentIds), cancellationToken);

    public Task<T> GenerateManifestAsync<T>(IReadOnlyCollection<long> shipmentIds, CancellationToken cancellationToken = default) =>
        SendAsync<T>(HttpMethod.Post, "manifests/generate", new ShipRocketIdsRequest(shipmentIds), cancellationToken);

    public Task<T> PrintManifestAsync<T>(IReadOnlyCollection<long> shipmentIds, CancellationToken cancellationToken = default) =>
        SendAsync<T>(HttpMethod.Post, "manifests/print", new ShipRocketIdsRequest(shipmentIds), cancellationToken);

    public Task<T> GenerateLabelAsync<T>(IReadOnlyCollection<long> shipmentIds, CancellationToken cancellationToken = default) =>
        SendAsync<T>(HttpMethod.Post, "courier/generate/label", new ShipRocketIdsRequest(shipmentIds), cancellationToken);

    public Task<T> PrintInvoiceAsync<T>(IReadOnlyCollection<long> orderIds, CancellationToken cancellationToken = default) =>
        SendAsync<T>(HttpMethod.Post, "orders/print/invoice", new ShipRocketInvoiceRequest(orderIds), cancellationToken);

    public Task<T> TrackAwbAsync<T>(string awbCode, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(awbCode)) throw new ArgumentException("AWB code is required.", nameof(awbCode));
        return SendAsync<T>(HttpMethod.Get, $"courier/track/awb/{Uri.EscapeDataString(awbCode)}", null, cancellationToken);
    }

    private async Task<T> SendAsync<T>(HttpMethod method, string path, object? body, CancellationToken cancellationToken)
    {
        var token = await GetTokenAsync(cancellationToken);
        using var request = new HttpRequestMessage(method, path);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        if (body is not null) request.Content = JsonContent.Create(body);
        using var response = await _http.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Shiprocket returned {(int)response.StatusCode} ({response.StatusCode}): {responseBody}", null, response.StatusCode);
        return JsonSerializer.Deserialize<T>(responseBody, new JsonSerializerOptions(JsonSerializerDefaults.Web))
            ?? throw new JsonException("Shiprocket returned an empty or invalid JSON response.");
    }

    private async Task<string> GetTokenAsync(CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(_token)) return _token;
        await _tokenLock.WaitAsync(cancellationToken);
        try
        {
            if (!string.IsNullOrWhiteSpace(_token)) return _token;
            if (string.IsNullOrWhiteSpace(_options.Email) || string.IsNullOrWhiteSpace(_options.Password))
                throw new InvalidOperationException("Shiprocket API credentials are not configured. Set ShipRocket:Email and ShipRocket:Password.");
            using var response = await _http.PostAsJsonAsync(LoginPath, new ShipRocketLoginRequest(_options.Email, _options.Password), cancellationToken);
            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Shiprocket login returned {(int)response.StatusCode} ({response.StatusCode}): {json}", null, response.StatusCode);
            var login = JsonSerializer.Deserialize<ShipRocketLoginResponse>(json, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            if (string.IsNullOrWhiteSpace(login?.Token)) throw new JsonException("Shiprocket login response did not contain a token.");
            _token = login.Token;
            return _token;
        }
        finally { _tokenLock.Release(); }
    }

    private static void Add(IDictionary<string, string> values, string key, object? value)
    {
        if (value is null) return;
        var text = value is IFormattable formattable ? formattable.ToString(null, CultureInfo.InvariantCulture) : value.ToString();
        if (!string.IsNullOrWhiteSpace(text)) values[key] = text;
    }
}
