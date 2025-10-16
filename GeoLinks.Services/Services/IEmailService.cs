using System.Threading.Tasks;

namespace GeoLinks.Services.Services
{
    public interface IEmailService
    {
        Task SendAsync(string toEmail, string subject, string body);
    }
}