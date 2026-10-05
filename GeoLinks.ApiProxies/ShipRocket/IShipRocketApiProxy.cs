namespace GeoLinks.ApiProxies.ShipRocket;

/// <summary>Typed access to Shiprocket's external API. Payloads and responses remain JSON-friendly objects.</summary>
public interface IShipRocketApiProxy
{
    Task<T> GetServiceabilityAsync<T>(ShipRocketServiceabilityQuery query, CancellationToken cancellationToken = default);
    Task<T> CreateOrderAsync<T>(object order, CancellationToken cancellationToken = default);
    Task<T> AssignAwbAsync<T>(long shipmentId, string? courierId = null, CancellationToken cancellationToken = default);
    Task<T> GeneratePickupAsync<T>(IReadOnlyCollection<long> shipmentIds, CancellationToken cancellationToken = default);
    Task<T> GenerateManifestAsync<T>(IReadOnlyCollection<long> shipmentIds, CancellationToken cancellationToken = default);
    Task<T> PrintManifestAsync<T>(IReadOnlyCollection<long> shipmentIds, CancellationToken cancellationToken = default);
    Task<T> GenerateLabelAsync<T>(IReadOnlyCollection<long> shipmentIds, CancellationToken cancellationToken = default);
    Task<T> PrintInvoiceAsync<T>(IReadOnlyCollection<long> orderIds, CancellationToken cancellationToken = default);
    Task<T> TrackAwbAsync<T>(string awbCode, CancellationToken cancellationToken = default);
}
