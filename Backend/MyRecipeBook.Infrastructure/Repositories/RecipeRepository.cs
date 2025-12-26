using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Infrastructure.DataAccess;

namespace MyRecipeBook.Infrastructure.Repositories;

public sealed class RecipeRepository : IRecipeWriteOnlyRepository, IRecipeReadOnlyRepository
{
    private readonly MyRecipeBookDbContext _dbContext;

    public RecipeRepository(MyRecipeBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Recipe recipe)
    {
        await _dbContext.AddAsync(recipe);
    }

    public async Task Delete(Recipe recipe)
    {
        var r = await _dbContext.Recipes.FindAsync(recipe.Id); // because of as no tracking
        _dbContext.Remove(r!);
    }

    public async Task<IList<Recipe>> Filter(User user, FilterRecipesDto filter)
    {
        var query = _dbContext.Recipes.AsNoTracking()
            .Where(recipe => recipe.Active && recipe.UserId == user.Id);
        if (filter.Difficulties.Any())
        {
            query = query.Where(recipe => 
                recipe.Difficulty.HasValue &&
                filter.Difficulties.Contains(recipe.Difficulty.Value));
        }
        if (filter.CookingTimes.Any())
        {
            query = query.Where(recipe => 
                recipe.CookingTime.HasValue &&
                filter.CookingTimes.Contains(recipe.CookingTime.Value));
        }
        if (filter.DishTypes.Any())
        {
            query = query.Where(recipe => 
                recipe.DishTypes.Any(dt => filter.DishTypes.Contains(dt.Type)));
        }
        if (! string.IsNullOrEmpty(filter.RecipeTitle_Ingredient))
        {
            query = query.Where(recipe =>
                recipe.Title.Contains(filter.RecipeTitle_Ingredient)
                 || recipe.Ingredients.Any(ingredient => ingredient.Item.Contains(filter.RecipeTitle_Ingredient)));
        }
        return await query.ToListAsync();
    }

    public async Task<Recipe?> GetById(User user, long recipeId)
    {
        return await _dbContext.Recipes
            .AsNoTracking()
            .Include(r => r.Ingredients)
            .Include(r => r.Instructions)
            .Include(r => r.DishTypes)
            .FirstOrDefaultAsync(r => r.Active && r.Id == recipeId && r.UserId == user.Id);
    }
}