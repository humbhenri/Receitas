using Mapster;
using MyRecipeBook.Application.Services.Crypto;
using MyRecipeBook.Application.Services.Mappings;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{

    private readonly IUserWriteOnlyRepository _writeOnlyRepository;
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    private readonly PasswordEncrypter _passwordEncrypter;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserUseCase(IUserReadOnlyRepository readOnlyRepository, IUserWriteOnlyRepository writeOnlyRepository, PasswordEncrypter passwordEncrypter, IUnitOfWork unitOfWork)
    {
        this._writeOnlyRepository = writeOnlyRepository;
        this._readOnlyRepository = readOnlyRepository;
        this._passwordEncrypter = passwordEncrypter;
        this._unitOfWork = unitOfWork;
    }
    
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        MappingConfigurations.Configure();

        var user = request.Adapt<MyRecipeBook.Domain.Entities.User>();
        user.Password = _passwordEncrypter.Encrypt(request.Password);

        await _writeOnlyRepository.Add(user);

        await _unitOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            Name = request.Name
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);
        var emailExists = await _readOnlyRepository.ExistActiveUserWithEmail(request.Email);
        if (emailExists)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, 
                Messages.email_already_registered));
        }
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}