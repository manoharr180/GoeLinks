using System;
using System.Threading.Tasks;
using GeoLinks.Services.Services;

public class EmailService : IEmailService
{
    public Task SendAsync(string toEmail, string subject, string body)
    {
        // Implement email sending logic here (e.g., using SMTP or an email service provider)
        throw new NotImplementedException();
    }
}