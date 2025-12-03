using Commons.Requests;
using MyRecipeBook.Application.UseCases.User.Register;
using Shouldly;

namespace Validators.Test.User.Register;

public class RegisterUserValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(RequestRegisterUserJsonBuilder.Build());
        result.IsValid.ShouldBeTrue();
    }
}