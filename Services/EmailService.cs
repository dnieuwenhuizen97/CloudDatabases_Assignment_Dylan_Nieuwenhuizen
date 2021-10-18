using SendGrid;
using SendGrid.Helpers.Mail;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmailService : IEmailService
    {
        public async Task SendMortgageEmail(string emailAddress, string blobUrl)
        {
            try
            {
                //TODO: Make SendGrid API-KEY not hardcoded
                var client = new SendGridClient(Environment.GetEnvironmentVariable("SendGridMailClient"));
                var from = new EmailAddress(Environment.GetEnvironmentVariable("SendGridEmailAddress"), "BuyMyHouse Mortgages");
                var subject = "Your personal mortgage offer";
                var to = new EmailAddress(emailAddress, "");
                var plainTextContent = "Thank you for your interest in BuyMyHouse. Through this link you can view your personal mortgage offer";
                var htmlContent = $"<div><strong>Thank you for your interest in BuyMyHouse.</strong><br>" +
                                    $"<p>Through <a href={blobUrl}>this link</a> you can view your personal mortgage offer.</p></div>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
