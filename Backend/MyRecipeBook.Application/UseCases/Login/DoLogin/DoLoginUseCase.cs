using MyRecipeBook.Application.Services.Crypto;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{

    private readonly IUserReadOnlyRepository _repository;

    private readonly PasswordEncrypter _passwordEncrypter;

    private readonly IAccessTokenGenerator _tokenGenerator;

#pragma warning disable IDE0290 // Use primary constructor
    public DoLoginUseCase(IUserReadOnlyRepository repository, PasswordEncrypter passwordEncrypter, IAccessTokenGenerator tokenGenerator)
#pragma warning restore IDE0290 // Use primary constructor
    {
        _repository = repository;
        _passwordEncrypter = passwordEncrypter;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var user = await _repository.GetByEmailAndPassword(request.Email, 
            _passwordEncrypter.Encrypt(request.Password)) ?? throw new InvalidLoginException();
        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Tokens = new ResponseTokensJson()
            {
                AccessToken = _tokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }
}