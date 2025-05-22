using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Configuration.Contracts;

namespace FoodZOAI.UserManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapperService<User, UserDTO> _mapper;

        public UserService(IUserRepository userRepository, IMapperService<User, UserDTO> mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> AddUserAsync(UserDTO userDto)
        {
            var user = _mapper.MapToEntity(userDto);
            var result = await _userRepository.AddUserAsync(user);
            return _mapper.Map(result);
        }

        public async Task<UserDTO?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return user == null ? null : _mapper.Map(user);
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(_mapper.Map);
        }

        public async Task<IEnumerable<UserDTO>> GetPaginatedUsersAsync(int pageNumber, int pageSize)
        {
            var users = await _userRepository.GetPaginatedUsersAsync(pageNumber, pageSize);
            return users.Select(_mapper.Map);
        }

        public async Task<UserDTO?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            return user == null ? null : _mapper.Map(user);
        }

        public async Task<UserDTO?> GetUserByUsernameAsync(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            return user == null ? null : _mapper.Map(user);
        }

        public async Task<UserDTO> UpdateUserAsync(UserDTO userDto)
        {
            var existingUser = await _userRepository.GetByIdAsync(userDto.Id);
            var updatedUser = _mapper.MapToEntity(userDto, existingUser);
            var result = await _userRepository.UpdateUserAsync(updatedUser);
            return _mapper.Map(result);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteUserAsync(id);
        }

        public async Task<bool> ChangePasswordAsync(int userId, string newPasswordHash, string newSalt)
        {
            return await _userRepository.ChangePasswordAsync(userId, newPasswordHash, newSalt);
        }

        public async Task<IEnumerable<UserDTO>> GetRecentlyRegisteredUsersAsync(int count)
        {
            var users = await _userRepository.GetRecentlyRegisteredUsersAsync(count);
            return users.Select(_mapper.Map);
        }

        public async Task<int> GetTotalUserCountAsync()
        {
            return await _userRepository.GetTotalUserCountAsync();
        }

        public async Task<IEnumerable<UserDTO>> GetUsersRegisteredAfterAsync(DateTime fromDate)
        {
            var users = await _userRepository.GetUsersRegisteredAfterAsync(fromDate);
            return users.Select(_mapper.Map);
        }
    }
}
