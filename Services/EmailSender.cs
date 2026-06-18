using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _config;

    public EmailSender(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

        if (string.IsNullOrEmpty(apiKey))
        {
            throw new Exception("SendGrid API Key is missing!");
        }

        var client = new SendGridClient(apiKey);

        var from = new EmailAddress("eduv13135018@vossie.net", "OakDevelopments");
        var to = new EmailAddress(email);


        var plainTextContent = $"Please confirm your account: {htmlMessage}";

        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlMessage);


        //await client.SendEmailAsync(msg);


        var response = await client.SendEmailAsync(msg);

        var responseBody = await response.Body.ReadAsStringAsync();

        Console.WriteLine(response.StatusCode);
        Console.WriteLine(responseBody);

    }
}