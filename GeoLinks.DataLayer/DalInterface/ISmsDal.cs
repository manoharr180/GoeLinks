using System.Threading.Tasks;

public interface ISmsDal
{
    Task SendAsync(string toPhoneNumber, string message);
}