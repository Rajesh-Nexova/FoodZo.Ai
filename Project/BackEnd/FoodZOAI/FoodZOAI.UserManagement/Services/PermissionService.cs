using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.DTOs;
namespace FoodZOAI.UserManagement.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _repository;
        private readonly IPermissionMapper _mapper;
        private readonly ILogger<PermissionService> _logger;

        public PermissionService(IPermissionRepository repository, IPermissionMapper mapper, ILogger<PermissionService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<PermissionDTO>> GetPermissionsAsync()
        {
            var permissions = await _repository.GetAllAsync();
            return permissions.Select(_mapper.ToDTO);
        }

        public async Task<PermissionDTO?> GetPermissionByIdAsync(int id)
        {
            var permission = await _repository.GetByIdAsync(id);
            return permission != null ? _mapper.ToDTO(permission) : null;
        }

        public async Task AddPermissionAsync(PermissionDTO dto)
        {
            var entity = _mapper.ToEntity(dto);
            entity.CreatedAt = DateTime.UtcNow;
            await _repository.AddAsync(entity);
            _logger.LogInformation("Permission created: {Name}", entity.Name);
        }

        public async Task UpdatePermissionAsync(int id, PermissionDTO dto)
        {
            if (!await _repository.ExistsAsync(id))
                throw new KeyNotFoundException($"Permission with ID {id} not found.");

            var entity = _mapper.ToEntity(dto);
            entity.Id = id;
            entity.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(entity);
            _logger.LogInformation("Permission updated: {Id}", id);
        }

        public async Task DeletePermissionAsync(int id)
        {
            await _repository.DeleteAsync(id);
            _logger.LogInformation("Permission deleted: {Id}", id);
        }
    }

}
