using Microsoft.Extensions.DependencyInjection;
using StupidMapper.Extensions;
using StupidMapper.Tests.Models;

namespace StupidMapper.Tests;

public class MapperTests
{
    private readonly IServiceProvider _serviceProvider;

    public MapperTests()
    {
        _serviceProvider = new ServiceCollection()
            .AddStupidMapper()
            .BuildServiceProvider();
    }


    [Fact]
    public void MapperResolvingTest()
    {
        var mapper = _serviceProvider.GetRequiredService<IStupidMapper>();
        
        Assert.NotNull(mapper);
    }

    [Fact]
    public void MapResolveTest()
    {
        var map = _serviceProvider.GetRequiredService<IStupidMap<Account, PersonDto>>();
        
        Assert.NotNull(map);
        Assert.IsAssignableFrom<IStupidMap<Account, PersonDto>>(map);
        Assert.IsType<PersonDtoMap>(map);
    }

    [Fact]
    public void MapTwoGenericTest()
    {
        var mapper = _serviceProvider.GetRequiredService<IStupidMapper>();
        var expected = new PersonDto
        {
            Login = "johndoe",
            DisplayName = "John Bart Doe",
        };
        var account = new Account
        {
            Id = 1,
            Email = "johndoe@example.com",
            FirstName = "John",
            MiddleName = "Bart",
            LastName = "Doe",
        };

        var actual = mapper.Map<Account, PersonDto>(account);
        
        Assert.NotNull(actual);
        Assert.Equivalent(expected, actual);
    }
    
    [Fact]
    public void MapSingleGenericTest()
    {
        var mapper = _serviceProvider.GetRequiredService<IStupidMapper>();
        var expected = new PersonDto
        {
            Login = "johndoe",
            DisplayName = "John Bart Doe",
        };
        var account = new Account
        {
            Id = 1,
            Email = "johndoe@example.com",
            FirstName = "John",
            MiddleName = "Bart",
            LastName = "Doe",
        };

        var actual = mapper.Map<PersonDto>(account);
        
        Assert.NotNull(actual);
        Assert.Equivalent(expected, actual);
    }
}