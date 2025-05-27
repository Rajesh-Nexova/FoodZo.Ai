using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Services.Contract;

namespace FoodZOAI.UserManagement.Services.Implementation
{
    public class EmailTemplateService : IEmailTemplateService
    {

        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IEmailTemplateMapper _emailTemplateMapper;
        private readonly ILogger<EmailTemplateService> _logger;

        public EmailTemplateService(IEmailTemplateRepository emailTemplateRepository, IEmailTemplateMapper emailTemplateMapper, ILogger<EmailTemplateService> logger)
        {
            _emailTemplateRepository = emailTemplateRepository;
            _emailTemplateMapper = emailTemplateMapper;
            _logger = logger;
        }




        public async Task<EmailTemplateDTO> AddEmailTemplateAsync(EmailTemplateDTO dto)
        {
            try
            {
                var entity = _emailTemplateMapper.MapToDomain(dto);
                entity.CreatedByUser = dto.CreatedByUser;

                await _emailTemplateRepository.AddAsync(entity);

                return _emailTemplateMapper.Map(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding Email Template.");
                throw;
            }
        }
        public async Task<bool> DeleteEmailTemplateAsync(int id)
        {
            try
            {
                return await _emailTemplateRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Email Template.");
                throw;
            }
        }

        public async Task<List<EmailTemplateDTO>> GetAllEmailTemplateAsync()
        {
            try
            {
                var list = await _emailTemplateRepository.GetAllAsync();
                return _emailTemplateMapper.MapList(list.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving all Email Template.");
                throw;
            }
        }

        public async Task<EmailTemplateDTO?> GetEmailTemplateByIdAsync(int id)
        {
            try
            {
                var entity = await _emailTemplateRepository.GetByIdAsync(id);
                return entity != null ? _emailTemplateMapper.Map(entity) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving Email Template by ID.");
                throw;
            }
        }



        public async Task<EmailTemplateDTO?> UpdateEmailTemplateAsync(int id, EmailTemplateDTO dto)
        {
            try
            {
                var existing = await _emailTemplateRepository.GetByIdAsync(id);
                if (existing == null)
                    return null;

                existing.Name = dto.Name;
                existing.Subject = dto.Subject;
                existing.Body = dto.Body;
                existing.IsActive = dto.IsActive;
                existing.ModifiedByUser = dto.ModifiedByUser;


                await _emailTemplateRepository.UpdateAsync(existing);
                return _emailTemplateMapper.Map(existing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating EmailTemplate.");
                throw;
            }
        }
    }
}
