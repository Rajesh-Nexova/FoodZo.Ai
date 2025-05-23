using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Mappers
{
    public class AppsettingMapper : IAppsetting
    {
        public AppsettingDTO Map(Appsetting source)
        {
            return new AppsettingDTO
            {
                Id = source.Id,
                Name = source.Name,
                Key = source.Key,
                Value = source.Value,
                CreatedByUser = source.CreatedByUser,
                ModifiedByUser = source.ModifiedByUser,
                DeletedByUser = source.DeletedByUser,
                IsActive = source.IsActive
            };
        }

        public List<AppsettingDTO> MapList(List<Appsetting> source)
        {
            return source.Select(Map).ToList();
        }
    }
}
