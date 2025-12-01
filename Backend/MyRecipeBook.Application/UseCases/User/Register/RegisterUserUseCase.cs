using Mapster;
using MyRecipeBook.Application.Services.Crypto;
using MyRecipeBook.Application.Services.Mappings;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions.ExceptionsBase;

public class RegisterUserUseCase
{
    
    public ResponseRegisteredUserJson Execute(RequestRegisterUserJson request)
    {

        validate(request);

        MappingConfigurations.Configure();

        var user = request.Adapt<MyRecipeBook.Domain.Entities.User>();
        var passwordEncripter = new PasswordEncripter();
        user.Password = passwordEncripter.Encrypt(request.Password);

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