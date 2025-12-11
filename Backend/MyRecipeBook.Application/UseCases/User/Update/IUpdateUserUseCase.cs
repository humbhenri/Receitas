using MyRecipeBook.Communication.Requests;

namespace MyRecipeBook.Application.UseCases.User.Profile;

public interface IUpdateUserUseCase
{
    public Task Execute(RequestUpdateUserJson request);
}