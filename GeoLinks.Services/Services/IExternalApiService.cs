using System.Threading.Tasks;

namespace GeoLinks.Services.Services
{
    public interface IExternalApiService
    {
        Task<string> CreateAsync(string url, object data);
        Task<string> ReadAsync(string url);
        Task<string> UpdateAsync(string url, object data);
        Task<string> DeleteAsync(string url);
    }
}