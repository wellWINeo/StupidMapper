namespace StupidMapper.Tests.Models;

public sealed class PersonDtoBaseMap : IStupidMap<AccountBase, PersonDtoBase>
{
    public PersonDtoBase Map(AccountBase source)
    {
        return new PersonDto()
        {
            Login = source.Email
                .Split("@")
                .First(),
            DisplayName = string.Empty,
        };
    }
}