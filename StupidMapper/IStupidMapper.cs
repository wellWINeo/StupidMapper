namespace StupidMapper;

public interface IStupidMapper
{
    TDestination Map<TDestination>(object source);
    
    TDestination Map<TSource, TDestination>(TSource source)
        where TSource : new()
        where TDestination : new();
}