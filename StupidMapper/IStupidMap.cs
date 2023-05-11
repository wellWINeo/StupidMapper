namespace StupidMapper;

public interface IStupidMap<TSource, TDestination>
    where TSource : new()
    where TDestination: new()
{
    TDestination Map(TSource source);
}

public interface IStupidMap : IStupidMap<object, object> { }