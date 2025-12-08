using MyRecipeBook.Application.Services.Crypto;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase(IUserReadOnlyRepository repository, PasswordEncrypter passwordEncrypter, IAccessTokenGenerator tokenGenerator) : IDoLoginUseCase
{

    private readonly IUserReadOnlyRepository _repository = repository;

    private readonly PasswordEncrypter _passwordEncrypter = passwordEncrypter;

    private readonly IAccessTokenGenerator _tokenGenerator = tokenGenerator;

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