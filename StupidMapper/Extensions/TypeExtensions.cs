using System;

namespace StupidMapper.Extensions;

public static class TypeExtensions
{
    internal static bool IsImplementInterfaceWithoutGenerics(this Type @class, Type @interface)
    {
        foreach (var implementedInterface in @class.GetInterfaces())
        {
            if (CompareInterfaceWithoutGenerics(implementedInterface, @interface))
                return true;
        }
        
        return false;
    }

    internal static bool CompareInterfaceWithoutGenerics(Type withGenerics, Type noGenerics)
    {
        try
        {
            var genericType = noGenerics.MakeGenericType(withGenerics.GetGenericArguments());
            return genericType.IsEquivalentTo(withGenerics);
        }
        catch (Exception)
        {
            return false;
        }
    }
}