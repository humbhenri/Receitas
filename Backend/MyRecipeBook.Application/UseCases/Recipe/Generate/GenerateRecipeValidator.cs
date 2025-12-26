using MyRecipeBook.Domain.ValueObjects;
using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.Recipe.Generate;

public class GenerateRecipeValidator: AbstractValidator<RequestGenerateRecipeJson>
{
    public GenerateRecipeValidator()
    {
        var max = MyRecipeBookConstants.MAX_INGREDIENTS_GENERATE_RECIPE;
        RuleFor(r => r.Ingredients.Count).InclusiveBetween(1, max).WithMessage(Messages.wrong_number_ingredients);
        RuleFor(r => r.Ingredients).Must(ingredients => ingredients.Count == ingredients.Select(c => c).Distinct().Count()).WithMessage(Messages.wrong_number_ingredients);
        RuleFor(r => r.Ingredients).ForEach(rule => 
            rule.Custom((value, context) =>
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    context.AddFailure("Ingredient", Messages.ingredient_empty);
                    return;
                }
                if (value.Count(c => c == ' ') > 3 || value.Count(c => c == '/') > 1)
                {

                    context.AddFailure("Ingredient", Messages.ingredient_not_following_pattern);
                    return;
                }
            })
        );
    }

}