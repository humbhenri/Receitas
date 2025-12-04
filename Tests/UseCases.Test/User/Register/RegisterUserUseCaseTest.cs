using Commons.Repositories;
using Commons.Requests;
using MyRecipeBook.Application.Services.Crypto;
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
    }

    [Fact]
    public async Task Error_Email_Already_Registered()
    {
        var request = RequestRegisterUserJsonBuilder.Build(6);
        var useCase = CreateUseCase(request.Email);
        Func<Task> act = async () => await useCase.Execute(request);
        await act.ShouldThrowAsync<ErrorOnValidationException>();
    }

    public RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var readOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
        if (!string.IsNullOrEmpty(email))
        {
            readOnlyRepositoryBuilder.ExistActiveUserWithEmail(email);
        }
        var writeOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var passwordEncrypter = new PasswordEncrypter("test");
        var useCase = new RegisterUserUseCase(
            readOnlyRepositoryBuilder.Build(),
            writeOnlyRepository,
            passwordEncrypter,
            unitOfWork
        );
        return useCase;
    }
}