using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Application.UseCases.Recipe;

public interface IGetRecipeByIdUseCase
{
    public Task<ResponseRecipeJson> Execute(long recipeId);
}