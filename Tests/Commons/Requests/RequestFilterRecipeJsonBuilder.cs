using Bogus;
using MyRecipeBook.Communication.Enums;
using MyRecipeBook.Communication.Requests;

namespace Commons.Requests;

public static class RequestFilterRecipeJsonBuilder
{
    public static RequestFilterRecipeJson Build()
    {
        return new Faker<RequestFilterRecipeJson>()
            .RuleFor(u => u.CookingTimes, f => f.Make(1, () => f.PickRandom<CookingTime>()))
            .RuleFor(u => u.Difficulties, f => f.Make(1, () => f.PickRandom<Difficulty>()))
            .RuleFor(u => u.DishTypes, f => f.Make(1, () => f.PickRandom<DishType>()))
            .RuleFor(u => u.RecipeTitle_Ingredient, f => f.Lorem.Word());
    }
}