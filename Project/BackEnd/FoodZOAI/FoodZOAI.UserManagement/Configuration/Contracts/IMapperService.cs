using System.Collections.Generic;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IMapperService<TSource, TDestination>
    {
        TDestination Map(TSource source);
        List<TDestination> MapList(List<TSource> source);

        TSource MapToEntity(TDestination dto, TSource existingEntity);
        TSource MapToEntity(TDestination dto); // ✅ Add this method
    }
}
