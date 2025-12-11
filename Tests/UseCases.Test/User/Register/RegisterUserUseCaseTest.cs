using Commons;
using Commons.Repositories;
using Commons.Requests;
using Commons.Tokens;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Exceptions.ExceptionsBase;
using Shouldly;

namespace UseCases.Test.User.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build(6);
        var useCase = CreateUseCase();
        var result = await useCase.Execute(request);
        result.ShouldNotBeNull();
        result.Name.ShouldBe(request.Name);
        result.Tokens.ShouldNotBeNull();
        result.Tokens.AccessToken.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_Email_Already_Registered()
    {
        var request = RequestRegisterUserJsonBuilder.Build(6);
        var useCase = CreateUseCase(request.Email);
        Func<Task> act = async () => await useCase.Execute(request);
        await act.ShouldThrowAsync<ErrorOnValidationException>();
    }

    public static RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var readOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
        if (!string.IsNullOrEmpty(email))
        {
            readOnlyRepositoryBuilder.ExistActiveUserWithEmail(email);
        }
        var writeOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var passwordEncrypter = PasswordEncrypterBuilder.Build();
        var accessToken = JwtTokenGeneratorBuilder.Build();
        var useCase = new RegisterUserUseCase(
            readOnlyRepositoryBuilder.Build(),
            writeOnlyRepository,
            passwordEncrypter,
            unitOfWork,
            accessToken
        );
        return useCase;
    }
}