using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Configuration.Mappers;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Repository;
using FoodZOAI.UserManagement.Services.Contract;

namespace FoodZOAI.UserManagement.Services.Implementation
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationMapper _organizationMapper;
        private readonly IOrganizationRepository _organizationRepository;



        public OrganizationService(IOrganizationMapper organizationMapper, IOrganizationRepository organizationRepository)
        {
            _organizationMapper = organizationMapper;
            _organizationRepository = organizationRepository;
        }
        public async Task<OrganizationDTO?> GetOrganizationByIdAsync(int id)
        {

            try
            {
                var entity = await _organizationRepository.GetByIdAsync(id);
                return entity != null ? _organizationMapper.Map(entity) : null;
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex, "Error while retrieving Email Template by ID.");
                throw;
            }
        }

        

        public  async Task<OrganizationDTO?> UpdateOrganizationAsync(int id, OrganizationDTO dto)
        {
            try
            {
                var existing = await _organizationRepository.GetByIdAsync(id);
                if (existing == null)
                    return null;

                existing.Id = dto.Id;
                existing.Name = dto.Name;
                existing.Slug = dto.Slug;
                existing.Description = dto.Description;

                existing.Website = dto.Website;
                existing.LogoUrl = dto.LogoUrl;
                existing.SubscriptionPlan = dto.SubscriptionPlan;
                existing.MaxUsers = dto.MaxUsers;

                existing.Status = dto.Status;
                existing.CreatedAt = dto.CreatedAt;
                existing.UpdatedAt = dto.UpdatedAt;
                existing.DeletedAt = dto.DeletedAt;


                await _organizationRepository.UpdateAsync(existing);
                return _organizationMapper.Map(existing);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error while updating EmailTemplate.");
                throw;
            }
        }
    }
}
