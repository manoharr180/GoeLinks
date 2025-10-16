using System.Threading.Tasks;

public interface IEmailDal
{
    Task SendAsync(string toEmail, string subject, string body);
}