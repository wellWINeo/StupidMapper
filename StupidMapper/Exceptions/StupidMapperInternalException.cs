using System;

namespace StupidMapper.Exceptions;

public sealed class StupidMapperInternalException : Exception
{
    public StupidMapperInternalException(string message) 
        : base(message){ }
}