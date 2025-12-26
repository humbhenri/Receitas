
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Recipe;

public class DeleteRecipeUseCase : IDeleteRecipeUseCase
{
    private readonly ILoggedUser loggedUser;

    private readonly IRecipeReadOnlyRepository recipeReadOnlyRepository;
    private readonly IRecipeWriteOnlyRepository recipeWriteOnlyRepository;
    private readonly IUnitOfWork unitOfWork;


    public DeleteRecipeUseCase(ILoggedUser loggedUser, IRecipeReadOnlyRepository recipeReadOnlyRepository, IRecipeWriteOnlyRepository recipeWriteOnlyRepository, IUnitOfWork unitOfWork)
    {
        this.loggedUser = loggedUser;
        this.recipeReadOnlyRepository = recipeReadOnlyRepository;
        this.recipeWriteOnlyRepository = recipeWriteOnlyRepository;
        this.unitOfWork = unitOfWork;
    }


    public async Task Execute(long id)
    {
        var user = await loggedUser.User();
        var recipe = await recipeReadOnlyRepository.GetById(user, id);
        if (recipe is null)
        {
            throw new NotFoundException(Messages.recipe_not_found);
        }
        await recipeWriteOnlyRepository.Delete(recipe);
        await unitOfWork.Commit();
    }
}