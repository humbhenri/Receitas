using Commons.Entities;
using Commons.LoggedUser;
using Commons.Repositories;
using Commons.Requests;
using MyRecipeBook.Application.UseCases.Recipe;
using Shouldly;

namespace UseCases.Test.Recipe.Filter;

public class FilterRecipeUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, _) = UserBuilder.Build();
        var request = RequestFilterRecipeJsonBuilder.Build();
        var recipes = RecipeBuilder.Collection(user);
        var useCase = CreateUseCase(user, recipes);
        var result = await useCase.Execute(request);
        result.ShouldNotBeNull();
        result.Recipes.Count.ShouldBe(recipes.Count);
    }

    private static FilterRecipeUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user, IList<MyRecipeBook.Domain.Entities.Recipe> recipes)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var repository = new RecipeReadOnlyRepositoryBuilder().Filter(user, recipes).Build();
        return new FilterRecipeUseCase(loggedUser, repository);
    }
}