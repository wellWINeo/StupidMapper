namespace StupidMapper.Tests.Models;

public sealed class PersonDtoMap : IStupidMap<Account, PersonDto>
{
    public PersonDto Map(Account source)
    {
        return new PersonDto()
        {
            Login = source.Email
                .Split("@")
                .First(),
            DisplayName = $"{source.FirstName} {source.MiddleName} {source.LastName}",
        };
    }
}