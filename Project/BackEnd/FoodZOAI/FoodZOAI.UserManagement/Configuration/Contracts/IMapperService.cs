namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IMapperService<TSource, TDestination>
    {
        TDestination Map(TSource source);

        List<TDestination> MapList(List<TSource> source);
    }
}

