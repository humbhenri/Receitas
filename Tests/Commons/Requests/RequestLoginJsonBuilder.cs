using Bogus;
using MyRecipeBook.Communication.Requests;

namespace Commons.Requests;

public class RequestLoginJsonBuilder
{
    public static RequestLoginJson Build()
    {
        return new Faker<RequestLoginJson>()
            .RuleFor(user => user.Email, (f, u) => f.Internet.Email(u.Email))
            .RuleFor(user => user.Password, (f) => f.Internet.Password(6));
    }
}