using MyRecipeBook.Application.Services.Crypto;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{

    private readonly IUserReadOnlyRepository _repository;
    private readonly PasswordEncrypter _passwordEncrypter;

    public DoLoginUseCase(IUserReadOnlyRepository repository, PasswordEncrypter passwordEncrypter)
    {
        _repository = repository;
        _passwordEncrypter = passwordEncrypter;
    }


    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var user = await _repository.GetByEmailAndPassword(request.Email, 
            _passwordEncrypter.Encrypt(request.Password)) ?? throw new InvalidLoginException();
        return new ResponseRegisteredUserJson
        {
            Name = user.Name
        };
    }
}