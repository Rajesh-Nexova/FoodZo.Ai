using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement;
using FoodZOAI.UserManagement.Mappers.Interfaces;

public class HalfYearlyReminderMapper : IHalfYearlyReminderMapper
{
    public List<HalfYearlyReminderDTO> ListMapToDTO(List<HalfYearlyReminder> domains)
    {
        return domains.Select(domain => new HalfYearlyReminderDTO
        {
            Id = domain.Id,
            ReminderId = domain.ReminderId,
            Day = domain.Day,
            Month = domain.Month,
            Quarter = domain.Quarter,
            IsActive = domain.IsActive
        }).ToList();
    }


    // Optional: Implement other methods later as needed
    public HalfYearlyReminderDTO Map(HalfYearlyReminder source)
        => throw new NotImplementedException();

    public List<HalfYearlyReminder> ListMapToDomain(List<HalfYearlyReminderDTO> dtoList)
        => throw new NotImplementedException();

    //public List<HalfYearlyReminder> ListMapToDTO(List<HalfYearlyReminder> domains)
    //    => throw new NotImplementedException();

    public HalfYearlyReminder MapToDomain(HalfYearlyReminderDTO dto)
        => throw new NotImplementedException();

    public HalfYearlyReminderDTO MapToDTO(HalfYearlyReminder domain)
        => throw new NotImplementedException();

    public List<HalfYearlyReminderDTO> MapList(List<HalfYearlyReminder> source)
    {
        throw new NotImplementedException();
    }
}
