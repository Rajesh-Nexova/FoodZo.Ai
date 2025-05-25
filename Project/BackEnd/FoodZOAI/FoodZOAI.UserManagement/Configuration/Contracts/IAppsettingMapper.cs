using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IAppsettingMapper : IMapperServices<Appsetting, AppsettingDTO>
    {
        Appsetting MapToDomain(AppsettingDTO dto);
        List<Appsetting> ListMapToDomain(List<AppsettingDTO> dtoList);
    }
}
