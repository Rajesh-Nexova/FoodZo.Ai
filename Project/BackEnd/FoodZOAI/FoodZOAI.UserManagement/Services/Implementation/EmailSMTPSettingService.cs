using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Services.Contract;

namespace FoodZOAI.UserManagement.Services.Implementation
{
    public class EmailSMTPSettingService : IEmailSMTPSettingService
    {

        private readonly IEmailSettingRepository _emailSettingRepository;
        private readonly IEmailSettingMapper _emailSettingMapper;
        private readonly ILogger<EmailSMTPSettingService> _logger;

        public EmailSMTPSettingService(IEmailSettingRepository emailSettingRepository, IEmailSettingMapper emailSettingMapper, ILogger<EmailSMTPSettingService> logger)
        {
            _emailSettingRepository = emailSettingRepository;
            _emailSettingMapper = emailSettingMapper;
            _logger = logger;
        }


        public async Task<EmailSettingDTO> AddEmailSMTPSettingAsync(EmailSettingDTO dto)
        {
            try
            {
                var entity = _emailSettingMapper.MapToDomain(dto);
                entity.CreatedByUser = dto.CreatedByUser;

                await _emailSettingRepository.AddAsync(entity);

                return _emailSettingMapper.Map(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding Email SMTP setting.");
                throw;
            }
        }

        public async Task<bool> DeleteEmailSMTPSettingAsync(int id)
        {
            try
            {
                return await _emailSettingRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Email SMTP setting.");
                throw;
            }
        }

        public async Task<List<EmailSettingDTO>> GetAllEmailSMTPSettingAsync()
        {
            try
            {
                var list = await _emailSettingRepository.GetAllAsync();
                return _emailSettingMapper.MapList(list.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving all Email SMTP settings.");
                throw;
            }
        }

        public async Task<EmailSettingDTO?> GetEmailSMTPSettingByIdAsync(int id)
        {
            try
            {
                var entity = await _emailSettingRepository.GetByIdAsync(id);
                return entity != null ? _emailSettingMapper.Map(entity) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving Email SMTP setting by ID.");
                throw;
            }
        }

        public async Task<EmailSettingDTO?> UpdateEmailSMTPSettingAsync(int id, EmailSettingDTO dto)
        {
            try
            {
                var existing = await _emailSettingRepository.GetByIdAsync(id);
                if (existing == null)
                    return null;

                existing.Host = dto.Host;
                existing.Port = dto.Port;
                existing.UserName = dto.UserName;
                existing.Password = dto.Password;
                existing.IsEnableSsl = dto.IsEnableSsl;
                existing.IsDefault = dto.IsDefault;
                existing.IsActive = dto.IsActive;
                existing.ModifiedByUser = dto.ModifiedByUser;

                await _emailSettingRepository.UpdateAsync(existing);
                return _emailSettingMapper.Map(existing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating Email SMTP setting.");
                throw;
            }
        }

    }
}
