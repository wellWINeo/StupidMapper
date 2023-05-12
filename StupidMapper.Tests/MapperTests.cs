namespace StupidMapper.Tests;

public class MapperTests
{
    private readonly IStupidMapper _mapper;

    public MapperTests()
    {
        var serviceProvider = new ServiceCollection()
            .AddStupidMapper()
            .BuildServiceProvider();
        _mapper = serviceProvider.GetRequiredService<IStupidMapper>();
    }

    [Fact]
    public void MapTwoGenericTest()
    {
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

        var actual = _mapper.Map<Account, PersonDto>(account);
        
        actual
            .Should().NotBeNull()
            .And.BeEquivalentTo(actual);
    }
    
    [Fact]
    public void MapSingleGenericTest()
    {
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

        var actual = _mapper.Map<PersonDto>(account);

        actual
            .Should().NotBeNull()
            .And.BeEquivalentTo(actual);
    }

    [Fact]
    public void MapFewTwoGenericTest()
    {
        var accounts = new Account[]
        {
            new()
            {
                Id = 1,
                Email = "johndoe@example.com",
                FirstName = "John",
                MiddleName = "Bart",
                LastName = "Doe",
            },
            new()
            {
                Id = 1,
                Email = "AntioneCarraway@example.com",
                FirstName = "Antione",
                MiddleName = "Brandon",
                LastName = "Carraway",
            }
        };
        var expected = new PersonDto[]
        {
            new()
            {
                Login = "johndoe",
                DisplayName = "John Bart Doe",
            },
            new()
            {
                Login = "AntioneCarraway",
                DisplayName = "Antione Brandon Carraway",
            },
        };

        var actual = _mapper
            .MapFew<Account, PersonDto>(accounts)
            .ToArray();

        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void MapFewSingleGenericTest()
    {
        var accounts = new Account[]
        {
            new()
            {
                Id = 1,
                Email = "johndoe@example.com",
                FirstName = "John",
                MiddleName = "Bart",
                LastName = "Doe",
            },
            new()
            {
                Id = 1,
                Email = "AntioneCarraway@example.com",
                FirstName = "Antione",
                MiddleName = "Brandon",
                LastName = "Carraway",
            }
        };
        var expected = new PersonDto[]
        {
            new()
            {
                Login = "johndoe",
                DisplayName = "John Bart Doe",
            },
            new()
            {
                Login = "AntioneCarraway",
                DisplayName = "Antione Brandon Carraway",
            },
        };

        var actual = _mapper
            .MapFew<PersonDto>(accounts)
            .ToArray();

        actual.Should().BeEquivalentTo(expected);
    }
}