namespace StupidMapper.Tests;

public class IoCTests
{
    private readonly IServiceProvider _serviceProvider;
    
    public IoCTests()
    {
        _serviceProvider = new ServiceCollection()
            .AddStupidMapper()
            .BuildServiceProvider();
    }


    [Fact]
    public void MapperResolvingTest()
    {
        var mapper = _serviceProvider.GetRequiredService<IStupidMapper>();

        mapper.Should().NotBeNull();
    }

    [Fact]
    public void MapResolveTest()
    {
        var map = _serviceProvider.GetRequiredService<IStupidMap<Account, PersonDto>>();
        
        map
            .Should().NotBeNull()
            .And.BeAssignableTo<IStupidMap<Account, PersonDto>>()
            .And.BeOfType<PersonDtoMap>();
    }
}