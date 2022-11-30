using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace Infrastructure.Services
{
    public class SendGridEmailSender : IEmailSender
    {
        
        private readonly ILogger _logger;

        public SendGridEmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor
            , ILogger<SendGridEmailSender> logger)
        {
            Options = optionsAccessor.Value;
            this._logger = logger;
        }
        public AuthMessageSenderOptions Options { get; }


        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            if (string.IsNullOrEmpty(Options.SendGridApiKey))
            {
                throw new Exception("The 'SendGridApiKey' is not configured");
            }

            var client = new SendGridClient(Options.SendGridApiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("nshivakhanova@gmail.com"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(toEmail));

            var response = await client.SendEmailAsync(msg);
            _logger.LogInformation(response.IsSuccessStatusCode
                               ? $"Email to {toEmail} queued successfully!"
                               : $"Failure Email to {toEmail}");
        }
    }
}

