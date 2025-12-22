using Moq;
using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services;

namespace Commons.Repositories;

public class RecipeReadOnlyRepositoryBuilder
{
    private readonly Mock<IRecipeReadOnlyRepository> repository;

    public RecipeReadOnlyRepositoryBuilder()
    {
        repository = new Mock<IRecipeReadOnlyRepository>();
    }

    public RecipeReadOnlyRepositoryBuilder Filter(User user, IList<Recipe> recipes)
    {
        repository.Setup(r => r.Filter(user, It.IsAny<FilterRecipesDto>()))
            .ReturnsAsync(recipes);
        return this;
    }

    public IRecipeReadOnlyRepository Build() => repository.Object;

    public RecipeReadOnlyRepositoryBuilder GetById(User user, Recipe recipe)
    {
        repository.Setup(r => r.GetById(user, It.IsAny<long>())).ReturnsAsync(recipe);
        return this;
    }
}