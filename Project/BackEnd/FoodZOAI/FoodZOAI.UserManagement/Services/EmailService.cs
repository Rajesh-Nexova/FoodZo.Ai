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
        public async Task<bool> SendEmailAsync(SendEmailDTO emailDto)
        {
            try
            {
                var setting = await _emailSettingRepository.GetActiveEmailSettingAsync();
                if (setting == null)
                    throw new InvalidOperationException("No active email setting found.");

                var smtpClient = new SmtpClient(setting.Host)
                {
                    Port = 587, // Optional: make port part of DTO
                    Credentials = new NetworkCredential(setting.UserName, setting.Password),
                    EnableSsl = setting.IsEnableSsl
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(setting.UserName, emailDto.FromName ?? "System"),
                    Subject = emailDto.Subject,
                    Body = emailDto.Body,
                    IsBodyHtml = emailDto.IsHtml
                };

                emailDto.To?.ForEach(to => mailMessage.To.Add(to));
                emailDto.Cc?.ForEach(cc => mailMessage.CC.Add(cc));
                emailDto.Bcc?.ForEach(bcc => mailMessage.Bcc.Add(bcc));

                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // Log exception here
                return false;
            }
        }
    }
}

