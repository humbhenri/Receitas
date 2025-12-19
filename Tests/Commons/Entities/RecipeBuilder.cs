using Bogus;
using MyRecipeBook.Communication.Enums;
using MyRecipeBook.Domain.Entities;

namespace Commons.Entities;

public static class RecipeBuilder
{
    public static IList<Recipe> Collection(User user, uint count = 2)
    {
        var list = new List<Recipe>();
        if (count == 0)
        {
            count = 1;
        }
        var recipeId = 1;
        for (int i = 0; i < count; i++)
        {
            var recipe = Build(user);
            recipe.Id = recipeId++;
            list.Add(recipe);
        }
        return list;
    }

    public static Recipe Build(User user)
    {
        return new Faker<Recipe>()
            .RuleFor(r => r.Id, _ => 1)
            .RuleFor(r => r.Title, f => f.Lorem.Word())
            // .RuleFor(r => r.Difficulty, f => f.PickRandom<Difficulty>())
            // .RuleFor(r => r.)
            .RuleFor(r => r.UserId, _ => user.Id)
            // .RuleFor(r => r.CookingTime, f => f.PickRandom<CookingTime>())
            ;
    }
}