using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Crypto;
using MyRecipeBook.Domain.Services;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Password;

public class ChangePasswordUseCase : IChangePasswordUseCase
{

    private readonly ILoggedUser _loggedUser;
    private readonly IUserReadOnlyRepository _repository;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IUserUpdateOnlyRepository _userUpdateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangePasswordUseCase(ILoggedUser loggedUser, IUserReadOnlyRepository repository, IPasswordEncrypter passwordEncrypter, IUserUpdateOnlyRepository userUpdateRepository, IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _passwordEncrypter = passwordEncrypter;
        _userUpdateRepository = userUpdateRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestChangePasswordJson request)
    {
        var loggedUser = await _loggedUser.User();
        Validate(request, loggedUser);
        var user = await _repository.GetById(loggedUser.Id);
        user?.Password = _passwordEncrypter.Encrypt(request.NewPassword);
        _userUpdateRepository.Update(user!);
        await _unitOfWork.Commit();
    }

    private void Validate(RequestChangePasswordJson request, Domain.Entities.User loggedUser)
    {
        var result = new ChangePasswordValidator().Validate(request);
        var currentPwd = _passwordEncrypter.Encrypt(request.Password);
        if (!currentPwd.Equals(loggedUser.Password))
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, Messages.password_invalid));
        }
        if (!result.IsValid)
        {
            throw new ErrorOnValidationException([.. result.Errors.Select(e => e.ErrorMessage)]);
        }
    }
}
