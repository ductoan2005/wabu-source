using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
namespace FW.Common.Helpers
{
    public static class CommonEmails
    {
        public static async Task<bool> SendEmailCompany(string email,string contentHtml, string subject)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(ConfigurationManager.AppSettings["email"]);
            msg.To.Add(email);
            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = contentHtml;

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = true;
            client.Host = ConfigurationManager.AppSettings["host"];
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["email"], ConfigurationManager.AppSettings["password"]);
            client.Timeout = 20000;
            try
            {
                await client.SendMailAsync(msg);
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
            finally
            {
                msg.Dispose();
            }
        }
    }
}
