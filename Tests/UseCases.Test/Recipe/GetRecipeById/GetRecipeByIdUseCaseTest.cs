using Commons.Entities;
using Commons.LoggedUser;
using Commons.Repositories;
using MyRecipeBook.Application.Services.Mappings;
using MyRecipeBook.Application.UseCases.Recipe;
using MyRecipeBook.Exceptions.ExceptionsBase;
using Shouldly;

namespace UseCases.Test.Recipe.GetRecipeById;

public class GetRecipeByIdUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        MappingConfigurations.Configure("123");
        (var user, _) = UserBuilder.Build();
        var recipe = RecipeBuilder.Build(user);
        var useCase = CreateUseCase(user, recipe);
        var result = await useCase.Execute(recipe.Id);
        result.ShouldNotBeNull();
        result.Title.ShouldBe(recipe.Title);
    }

    [Fact]
    public async Task Error_Recipe_Not_Found()
    {
        MappingConfigurations.Configure("123");
        (var user, _) = UserBuilder.Build();
        var useCase = CreateUseCase(user, null);
        var act = async () => { await useCase.Execute(123); };
        await act.ShouldThrowAsync<NotFoundException>();
    }

    private static GetRecipeByIdUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user, 
        MyRecipeBook.Domain.Entities.Recipe? recipe)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var repositoryBuilder = new RecipeReadOnlyRepositoryBuilder();
        if (recipe is not null)
        {
            repositoryBuilder.GetById(user, recipe);
        }
        return new GetRecipeByIdUseCase(loggedUser, repositoryBuilder.Build());
    }
}