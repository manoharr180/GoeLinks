using System.Text.Json.Serialization;

namespace GeoLinks.ApiProxies.ShipRocket;

public sealed record ShipRocketLoginRequest(
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("password")] string Password);

public sealed class ShipRocketLoginResponse
{
    [JsonPropertyName("token")] public string Token { get; set; } = string.Empty;
}

public sealed record ShipRocketServiceabilityQuery(
    int PickupPostcode,
    int DeliveryPostcode,
    decimal Weight,
    bool Cod = false,
    string? OrderId = null,
    decimal? Length = null,
    decimal? Breadth = null,
    decimal? Height = null,
    string? DeliveryType = null);

public sealed record ShipRocketIdRequest([property: JsonPropertyName("shipment_id")] long ShipmentId);
public sealed record ShipRocketIdsRequest([property: JsonPropertyName("shipment_id")] IReadOnlyCollection<long> ShipmentIds);
public sealed record ShipRocketInvoiceRequest(
    [property: JsonPropertyName("ids")] IReadOnlyCollection<long> OrderIds,
    [property: JsonPropertyName("ropath")] string? RopPath = null);
