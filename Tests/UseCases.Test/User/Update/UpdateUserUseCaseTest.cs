using Commons.Entities;
using Commons.LoggedUser;
using Commons.Repositories;
using Commons.Requests;
using MyRecipeBook.Application.UseCases.User.Profile;
using Shouldly;

namespace UseCases.Test.User.Update;

public class UpdateUserUseCaseTest
{

    [Fact]
    public async Task Success()
    {
        var (user, _) = UserBuilder.Build();
        var request = RequestUpdateUserJsonBuilder.Build();
        var useCase = CreateUseCase(user);
        Func<Task> act = async() => await useCase.Execute(request);
        await act.ShouldNotThrowAsync();
        user.Name.ShouldBe(request.Name);
        user.Email.ShouldBe(request.Email);
    }

    private static UpdateUserUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var userUpdateOnlyRepository = new UserUpdateOnlyRepositoryBuilder();
        var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder();
        userReadOnlyRepository.GetById(user);
        var unitOfWork = UnitOfWorkBuilder.Build();
        return new UpdateUserUseCase(loggedUser, userUpdateOnlyRepository.Build(), userReadOnlyRepository.Build(), unitOfWork);
    }
}
