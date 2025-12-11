using MyRecipeBook.Exceptions;
using MyRecipeBook.Application.UseCases.User.Profile;
using Shouldly;
using Commons.Requests;

namespace Validators.Test.User.Profile;

public class UpdateUserValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new UpdateUserValidator();
        var request = RequestUpdateUserJsonBuilder.Build();
        var result = validator.Validate(request);
        result.IsValid.ShouldBe(true);
    }
}
