using System;


namespace Application.Repositories.Abstraction
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}

