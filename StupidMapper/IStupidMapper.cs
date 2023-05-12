using StupidMapper.Exceptions;

namespace StupidMapper;

public interface IStupidMapper
{
    /// <summary>
    /// Mapping value to `TDestination` type
    /// </summary>
    /// <param name="source">Source value</param>
    /// <typeparam name="TDestination">Target type</typeparam>
    /// <returns>Mapped value</returns>
    /// <exception cref="StupidMapperInternalException">Internal error occured</exception>
    /// <exception cref="StupidMapNotFoundException">Cannot find suitable map</exception>
    TDestination Map<TDestination>(dynamic source)
        where TDestination : new();
    
    /// <summary>
    /// Mapping multiple values to `TDestination` type
    /// </summary>
    /// <param name="sources">IEnumerable of source values</param>
    /// <typeparam name="TDestination">Target type of values</typeparam>
    /// <returns></returns>
    /// <exception cref="StupidMapperInternalException">Internal error occured</exception>
    /// <exception cref="StupidMapNotFoundException">Cannot find suitable map</exception>
    IEnumerable<TDestination> MapFew<TDestination>(IEnumerable<object> sources)
        where TDestination : new();
    
    /// <summary>
    /// Map with qualified source and destination value
    /// </summary>
    /// <remarks>Should be faster due to no reflection usage</remarks>
    /// <param name="source">Source value</param>
    /// <typeparam name="TSource">Type of source value</typeparam>
    /// <typeparam name="TDestination">Target value type</typeparam>
    /// <returns>Mapped value</returns>
    /// <exception cref="StupidMapNotFoundException">Cannot find suitable map</exception>
    TDestination Map<TSource, TDestination>(TSource source)
        where TSource : new()
        where TDestination : new();

    /// <summary>
    /// Map few values with qualified source and destination value
    /// </summary>
    /// <remarks>Should be faster due to no reflection usage</remarks>
    /// <param name="sources">Source values</param>
    /// <typeparam name="TSource">Type of every source value</typeparam>
    /// <typeparam name="TDestination">Target type of every source value</typeparam>
    /// <returns></returns>
    /// <exception cref="StupidMapNotFoundException">Cannot find suitable map</exception>
    IEnumerable<TDestination> MapFew<TSource, TDestination>(IEnumerable<TSource> sources)
        where TSource : new()
        where TDestination : new();
}