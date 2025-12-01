using System.Threading.Tasks;
using Mapster;
using MyRecipeBook.Application.Services.Crypto;
using MyRecipeBook.Application.Services.Mappings;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionsBase;

public class RegisterUserUseCase : IRegisterUserUseCase
{

    private readonly IUserWriteOnlyRepository _writeOnlyRepository;
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    private readonly PasswordEncrypter _passwordEncrypter;

    public RegisterUserUseCase(IUserReadOnlyRepository readOnlyRepository, IUserWriteOnlyRepository writeOnlyRepository, PasswordEncrypter passwordEncrypter)
    {
        this._writeOnlyRepository = writeOnlyRepository;
        this._readOnlyRepository = readOnlyRepository;
        this._passwordEncrypter = passwordEncrypter;
    }
    
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        validate(request);

        MappingConfigurations.Configure();

        var user = request.Adapt<MyRecipeBook.Domain.Entities.User>();
        user.Password = _passwordEncrypter.Encrypt(request.Password);

        await _writeOnlyRepository.Add(user);

        return new ResponseRegisteredUserJson
        {
            Name = request.Name
        };
    }

    private void validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}