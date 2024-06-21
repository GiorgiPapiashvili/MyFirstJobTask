using FluentEmail.Core;
using FluentEmail.Smtp;
using System.Net;
using System.Net.Mail;

namespace MyFirstJobProject.Services
{
    public class EmailService
    {
        private readonly string? _fromAddress;
        private readonly string _password;

        public EmailService()
        {
            _fromAddress = "GiorgioPapiashvili77@Gmail.Com";
            _password = "Nugaaxure1";
        }

        public void SendEmailAsync(string toAddress, string subject, string body)
        {
            var sender = new SmtpSender(() => new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_fromAddress, _password),
            });

            Email.DefaultSender = sender;

            Email
           .From(_fromAddress)
           .To(toAddress)
           .Subject(subject)
           .Body(body)
           .Send();
        }
    }
}
