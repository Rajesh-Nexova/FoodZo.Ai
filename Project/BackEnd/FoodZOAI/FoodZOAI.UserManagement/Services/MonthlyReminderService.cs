// File: Services/Implementations/MonthlyReminderService.cs
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Mappers.Interfaces;
using FoodZOAI.UserManagement.Repositories;

public class MonthlyReminderService : IMonthlyReminderService
{
    private readonly IMonthlyReminderRepository _repository;
    private readonly IMonthlyReminderMapper _mapper;

    public MonthlyReminderService(IMonthlyReminderRepository repository, IMonthlyReminderMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MonthlyReminderDTO>> GetMonthlyRemindersAsync()
    {
        var reminders = await _repository.GetAllAsync();
        return _mapper.ListMapToDTO(reminders);
    }

    public async Task<MonthlyReminderDTO?> GetMonthlyReminderByIdAsync(int id) // ← Add this method
    {
        var reminder = await _repository.GetByIdAsync(id);
        return reminder == null ? null : _mapper.MapToDTO(reminder);
    }
}
