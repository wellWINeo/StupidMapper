using System;
using Microsoft.Extensions.DependencyInjection;
using StupidMapper.Exceptions;

namespace StupidMapper;

public sealed class StupidMapper : IStupidMapper
{
    private readonly IServiceProvider _serviceProvider;

    public StupidMapper(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TDestination Map<TDestination>(object source)
    {
        var mapperType = typeof(IStupidMap<,>)
            .MakeGenericType(source.GetType(), typeof(TDestination));
        var map = _serviceProvider
                      .GetRequiredService(mapperType)
                  ?? throw new StupidMapNotFound(source.GetType(), typeof(TDestination));

        var methodInfo = map
                             .GetType()
                             .GetMethod("Map")
                         ?? throw new StupidMapperInternalException("Method on map not found");

        var value = (TDestination?) methodInfo.Invoke(map, new []{source});
            
        return value
            ?? throw new StupidMapperInternalException("Mapped value is null");
    }

    public TDestination Map<TSource, TDestination>(TSource source) 
        where TSource : new()
        where TDestination : new()
    {
        var mapper = _serviceProvider
                         .GetRequiredService<IStupidMap<TSource, TDestination>>()
                     ?? throw StupidMapNotFound.Create<TSource, TDestination>();
        return mapper.Map(source);
    }
}