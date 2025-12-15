using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.Recipe;

public class RecipeValidator : AbstractValidator<RequestRecipeJson>
{
    public RecipeValidator()
    {
        RuleFor(r => r.Title).NotEmpty().WithMessage(Messages.recipe_empty_title);
        RuleFor(r => r.CookingTime).IsInEnum().WithMessage(Messages.cooking_time_not_supported);
        RuleFor(r => r.Difficulty).IsInEnum().WithMessage(Messages.difficulty_not_supported);
    }
}