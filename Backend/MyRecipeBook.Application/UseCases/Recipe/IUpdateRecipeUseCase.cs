using MyRecipeBook.Communication.Requests;

namespace MyRecipeBook.Application.UseCases.Recipe;

public interface IUpdateRecipeUseCase
{
    Task Execute(long id, RequestRecipeJson request);
}