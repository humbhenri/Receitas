using Commons;
using Commons.Entities;
using Commons.LoggedUser;
using Commons.Repositories;
using Commons.Requests;
using MyRecipeBook.Application.UseCases.User.Password;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions.ExceptionsBase;
using MyRecipeBook.Infrastructure.Services.Crypto;
using Shouldly;

namespace UseCases.Test.User.Password;

public class ChangePasswordUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, password) = UserBuilder.Build();
        var request = RequestChangePasswordJsonBuilder.Build();
        request.Password = password;
        var useCase = CreateUseCase(user);
        Func<Task> act = async() => await useCase.Execute(request);
        await act.ShouldNotThrowAsync();
        var encrypter = PasswordEncrypterBuilder.Build();
        user.Password.ShouldBe(encrypter.Encrypt(request.NewPassword));
    }

    [Fact]
    public async Task Error_NewPassword_Empty()
    {
        var (user, password) = UserBuilder.Build();
        var request = RequestChangePasswordJsonBuilder.Build();
        request.Password = password;
        request.NewPassword = string.Empty;
        var useCase = CreateUseCase(user);
        Func<Task> act = async() => await useCase.Execute(request);
        await act.ShouldThrowAsync<ErrorOnValidationException>();
    }

    [Fact]
    public async Task Error_CurrentPassword_Different()
    {
        var (user, password) = UserBuilder.Build();
        var request = RequestChangePasswordJsonBuilder.Build();
        request.Password = password+"!";
        var useCase = CreateUseCase(user);
        Func<Task> act = async() => await useCase.Execute(request);
        await act.ShouldThrowAsync<ErrorOnValidationException>();
    }

    private ChangePasswordUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var repositoryBuilder = new UserReadOnlyRepositoryBuilder();
        repositoryBuilder.GetById(user);
        var passwordEncrypter = PasswordEncrypterBuilder.Build();
        var userUpdateRepositoryBuilder = new UserUpdateOnlyRepositoryBuilder();
        var unitOfWork = UnitOfWorkBuilder.Build();
        return new ChangePasswordUseCase(loggedUser, repositoryBuilder.Build(), passwordEncrypter, userUpdateRepositoryBuilder.Build(), unitOfWork);
    }
}