namespace MyRecipeBook.Application.UseCases.Recipe;

public interface IDeleteRecipeUseCase
{
    Task Execute(long id);
}