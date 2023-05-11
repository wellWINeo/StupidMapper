# StupidMapper

## Overview
In my opinion, the mapping library should be as simple and stupid as possible. 
Its only task is to transfer fields between classes and nothing more. 
Also such direct use allows to solve the problem with runtime exceptions 
and searching for references.

## Usage
Here provided some brief usage example with notes.

### Arrange
For example we have entity class and dto.
```csharp
// Entity class
public class Account
{
    public long Id { get; set; }
    public string Email { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string MiddleName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}

// DTO
public class PersonDto
{
    public string Login { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
}
```

Next we declare mapping configuration
```csharp
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
```

### Register
Next we need to register all `IStupidMap` configurations to DI:
```csharp
_serviceProvider = new ServiceCollection()
    .AddStupidMapper()
    .BuildServiceProvider();
```

If mapping configurations placed in another assembly 
then we need to make some manual actions:
```csharp
serviceCollection
    .RegisterMaps(customAssembly)
    .AddStupidMapper(scanForMaps: false);
```

Or you can avoid scanning assemblies and register it fully manual:
```csharp
serviceCollection.RegisterMap(typeof(PersonDtoMap));
```

### Map!
After below steps we can now resolve mapper's service and Map our values.
```csharp
var mapper = _serviceProvider.GetRequiredService<IStupidMapper>();

var person = mapper.Map<Account, PersonDto>(account);
person = mapper.Map<PersonDto>(account);
```

## Conclusion
I hope that this few lines of code will help you =)

Feel free to contribute and improve