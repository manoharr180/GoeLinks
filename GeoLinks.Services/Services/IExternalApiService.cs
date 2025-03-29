using System.Threading.Tasks;

namespace GeoLinks.Services.Services
{
    public interface IExternalApiService
    {
        Task<T> CreateAsync<T>(string url, object data);
        Task<T> ReadAsync<T>(string url);
        Task<T> UpdateAsync<T>(string url, object data);
        Task<T> DeleteAsync<T>(string url);
    }
}