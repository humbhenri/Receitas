using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.User.Profile;

public class UpdateUserValidator : AbstractValidator<RequestUpdateUserJson>
{
    public UpdateUserValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage(Messages.name_empty);
        RuleFor(request => request.Email).NotEmpty().WithMessage(Messages.email_empty);
        When(request => !string.IsNullOrWhiteSpace(request.Email), () =>
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage(Messages.email_invalid);
        });
    }
}
