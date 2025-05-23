using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodZOAI.UserManagement.Repository
{
    public class EmailTemplateRepository : IEmailTemplateRepository
    {
        public readonly FoodZoaiContext _Context;
        public EmailTemplateRepository(FoodZoaiContext Context)
        {
            _Context = Context;
        }
        public  async Task AddAsync(EmailTemplate emailTemplate)
        {
            _Context.EmailTemplates.Add(emailTemplate);
            await _Context.SaveChangesAsync();
        }

        public  async Task DeleteAsync(int id)
        {
            var email = await _Context.EmailTemplates.FindAsync(id);
            if (email != null)
            {
                _Context.EmailTemplates.Remove(email);
                await _Context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _Context.EmailTemplates.AnyAsync(e => e.Id == id);
        }

        public  async Task<IEnumerable<EmailTemplate>> GetAllAsync()
        {
            return await _Context.EmailTemplates.ToListAsync();
        }

        public async Task<EmailTemplate?> GetByIdAsync(int id)
        {
            return await _Context.EmailTemplates.FindAsync(id);
        }

        

        public  async Task UpdateAsync(EmailTemplate EmailTemplates)
        {
            _Context.EmailTemplates.Update(EmailTemplates);
            await _Context.SaveChangesAsync();
        }

        public  async Task<EmailTemplateDTO?> GetTemplateByIdAsync(int id)
        {
            var template = await _Context.EmailTemplates.FirstOrDefaultAsync(t => t.Id == id && t.IsActive);
            return template == null ? null : new EmailTemplateDTO
            {
                Id = template.Id,
                Name = template.Name,
                Subject = template.Subject,
                Body = template.Body,
                IsActive = template.IsActive,
                CreatedByUser = template.CreatedByUser
            };
        }



    }
}
