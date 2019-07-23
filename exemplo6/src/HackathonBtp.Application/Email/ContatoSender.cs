using HackatonBtp.Domain.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace HackathonBtp.Application.Email
{
    public class ContatoSender : IContantoEmailSender
    {
        private EmailOptions _emailOptions;

        public ContatoSender(IOptions<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions.Value;
        }

        public bool EnviarEmail(string nome, string email, string mensagem)
        {
            using (var mailMessage = new MailMessage())
            using (var client = new SmtpClient(_emailOptions.Host, _emailOptions.Port))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_emailOptions.Username, _emailOptions.Password);
                client.EnableSsl = _emailOptions.EnableSsl;

                mailMessage.From = new MailAddress(_emailOptions.Username);
                mailMessage.To.Insert(0, new MailAddress(_emailOptions.Username));
                mailMessage.Subject = nome + " - " + email;
                mailMessage.Body = mensagem;

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
    }
}
