using System.Collections.Generic;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IMapperService<TSource, TDestination>
    {
        UserDTO MapToDTO(User entity);

        TDestination Map(TSource source);
        List<TDestination> MapList(List<TSource> source);

        TSource MapToEntity(TDestination dto, TSource existingEntity);
        TSource MapToEntity(TDestination dto); // ✅ Add this method
        void MapToDTOList(List<User> users);
    }
}
