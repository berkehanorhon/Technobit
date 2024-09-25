namespace TechnoBit.Interfaces;

public interface IEmailService
{
    Task SendWelcomeEmail(string toEmail, string toUsername);
}