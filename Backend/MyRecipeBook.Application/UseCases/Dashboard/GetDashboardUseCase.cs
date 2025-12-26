using Mapster;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services;

namespace MyRecipeBook.Application.UseCases.Dashboard;

public class GetDashboardUseCase : IGetDashboardUseCase
{
    private readonly IRecipeReadOnlyRepository recipeReadOnlyRepository;
    private readonly ILoggedUser loggedUser;

    public GetDashboardUseCase(IRecipeReadOnlyRepository recipeReadOnlyRepository, ILoggedUser loggedUser)
    {
        this.recipeReadOnlyRepository = recipeReadOnlyRepository;
        this.loggedUser = loggedUser;
    }

    public async Task<ResponseRecipesJson> Execute()
    {
        var user = await loggedUser.User();
        var recipes = await recipeReadOnlyRepository.GetForDashboard(user);
        return new ResponseRecipesJson
        {
            Recipes = recipes.Adapt<IList<ResponseShortRecipeJson>>()
        };
    }
}