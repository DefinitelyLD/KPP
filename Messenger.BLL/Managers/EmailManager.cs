using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Messenger.BLL.Managers.Interfaces;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Messenger.BLL
{
    public class EmailManager : IEmailManager
    {
        
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration config = builder.Build();
            
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Admin", config.GetValue<string>("Smtp:MailFrom")));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(config.GetValue<string>("Smtp:Server"), config.GetValue<int>("Smtp:Port"), true);
                await client.AuthenticateAsync(config.GetValue<string>("Smtp:Username"), config.GetValue<string>("Smtp:Password"));
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }

        public string RegistrationMessageTemplate(string userName, string url)
        {
            return $"Hello, {userName}. Click the following link to confirm your registration: <a href='{url}'>link</a>";
        }
    }
}
