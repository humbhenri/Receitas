using Bogus;
using MyRecipeBook.Domain.Entities;

namespace Commons.Entities;

public class UserBuilder
{
    public static (User user, string password) Build()
    {
        var passwordEncrypter = PasswordEncrypterBuilder.Build();
        var password = new Faker().Internet.Password();
        var user = new Faker<User>()
            .RuleFor(user => user.Id, () => 1)
            .RuleFor(user => user.Name, (f) => f.Person.FirstName)
            .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
            .RuleFor(user => user.UserIdentifier, _ => new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"))
            .RuleFor(user => user.Password, (f) => passwordEncrypter.Encrypt(password));
        return (user, password);
    }
}