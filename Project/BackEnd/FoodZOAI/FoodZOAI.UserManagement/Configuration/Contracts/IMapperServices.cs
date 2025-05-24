using System.Collections.Generic;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IMapperServices<TSource, TDestination>
    {
        TDestination Map(TSource source);

        List<TDestination> MapList(List<TSource> source);
    }
}
