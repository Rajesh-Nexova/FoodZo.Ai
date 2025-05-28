using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Mappers
{
    public class AppsettingMapper : IAppsettingMapper
	{
		public List<Appsetting> ListMapToDomain(List<AppsettingDTO> dtoList)
		{
			return dtoList?.Select(MapToDomain).ToList() ?? [];
		}

		public AppsettingDTO Map(Appsetting source)
        {
            if(source == null) throw new ArgumentNullException("Appsetting");

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
			return source?.Select(Map).ToList() ?? [];
		}

		public Appsetting MapToDomain(AppsettingDTO dto)
		{
            if (dto == null) throw new ArgumentNullException("Appsetting DTO");

            return new Appsetting
            {
                CreatedByUser = dto.CreatedByUser,
                ModifiedByUser = dto.ModifiedByUser,
                DeletedByUser = dto.DeletedByUser,
                IsActive = dto.IsActive,
                Id = dto.Id,
                Name = dto.Name,
                Value = dto.Value,
                Key = dto.Key
            };
		}
	}
}
