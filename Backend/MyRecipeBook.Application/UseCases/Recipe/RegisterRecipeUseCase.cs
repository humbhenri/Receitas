using Mapster;
using MyRecipeBook.Application.Services.Mappings;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Recipe;

public class RegisterRecipeUseCase : IRegisterRecipeUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IRecipeWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterRecipeUseCase(ILoggedUser loggedUser, IRecipeWriteOnlyRepository repository, IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseRegisteredRecipeJson> Execute(RequestRecipeJson request)
    {
        Validate(request);
        var recipe = request.Adapt<Domain.Entities.Recipe>();
        var loggedUser = await _loggedUser.User();
        recipe.UserId = loggedUser.Id;
        var instructions = request.Instructions.OrderBy(i => i.Step).ToList();
        for (var i=0; i<instructions.Count; i++)
        {
            instructions.ElementAt(i).Step = i + 1;
        }
        foreach (var instruction in instructions)
        {
            recipe.Instructions.Add(instruction.Adapt<Domain.Entities.Instruction>());
        }
        await _repository.Add(recipe);
        await _unitOfWork.Commit();
        return recipe.Adapt<ResponseRegisteredRecipeJson>();
    }

    private void Validate(RequestRecipeJson request)
    {
        var result = new RecipeValidator().Validate(request);
        if (!result.IsValid)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage)
                .Distinct().ToList();
            throw new ErrorOnValidationException(errors);
        }
    }
}