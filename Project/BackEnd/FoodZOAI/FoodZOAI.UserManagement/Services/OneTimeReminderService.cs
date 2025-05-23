using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Services
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

        public async Task<List<OneTimeReminderDTO>> GetOneTimeRemindersAsync()
        {
            var reminders = await _repository.GetAllAsync();
            return reminders
                .Where(r => r.IsActive)
                .Select(r => _mapper.MapToDTO(r))
                .ToList();
        }
    }
}
