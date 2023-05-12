namespace StupidMapper.Extensions;

public static class IEnumerableExtensions
{
    public static Type GetTypeOfElements<T>(this IEnumerable<T> source) => typeof(T);
}