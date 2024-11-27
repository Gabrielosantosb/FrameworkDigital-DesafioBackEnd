using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using FrameworkDigital_DesafioBackEnd.ORM.Entity.EmailSettings;
using FrameworkDigital_DesafioBackEnd.ORM.Entity.Lead;

namespace FrameworkDigital_DesafioBackEnd.Application.Email
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;
        
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        
        public void SendEmail(LeadModel lead)
        {

            if (_emailSettings == null)
            {
                throw new InvalidOperationException("Email settings not configured.");
            }

            //  objeto MailMessage
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.Email, "Framework Digital"),
                Subject = "Alteração no Status da Lead - Status Aceito",
                Body = $@"
                    <h1>Notificação de Status de Lead</h1>
                    <p><strong>Status da Lead:</strong> {lead.Status}</p>
                    <p><strong>Nome da Lead:</strong> {lead.ContactFirstName}</p>
                    <p><strong>Valor Original:</strong> {lead.Price:C}</p>
                    <p><strong>Desconto Aplicado:</strong> {lead.Price * 0.10m:C}</p>
                    <p><strong>Valor Final Após Desconto:</strong> {lead.Price:C}</p>",
                IsBodyHtml = true
            };

            //  e-mail de destino
            //mailMessage.To.Add(lead.ContactEmail);
            mailMessage.To.Add("gabrielosantosb@gmail.com");

            // configuração do cliente SMTP
            var smtpClient = new SmtpClient(_emailSettings.SmtpHost, _emailSettings.SmtpPort)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password)
            };

            // envia o e-mail
            smtpClient.Send(mailMessage);
        }
    }
}
