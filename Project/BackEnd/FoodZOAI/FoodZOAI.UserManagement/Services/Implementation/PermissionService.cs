using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repository;
using FoodZOAI.UserManagement.Services.Contract;

namespace FoodZOAI.UserManagement.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _repository;
        private readonly IPermissionMapper _mapper;

        public PermissionService(IPermissionRepository repository, IPermissionMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Existing Methods

        public async Task<List<PermissionDTO>> GetAllAsync()
        {
            var permissions = await _repository.GetAllAsync();
            return _mapper.MapList(permissions);
        }

        public async Task<PermissionDTO?> GetByIdAsync(int id)
        {
            var permission = await _repository.GetByIdAsync(id);
            if (permission == null)
                return null;

            return _mapper.Map(permission);
        }

        public async Task<PermissionDTO> AddAsync(PermissionDTO dto)
        {
            var domain = _mapper.MapToDomain(dto);
            var result = await _repository.AddAsync(domain);
            return _mapper.Map(result);
        }

        public async Task<PermissionDTO> UpdateAsync(PermissionDTO dto)
        {
            var domain = _mapper.MapToDomain(dto);
            var result = await _repository.UpdateAsync(domain);
            return _mapper.Map(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        // New Methods

        public async Task<IEnumerable<PermissionDTO>> GetPermissionsAsync()
        {
            var permissions = await _repository.GetAllAsync();
            return _mapper.MapList(permissions);
        }

        public async Task<PermissionDTO?> GetPermissionByIdAsync(int id)
        {
            var permission = await _repository.GetByIdAsync(id);
            return permission == null ? null : _mapper.Map(permission);
        }

        public async Task AddPermissionAsync(PermissionDTO dto)
        {
            var permission = _mapper.MapToDomain(dto);
            await _repository.AddAsync(permission);
        }

        public async Task UpdatePermissionAsync(int id, PermissionDTO dto)
        {
            var permission = _mapper.MapToDomain(dto);
            permission.Id = id; // Ensure correct ID is set
            await _repository.UpdateAsync(permission);
        }

        public async Task DeletePermissionAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
