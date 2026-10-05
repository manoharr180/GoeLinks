namespace GeoLinks.ApiProxies.ShipRocket;

public sealed class ShipRocketOptions
{
    public const string SectionName = "ShipRocket";
    public string BaseUrl { get; set; } = "https://apiv2.shiprocket.in/v1/external/";
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
