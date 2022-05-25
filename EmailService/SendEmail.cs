using System;
using System.Net;
using System.Net.Mail;

namespace EmailService
{
    public class SendEmail
    {
        private SmtpClient client = new SmtpClient()
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential()
            {
                UserName = "bybitbotapi@gmail.com",
                Password = "Mohammad1211"
            }
        };

        public SendEmail(string toEmailAddress,string subject,string body)
        {
            MailAddress FromEmail = new MailAddress("bybitbotapi@gmail.com", "ByBitBotApi");
            MailAddress ToEmail = new MailAddress(toEmailAddress);
            MailMessage mailMessage = new MailMessage()
            {
                From = FromEmail,
                Subject = subject,
                Body = body
            };
            mailMessage.To.Add(ToEmail);
            try
            {
                client.Send(mailMessage);

            }
            catch (Exception e)
            {

            }

        }
    }
}
