using System.Threading.Tasks;

namespace GeoLinks.Services.Services
{
    public interface ISmsService
    {
        Task SendAsync(string toPhoneNumber, string message);
    }
}