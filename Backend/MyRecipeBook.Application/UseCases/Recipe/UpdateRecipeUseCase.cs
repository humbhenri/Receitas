
using Mapster;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Recipe;

public class UpdateRecipeUseCase : IUpdateRecipeUseCase
{
    private readonly IRecipeUpdateOnlyRepository recipeUpdateOnlyRepository;
    private readonly ILoggedUser loggedUser;
    private readonly IUnitOfWork unitOfWork;

    public UpdateRecipeUseCase(IRecipeUpdateOnlyRepository recipeUpdateOnlyRepository, ILoggedUser loggedUser, IUnitOfWork unitOfWork)
    {
        this.recipeUpdateOnlyRepository = recipeUpdateOnlyRepository;
        this.loggedUser = loggedUser;
        this.unitOfWork = unitOfWork;
    }

    public async Task Execute(long id, RequestRecipeJson request)
    {
        Validate(request);
        var user = await loggedUser.User();
        var recipe = await recipeUpdateOnlyRepository.GetById(user, id) ?? throw new NotFoundException(Messages.recipe_not_found);
        recipe.Ingredients.Clear();
        recipe.Instructions.Clear();
        recipe.DishTypes.Clear();
        var userId = recipe.UserId;
        request.Adapt(recipe);
        recipe.UserId = userId;
        var instructions = request.Instructions.OrderBy(i => i.Step).ToList();
        for (var i=0; i < instructions.Count; i++)
        {
            instructions.ElementAt(i).Step = i + 1;
        }
        recipe.Instructions = instructions.Adapt<IList<Domain.Entities.Instruction>>();
        recipeUpdateOnlyRepository.Update(recipe);
        await unitOfWork.Commit();
    }

    private static void Validate(RequestRecipeJson request)
    {
        var result = new RecipeValidator().Validate(request);
        if (!result.IsValid)
        {
            throw new ErrorOnValidationException([.. result.Errors.Select(e => e.ErrorMessage).Distinct()]);
        }
    }
}