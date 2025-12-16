using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Application.UseCases.Recipe;

public interface IRegisterRecipeUseCase
{
    Task<ResponseRegisteredRecipeJson> Execute(RequestRecipeJson request);
}