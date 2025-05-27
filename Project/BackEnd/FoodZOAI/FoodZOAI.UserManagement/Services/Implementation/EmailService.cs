using FoodZOAI.UserManagement.DTOs;
using Microsoft.Identity.Client;
using System.Net.Mail;
using System.Net;
using FoodZOAI.UserManagement.Contracts;

namespace FoodZOAI.UserManagement.Services.Implementation
{
    public class EmailService : IEmailService
    {

        private readonly IEmailSettingRepository _emailSettingRepository;
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IEmailSettingRepository emailSettingRepository, IEmailTemplateRepository emailTemplateRepository, ILogger<EmailService> logger)
        {
            _emailSettingRepository = emailSettingRepository;
            _emailTemplateRepository = emailTemplateRepository;
            _logger = logger;
            
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
                    _logger.LogError("No SMTP settings available.");
                    return false;
                }

                if (dto.TemplateId.HasValue)
                {
                    var template = await _emailTemplateRepository.GetTemplateByIdAsync(dto.TemplateId.Value);
                    if (template != null)
                    {
                        dto.Subject = template.Subject;
                        dto.Body = template.Body;
                    }
                }

                using var client = new SmtpClient(smtp.Host, smtp.Port)
                {
                    UseDefaultCredentials = false,

                    Credentials = new NetworkCredential(smtp.UserName, smtp.Password),
                    EnableSsl = smtp.IsEnableSsl
                };

                var mail = new MailMessage
                {
                    //From = new MailAddress(smtp.UserName, dto.FromName ?? smtp.UserName),
                    From = new MailAddress(smtp.UserName, "Lavanya"),

                    Subject = dto.Subject,
                    Body = dto.Body,
                    IsBodyHtml = dto.IsHtml
                };

                dto.To.ForEach(to => mail.To.Add(to));
                dto.Cc?.ForEach(cc => mail.CC.Add(cc));
                dto.Bcc?.ForEach(bcc => mail.Bcc.Add(bcc));


                //Attachment
                if (dto.Attachments != null)
                {
                    foreach (var file in dto.Attachments)
                    {
                        using var ms = new MemoryStream();
                        await file.CopyToAsync(ms);
                        mail.Attachments.Add(new Attachment(new MemoryStream(ms.ToArray()), file.FileName));
                    }
                }



                await client.SendMailAsync(mail);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email.");

                if (ex.InnerException != null)
                {
                    _logger.LogError("Inner Exception: {Message}", ex.InnerException.Message);
                }

                Console.WriteLine("Exception Message: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }

                return false;
            }

        }
    }
}

