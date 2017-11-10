using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace IdSP.Core.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var myMessage = new MailMessage();

                myMessage.To.Add(email);

                myMessage.From = new MailAddress("ale911115@gmail.com", "Rapido Etecsa IdSP");

                myMessage.Subject = subject;

                myMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message, null, MediaTypeNames.Text.Plain));
                myMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message, null, MediaTypeNames.Text.Html));

                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("ale911115@gmail.com", "ogktyjacafrixwyi")
                };

                client.Send(myMessage);

                return Task.FromResult(0);
            }
            catch (Exception x)
            {

                throw x;
            }
        }
    }
}
