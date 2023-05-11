using System;

namespace StupidMapper.Exceptions;

public sealed class StupidMapNotFound
    : Exception
{
    public StupidMapNotFound(Type source, Type destination)
        : base($"Cannot find map from {nameof(source)} to {nameof(destination)}") { }

    public static StupidMapNotFound Create<TSource, TDestination>() => new(typeof(TSource), typeof(TDestination));
}