using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Services.Contract;

namespace FoodZOAI.UserManagement.Services.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleMapper _roleMapper;

        public RoleService(IRoleRepository roleRepository, IRoleMapper roleMapper)
        {
            _roleRepository = roleRepository;
            _roleMapper = roleMapper;
        }

        public async Task<List<RoleDTO>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            return _roleMapper.MapList(roles);
        }

        public async Task<RoleDTO> UpdateRoleAsync(RoleDTO roleDto)
        {
            var existingRole = await _roleRepository.GetByIdAsync(roleDto.Id);
            if (existingRole == null)
                throw new KeyNotFoundException($"Role with ID {roleDto.Id} not found.");

            var updatedEntity = _roleMapper.MapToEntity(roleDto, existingRole);
            var updatedRole = await _roleRepository.UpdateAsync(updatedEntity);
            return _roleMapper.Map(updatedRole);
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var existingRole = await _roleRepository.GetByIdAsync(id);
            if (existingRole == null)
                return false;

            return await _roleRepository.DeleteAsync(id);
        }
    }
}
