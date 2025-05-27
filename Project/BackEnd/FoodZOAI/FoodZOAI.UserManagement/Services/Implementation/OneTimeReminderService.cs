using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Mappers.Interfaces;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repositories.Interfaces;
using FoodZOAI.UserManagement.Services.Interfaces;

namespace FoodZOAI.UserManagement.Services.Implementations
{
    public class OneTimeReminderService : IOneTimeReminderService
    {
        private readonly IOneTimeReminderRepository _repository;
        private readonly IOneTimeReminderMapper _mapper;

        public OneTimeReminderService(IOneTimeReminderRepository repository, IOneTimeReminderMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<OneTimeReminder>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<OneTimeReminder?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task AddAsync(OneTimeReminderDTO dto) => await _repository.AddAsync(_mapper.MapToDomain(dto));

        public async Task UpdateAsync(OneTimeReminderDTO dto) => await _repository.UpdateAsync(_mapper.MapToDomain(dto));

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

        Task<List<OneTimeReminderDTO>> IOneTimeReminderService.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<OneTimeReminderDTO> IOneTimeReminderService.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OneTimeReminderDTO> CreateAsync(OneTimeReminderDTO dto)
        {
            throw new NotImplementedException();
        }

        Task<OneTimeReminderDTO> IOneTimeReminderService.UpdateAsync(OneTimeReminderDTO dto)
        {
            throw new NotImplementedException();
        }

        Task<bool> IOneTimeReminderService.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}