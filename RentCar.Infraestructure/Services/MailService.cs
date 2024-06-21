using Diversos.Core.Interfaces;
using Diversos.Core.Models.Email;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Diversos.Infraestructure.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                var client = new SmtpClient()
                {
                    EnableSsl = _mailSettings.SSL,
                    UseDefaultCredentials = _mailSettings.DefaultCredentials,
                    Credentials = new NetworkCredential(_mailSettings.Email, _mailSettings.Password),
                    Host = _mailSettings.Host,
                    Port = _mailSettings.Port,
                };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                    | SecurityProtocolType.Tls11
                    | SecurityProtocolType.Tls12
                    | SecurityProtocolType.Tls13;


                // Create email message
                MailMessage mailMessage = new()
                {
                    From = new MailAddress(_mailSettings.Email)
                };
                mailMessage.To.Add(mailRequest.ToEmail);
                mailMessage.Subject = mailRequest.Subject;
                mailMessage.IsBodyHtml = true;
                //var builder = new BodyBuilder();
                //if (mailRequest.Attachments != null)
                //{
                //    byte[] fileBytes;
                //    foreach (var file in mailRequest.Attachments)
                //    {
                //        if (file.Length > 0)
                //        {
                //            using (var ms = new MemoryStream())
                //            {
                //                file.CopyTo(ms);
                //                fileBytes = ms.ToArray();
                //            }
                //            builder.Attachments.Add(file.FileName, fileBytes, MimeKit.ContentType.Parse(file.ContentType));
                //        }
                //    }
                //}
                //builder.HtmlBody = mailRequest.Body;
                //mailMessage.Body = builder.ToString();
                mailMessage.Body = mailRequest.Body;

                // Send email
                await Task.Run(() => client.Send(mailMessage));
            }
            catch (Exception err) { }
        }
    }
}
