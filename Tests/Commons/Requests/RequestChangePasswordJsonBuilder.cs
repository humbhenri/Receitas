using Bogus;
using MyRecipeBook.Communication.Requests;

namespace Commons.Requests;

public static class RequestChangePasswordJsonBuilder
{
    public static RequestChangePasswordJson Build()
    {
        return new Faker<RequestChangePasswordJson>()
            .RuleFor(user => user.Password, (f) => f.Internet.Password(6))
            .RuleFor(user => user.NewPassword, (f) => f.Internet.Password(6));
    }
}