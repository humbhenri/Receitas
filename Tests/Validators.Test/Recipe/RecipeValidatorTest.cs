using Commons.Requests;
using MyRecipeBook.Application.UseCases.Recipe;
using Shouldly;

namespace Validators.Test.Recipe;

public class RecipeValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new RecipeValidator();
        var request = RequestRecipeJsonBuilder.Build();
        var result = validator.Validate(request);
        result.IsValid.ShouldBe(true);
    }

    [Fact]
    public void Error_Cooking_Time()
    {
        var validator = new RecipeValidator();
        var request = RequestRecipeJsonBuilder.Build();
        request.CookingTime = (MyRecipeBook.Communication.Enums.CookingTime?)1000;
        var result = validator.Validate(request);
        result.IsValid.ShouldBe(false);
    }

    [Fact]
    public void Error_Difficulty()
    {
        var validator = new RecipeValidator();
        var request = RequestRecipeJsonBuilder.Build();
        request.Difficulty = (MyRecipeBook.Communication.Enums.Difficulty?)100;
        var result = validator.Validate(request);
        result.IsValid.ShouldBe(false);
    }
}