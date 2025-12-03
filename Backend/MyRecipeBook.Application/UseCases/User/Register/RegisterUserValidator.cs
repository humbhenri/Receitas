using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(Messages.name_empty);
        RuleFor(user => user.Email).NotEmpty().WithMessage(Messages.email_empty);
        RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage(Messages.password_empty);
        When(user => !string.IsNullOrEmpty(user.Email), () =>
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage(Messages.email_invalid);
        });
    }
}