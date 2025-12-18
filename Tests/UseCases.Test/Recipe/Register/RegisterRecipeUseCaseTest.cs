using Bogus;
using Commons.Entities;
using Commons.LoggedUser;
using Commons.Repositories;
using Commons.Requests;
using MyRecipeBook.Application.Services.Mappings;
using MyRecipeBook.Application.UseCases.Recipe;
using MyRecipeBook.Exceptions.ExceptionsBase;
using Shouldly;

namespace UseCases.Test.Recipe.Register;

public class RegisteRecipeUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, _) = UserBuilder.Build();
        var request = RequestRecipeJsonBuilder.Build();
        var useCase = CreateUseCase(user);
        MappingConfigurations.Configure("abc");
        var result = await useCase.Execute(request);
        result.ShouldNotBeNull();
        result.Id.ShouldNotBeNull();
        result.Title.ShouldBe(request.Title);
    }

    [Fact]
    public async Task Error_Title_Empty()
    {
        (var user, _) = UserBuilder.Build();
        var request = RequestRecipeJsonBuilder.Build();
        request.Title = string.Empty;
        var useCase = CreateUseCase(user);
        MappingConfigurations.Configure("abc");
        var act = async () => { await useCase.Execute(request); };
        await act.ShouldThrowAsync<ErrorOnValidationException>();
    }

    [Fact]
    public async Task Error_Instruction_Too_Long()
    {
        (var user, _) = UserBuilder.Build();
        var request = RequestRecipeJsonBuilder.Build();
        request.Instructions.First().Text = RequestStringGenerator.Paragraphs(minCharacters: 2001);
        var useCase = CreateUseCase(user);
        MappingConfigurations.Configure("abc");
        var act = async () => { await useCase.Execute(request); };
        await act.ShouldThrowAsync<ErrorOnValidationException>();
    }

    private static RegisterRecipeUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var unitOfWork = UnitOfWorkBuilder.Build();
        var repository = RecipeWriteOnlyRepositoryBuilder.Build();
        return new RegisterRecipeUseCase(loggedUser, repository, unitOfWork);
    }
}