using MyRecipeBook.Communication.Requests;

namespace MyRecipeBook.Application.UseCases.User.Password;

public interface IChangePasswordUseCase
{

    public Task Execute(RequestChangePasswordJson request);
}