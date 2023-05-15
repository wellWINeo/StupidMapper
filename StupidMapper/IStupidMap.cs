namespace StupidMapper;

public interface IStupidMap<TSource, TDestination>
{
    TDestination Map(TSource source);
}

public interface IStupidMap : IStupidMap<object, object> { }