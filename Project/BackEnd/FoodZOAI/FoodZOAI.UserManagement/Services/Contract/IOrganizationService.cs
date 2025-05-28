using FoodZOAI.UserManagement.DTOs;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Services.Contract
{
    public interface IOrganizationService
    {
    
        Task<OrganizationDTO?> GetOrganizationByIdAsync(int id);
        
        Task<OrganizationDTO?> UpdateOrganizationAsync(int id, OrganizationDTO dto);
        
    }
}
