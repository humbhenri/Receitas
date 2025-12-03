using Commons.Requests;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Exceptions;
using Shouldly;

namespace Validators.Test.User.Register;

public class RegisterUserValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(RequestRegisterUserJsonBuilder.Build(6));
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Error_Name_Empty()
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build(6);
        request.Name = string.Empty;
        var result = validator.Validate(request);
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldAllBe(e => e.ErrorMessage.Equals("Name cannot be empty"));
    }

    [Fact]
    public void Error_Email_Empty()
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build(6);
        request.Email = string.Empty;
        var result = validator.Validate(request);
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldAllBe(e => e.ErrorMessage.Equals("Email cannot be empty"));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Error_Password_Invalid(int length)
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build(length);
        var result = validator.Validate(request);
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldAllBe(e => e.ErrorMessage.Equals("Password cannot be empty"));
    }
}