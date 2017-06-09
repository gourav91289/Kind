
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using Microsoft.Extensions.Logging;

namespace OmniPot.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private readonly ILogger logger; 
        private readonly string twilioAccountSid;
        private readonly string twilioAuthToken;
        //TODO: Move into an options object
        private const string LocalDomain = "rimmptags.com";
        private const string SmtpHostAddress = "outlook.office365.com";
        private const string FromAddress = "mail@rimmptags.com";
        private const string FromDisplayName = "Postmaster";
        private const string EmailFooter = @"

Thank You!

This is an official State of Rhode Island communication delivered by the Department of Business Regulation's Medical Marijuana Program.

For help or questions with this communication, please email support@rimmptags.com, call (401) 462-9552, or send us a message at https://kindfinancial.wufoo.com/forms/m12f4r9c1pvz5ok/   ";


        //https://kindfinancial.wufoo.com/forms/m12f4r9c1pvz5ok/
        public AuthMessageSender(IOptions<TwilioOptions> options, ILoggerFactory loggerFactory)
        {
            twilioAccountSid = options.Value.AccountSid;
            twilioAuthToken = options.Value.AuthToken;
            logger = loggerFactory.CreateLogger<AuthMessageSender>();
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            logger.LogDebug("Entering SendEmailAsync"); 
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(FromDisplayName, FromAddress));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message + EmailFooter };

            using (var client = new SmtpClient())
            {
                client.LocalDomain = LocalDomain;
                // Note: only needed if the SMTP server requires authentication
                await client.ConnectAsync(SmtpHostAddress, 587, SecureSocketOptions.Auto).ConfigureAwait(false);
                 client.Authenticate("mail@rimmptags.com", "W33d4$ale!");
               
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
            logger.LogDebug($"Email message with subject {subject} sent to {email}.");
        }

        public Task SendSmsAsync(string number, string message)
        {           
            return Task.FromResult(0);
        }
    }

    public class TwilioOptions
    {
        public string AccountSid { get; set; } = "sGc0SNtn4IDz3MAqiY60d8I85iq9hCZD";
        public string AuthToken { get; set; } = "";
    }
}
