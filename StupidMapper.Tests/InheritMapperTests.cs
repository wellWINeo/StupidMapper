namespace StupidMapper.Tests;

public class InheritMapperTests
{
    private readonly IStupidMapper _mapper;

    public InheritMapperTests()
    {
        var serviceProvider = new ServiceCollection()
            .AddStupidMapper()
            .BuildServiceProvider();
        _mapper = serviceProvider.GetRequiredService<IStupidMapper>();
    }

    [Fact]
    public void MapAccountBase()
    {
        var expected = new PersonDto
        {
            Login = "johndoe",
            DisplayName = string.Empty,
        };
        var account = new Account
        {
            Id = 1,
            Email = "johndoe@example.com",
            FirstName = "John",
            MiddleName = "Bart",
            LastName = "Doe",
        };

        var actual = _mapper.Map<AccountBase, PersonDtoBase>(account);
        
        actual
            .Should().NotBeNull()
            .And.BeAssignableTo<PersonDtoBase>()
            .And.BeEquivalentTo(expected);
    }
}