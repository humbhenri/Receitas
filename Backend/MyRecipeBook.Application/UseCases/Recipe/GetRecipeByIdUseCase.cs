using Mapster;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Recipe;

public class GetRecipeByIdUseCase: IGetRecipeByIdUseCase
{

    private readonly ILoggedUser _loggedUser;
    private readonly IRecipeReadOnlyRepository _repository;

    public GetRecipeByIdUseCase(ILoggedUser loggedUser, IRecipeReadOnlyRepository repository)
    {
        _loggedUser = loggedUser;
        _repository = repository;
    }

    public async Task<ResponseRecipeJson> Execute(long recipeId)
    {
        var loggedUser = await _loggedUser.User();
        var recipe = await _repository.GetById(loggedUser, recipeId) ?? throw new NotFoundException(Messages.recipe_not_found);
        return recipe.Adapt<ResponseRecipeJson>();
    }
}