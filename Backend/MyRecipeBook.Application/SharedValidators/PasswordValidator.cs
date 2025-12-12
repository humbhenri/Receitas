using FluentValidation;
using FluentValidation.Validators;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.SharedValidators;

public class PasswordValidator<T> : PropertyValidator<T, string>
{
    public override string Name => "PasswordValidator";

    public override bool IsValid(ValidationContext<T> context, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", Messages.password_empty);
            return false;
        }
        if (value.Length < 6)
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", Messages.password_invalid);
            return false;
        }
        return true;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "{ErrorMessage}";
    }
}