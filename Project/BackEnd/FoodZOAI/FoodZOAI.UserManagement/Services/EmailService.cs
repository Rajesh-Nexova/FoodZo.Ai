using FoodZOAI.UserManagement.DTOs;
using Microsoft.Identity.Client;
using System.Net.Mail;
using System.Net;
using FoodZOAI.UserManagement.Contracts;

namespace FoodZOAI.UserManagement.Services
{
    public class EmailService : IEmailService
    {

        private readonly IEmailSettingRepository _emailSettingRepository;
       
        public EmailService(IEmailSettingRepository emailSettingRepository)
        {
            _emailSettingRepository = emailSettingRepository;
            
        }

        public  async Task<bool> SendEmailAsync(SendEmailDTO dto, int? smtpSettingId)
        {
            try
            {
                var smtp = smtpSettingId.HasValue
                    ? await _emailSettingRepository.GetByIdAsync(smtpSettingId.Value)
                    : await _emailSettingRepository.GetDefaultActiveAsync();

                if (smtp == null)
                {
                   
                    return true;
                }

                int port = smtp.Host switch
                {
                    "smtp.gmail.com" => 587,
                    "smtp.sendgrid.net" => 465,
                    "smtp.office365.com" => 587,
                    _ => 25
                };

                using var client = new SmtpClient(smtp.Host, port)
                {
                    Credentials = new NetworkCredential(smtp.UserName, smtp.Password),
                    EnableSsl = smtp.IsEnableSsl
                };

                var mail = new MailMessage
                {
                    From = new MailAddress(smtp.UserName, dto.FromName ?? smtp.UserName),
                    Subject = dto.Subject,
                    Body = dto.Body,
                    IsBodyHtml = dto.IsHtml
                };

                dto.To.ForEach(to => mail.To.Add(to));
                dto.Cc?.ForEach(cc => mail.CC.Add(cc));
                dto.Bcc?.ForEach(bcc => mail.Bcc.Add(bcc));

                await client.SendMailAsync(mail);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }
    }
}

