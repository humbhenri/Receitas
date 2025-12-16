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
        RuleFor(r => r.Ingredients.Count).GreaterThan(0).WithMessage(Messages.at_least_one_ingredient);
        RuleFor(r => r.Instructions.Count).GreaterThan(0).WithMessage(Messages.at_least_one_instruction);
        RuleForEach(r => r.DishTypes).IsInEnum().WithMessage(Messages.dish_type_not_supported);
        RuleForEach(r => r.Ingredients).NotEmpty().WithMessage(Messages.ingredient_empty);
        RuleForEach(r => r.Instructions).ChildRules(i =>
        {
            i.RuleFor(i => i.Step).GreaterThan(0).WithMessage(Messages.non_negative_step);
            i.RuleFor(i => i.Text).NotEmpty().WithMessage(Messages.empty_instruction)
                .MaximumLength(2000).WithMessage(Messages.instructions_too_big);
        });
        RuleFor(r => r.Instructions).Must(instructions => instructions.Select(i => i.Step).Distinct().Count() == instructions.Count)
            .WithMessage(Messages.steps_must_be_unique);
    }
}