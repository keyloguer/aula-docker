using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using HackatonBtp.Domain.Models;
using Microsoft.Extensions.Options;

namespace HackatonBtp.Application.Email
{
    public class EmailSender : IEmailSender
    {
        private EmailOptions _emailOptions;


        public EmailSender(IOptions<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions.Value;

        }
        public bool SendEmail(EmailDTO email)
        {
            using (var mailMessage = new MailMessage())
            using (var client = new SmtpClient(_emailOptions.Host, _emailOptions.Port))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_emailOptions.Username, _emailOptions.Password);
                client.EnableSsl = _emailOptions.EnableSsl;

                mailMessage.From = new MailAddress(_emailOptions.Username);
                mailMessage.To.Insert(0, new MailAddress(email.Time.Email));
                mailMessage.Subject = _emailOptions.Subject;
                mailMessage.Body = BuildHtml(email);
                mailMessage.IsBodyHtml = true;

                try
                {
                    client.Send(mailMessage);
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }
        }

        private string BuildHtml(EmailDTO emailDto)
        {
            var builder = new StringBuilder();

            using (var reader = File.OpenText(emailDto.EmailTemplate))
            {
                builder.Append(reader.ReadToEnd());
            }
            builder.Replace("{{LinkConfirmacao}}", "https://" + emailDto.UrlEnvioEmail);
            builder.Replace("{{NomeTime}}", emailDto.Time.Nome);

            return builder.ToString();

        }
    }
}