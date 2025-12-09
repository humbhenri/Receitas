using Mapster;
using MyRecipeBook.Application.Services.Mappings;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Services;

namespace MyRecipeBook.Application.UseCases.User.Profile;

public class GetUserProfileUseCase : IGetUserProfileUseCase
{

    private readonly ILoggedUser _loggedUser;

    public GetUserProfileUseCase(ILoggedUser loggedUser)
    {
        _loggedUser = loggedUser;
    }

    public async Task<ResponseUserProfileJson> Execute()
    {
        MappingConfigurations.Configure();
        var user = await _loggedUser.User();
        return user.Adapt<ResponseUserProfileJson>();
    }
}