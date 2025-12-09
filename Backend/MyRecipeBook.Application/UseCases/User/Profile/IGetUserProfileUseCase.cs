using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Application.UseCases.User.Register;

public interface IGetUserProfileUseCase
{
    public Task<ResponseUserProfileJson> Execute();
}