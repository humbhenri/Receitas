using FluentValidation;
using MyRecipeBook.Application.SharedValidators;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.User.Password;

public class ChangePasswordValidator: AbstractValidator<RequestChangePasswordJson>
{

    public ChangePasswordValidator()
    {
        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestChangePasswordJson>());
        RuleFor(user => user.NewPassword).SetValidator(new PasswordValidator<RequestChangePasswordJson>());
    }
}