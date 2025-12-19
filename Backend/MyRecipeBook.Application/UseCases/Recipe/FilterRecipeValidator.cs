using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.Recipe;

public class FilterRecipeValidator : AbstractValidator<RequestFilterRecipeJson>
{
    public FilterRecipeValidator()
    {
        RuleForEach(r => r.CookingTimes).IsInEnum().WithMessage(Messages.cooking_time_not_supported);
        RuleForEach(r => r.Difficulties).IsInEnum().WithMessage(Messages.difficulty_not_supported);
        RuleForEach(r => r.DishTypes).IsInEnum().WithMessage(Messages.dish_type_not_supported);
    }
}