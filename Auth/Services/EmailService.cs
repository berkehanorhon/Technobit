using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
using TechnoBit.Interfaces;

public class SmtpSettings
{
    public string Server { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string FromEmail { get; set; }
    public string FromName { get; set; }
}

public class EmailService : IEmailService
{
    private readonly SmtpSettings _smtpSettings;

    public EmailService(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }

    public async Task SendWelcomeEmail(string toEmail, string toUsername)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpSettings.FromName, _smtpSettings.FromEmail));
            message.To.Add(new MailboxAddress(toUsername, toEmail));
            message.Subject = "Welcome to Our Platform!";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = string.Format(@"
                    <html>
                    <head>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;
                                margin: 0;
                                padding: 20px;
                            }}
                            .container {{
                                max-width: 600px;
                                margin: auto;
                                background: #ffffff;
                                border-radius: 8px;
                                padding: 20px;
                                box-shadow: 0 2px 10px rgba(0,0,0,0.1);
                            }}
                            h1 {{
                                color: #333333;
                                text-align: center;
                            }}
                            p {{
                                line-height: 1.5;
                                color: #555555;
                            }}
                            .footer {{
                                text-align: center;
                                margin-top: 20px;
                                font-size: 12px;
                                color: #aaaaaa;
                            }}
                            a {{
                                color: #007BFF;
                                text-decoration: none;
                            }}
                            a:hover {{
                                text-decoration: underline;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <h1>Welcome {0}!</h1>
                            <p>Thank you for signing up on our platform. We are thrilled to have you with us!</p>
                            <p>To get started, feel free to explore our features and let us know if you have any questions.</p>
                            <p>If you wish to unsubscribe, please click <a href='https://mrpostman.com.tr/unsubscribe'>here</a>.</p>
                            <p>Best wishes,<br/>E-Shopping System Team</p>
                        </div>
                        <div class='footer'>
                            <p>&copy; {1} E-Shopping System. All rights reserved.</p>
                        </div>
                    </body>
                    </html>", toUsername, DateTime.Now.Year)
            };

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

            //Console.WriteLine("Welcome email sent successfully!");
        }
        catch (Exception e)
        {
            //Console.WriteLine(e);
            //throw;
        }
    }
}