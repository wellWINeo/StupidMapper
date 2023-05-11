using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace StupidMapper.Extensions;

public static class IoC
{
    public static IServiceCollection AddStupidMapper(this IServiceCollection services, bool scanForMaps = true)
    {
        if (scanForMaps)
            services.RegisterMaps(Assembly.GetCallingAssembly());

        services.AddSingleton<IStupidMapper, StupidMapper>();

        return services;
    }
    
    public static IServiceCollection RegisterMaps(this IServiceCollection services, Assembly assembly)
    {
        var types = assembly.GetTypes();

        var mapperTypes = assembly
            .GetTypes()
            .Where(t => t is { IsInterface: false, IsAbstract: false, IsClass: true }
                        && t.IsImplementInterfaceWithoutGenerics(typeof(IStupidMap<,>)));

        foreach (var mapperType in mapperTypes)
        {
            services.RegisterMap(mapperType);
        }
    
        return services;
    }

    public static IServiceCollection RegisterMap(this IServiceCollection services, Type mapperType)
    {
        var mapperInterfaces = mapperType
            .GetInterfaces()
            .Where(t => TypeExtensions.CompareInterfaceWithoutGenerics(t, typeof(IStupidMap<,>)));

        foreach (var mapperInterface in mapperInterfaces)
            services.AddSingleton(mapperInterface, mapperType);
        
        return services;
    }
}