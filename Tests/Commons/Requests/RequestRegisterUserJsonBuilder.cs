using Bogus;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Entities;

namespace Commons.Requests;

public class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Build()
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(user => user.Name, (f) => f.Person.FirstName)
            .RuleFor(user => user.Email, (f, u) => f.Internet.Email(u.Name))
            .RuleFor(user => user.Password, (f) => f.Internet.Password());
    }
}