using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Repositories
{
    public class WeeklyReminderRepository : IWeeklyReminderRepository
    {
        private readonly List<WeeklyReminder> _reminders = new()
        {
            new WeeklyReminder
            {
                Id = 1,
                Subject = "Weekly Meeting",
                Message = "Join weekly sync",
                Frequency = "Weekly",
                StartDate = DateTime.Now,
                DayOfWeek = "Monday",
                IsRepeated = true,
                IsEmailNotification = true,
                CreatedAt = DateTime.Now,
                IsActive = true,
                CreatedByUser = "admin"
            }
        };

        public Task<IEnumerable<WeeklyReminder>> GetAllAsync()
        {
            return Task.FromResult(_reminders.Where(r => r.IsActive).AsEnumerable());
        }

        public Task<WeeklyReminder?> GetByIdAsync(int id)
        {
            var reminder = _reminders.FirstOrDefault(r => r.Id == id && r.IsActive);
            return Task.FromResult(reminder);
        }
    }
}
