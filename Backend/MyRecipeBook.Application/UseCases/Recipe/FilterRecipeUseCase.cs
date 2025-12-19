using Mapster;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Recipe;

public class FilterRecipeUseCase : IFilterRecipeUseCase
{
    private readonly ILoggedUser loggedUser;
    private readonly IRecipeReadOnlyRepository repository;

    public FilterRecipeUseCase(ILoggedUser loggedUser, IRecipeReadOnlyRepository repository)
    {
        this.loggedUser = loggedUser;
        this.repository = repository;
    }

    public async Task<ResponseRecipesJson> Execute(RequestFilterRecipeJson request)
    {
        Validate(request);
        var user = await loggedUser.User();
        var filter = request.Adapt<FilterRecipesDto>();
        var result = await repository.Filter(user, filter);
        var response = result.Adapt<ResponseRecipesJson>();
        return response;
    }

    private void Validate(RequestFilterRecipeJson request)
    {
        var validator = new FilterRecipeValidator();
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            var msg = result.Errors.Select(err => err.ErrorMessage).Distinct().ToList();
            throw new ErrorOnValidationException(msg);
        }
    }
}