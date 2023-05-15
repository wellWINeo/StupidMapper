using Microsoft.Extensions.DependencyInjection;
using StupidMapper.Exceptions;

namespace StupidMapper;

/// <summary>
/// Wrapper around resolving maps and invoking them
/// </summary>
public sealed class StupidMapper : IStupidMapper
{
    private readonly IServiceProvider _serviceProvider;

    public StupidMapper(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public TDestination Map<TDestination>(dynamic source)
    {
        var value = (TDestination?)InvokeFullyQualifiedMethod(
            nameof(Map), 
            source.GetType(), 
            typeof(TDestination),
            source);

        return value ?? throw new StupidMapperInternalException("Mapped value is null");
    }
    
    /// <inheritdoc />
    public IEnumerable<TDestination> MapFew<TDestination>(IEnumerable<object>? sources) 
    {
        if (sources?.Any() != true)
            return Enumerable.Empty<TDestination>();

        var valuesType = sources!.First().GetType();
        var values = (IEnumerable<TDestination>?)InvokeFullyQualifiedMethod(
            nameof(MapFew),
            valuesType,
            typeof(TDestination),
            sources);

        return values ?? throw new StupidMapperInternalException("Mapped values are null");
    }

    
    private object InvokeFullyQualifiedMethod(
        string methodName, 
        Type source, 
        Type destination, 
        params object[] parameters)
    {
        return GetType()
            .GetMethods()
            .Single(m => m.Name == methodName && m.GetGenericArguments().Length == 2)
            .MakeGenericMethod(source, destination)
            .Invoke(this, parameters);
    }

    
    private IStupidMap<TSource, TDestination> GetMap<TSource, TDestination>()
        => _serviceProvider.GetRequiredService<IStupidMap<TSource, TDestination>>() ??
           throw StupidMapNotFoundException.Create<TSource, TDestination>();
    
    /// <inheritdoc />
    public TDestination Map<TSource, TDestination>(TSource source) 
    {
        var map = GetMap<TSource, TDestination>();
        return map.Map(source);
    }

    /// <inheritdoc />
    public IEnumerable<TDestination> MapFew<TSource, TDestination>(IEnumerable<TSource>? sources) 
    {
        if (sources?.Any() != true)
            yield break;
        
        var map = GetMap<TSource, TDestination>();

        foreach (var source in sources)
            yield return map.Map(source);
    }
}