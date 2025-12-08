using Commons;
using Commons.Entities;
using Commons.Repositories;
using Commons.Requests;
using Commons.Tokens;
using MyRecipeBook.Application.UseCases.Login.DoLogin;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions.ExceptionsBase;
using Shouldly;

namespace UseCases.Test.Login.DoLogin;

public class DoLoginUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, password) = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        var request = new RequestLoginJson
        {
            Email = user.Email,
            Password = password
        };
        var result = await useCase.Execute(request);
        result.Name.ShouldBe(user.Name);
        result.Tokens.ShouldNotBeNull();
        result.Tokens.AccessToken.ShouldNotBeNullOrEmpty();
    }

    [Fact]  
    public async Task Error_Invalid_User()
    {
        var request = RequestLoginJsonBuilder.Build();
        var useCase = CreateUseCase();
        Func<Task> act = async () => { await useCase.Execute(request); };
        await act.ShouldThrowAsync<InvalidLoginException>();
    }

    private static DoLoginUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User? user = null)
    {
        var encrypter = PasswordEncrypterBuilder.Build();
        var userReadOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
        var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();
        if (user is not null)
        {
            userReadOnlyRepositoryBuilder.GetByEmailAndPassword(user);
        }
        return new DoLoginUseCase(userReadOnlyRepositoryBuilder.Build(),
         encrypter, accessTokenGenerator);
    }
}