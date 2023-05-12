using System;

namespace StupidMapper.Exceptions;

public sealed class StupidMapNotFoundException
    : Exception
{
    public StupidMapNotFoundException(Type source, Type destination)
        : base($"Cannot find map from {nameof(source)} to {nameof(destination)}") { }

    public static StupidMapNotFoundException Create<TSource, TDestination>() => new(typeof(TSource), typeof(TDestination));
}