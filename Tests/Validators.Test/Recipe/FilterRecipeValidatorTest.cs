using Commons.Requests;
using MyRecipeBook.Application.UseCases.Recipe;
using Shouldly;

namespace Validators.Test.Recipe;

public class FilterRecipeValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new FilterRecipeValidator();
        var request = RequestFilterRecipeJsonBuilder.Build();
        var result = validator.Validate(request);
        result.IsValid.ShouldBeTrue();
    }
}