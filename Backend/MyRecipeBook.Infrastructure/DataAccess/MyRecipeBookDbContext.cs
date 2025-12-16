using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Infrastructure.DataAccess;

public class MyRecipeBookDbContext : DbContext
{
    public MyRecipeBookDbContext(DbContextOptions options) : base(options) {}
    
    public DbSet<User> Users {get; set;}
    public DbSet<Recipe> Recipes {get; set;}
    public DbSet<DishType> DishTypes {get; set;}
    public DbSet<Ingredient> Ingredients {get; set;}
    public DbSet<Instruction> Instructions {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyRecipeBookDbContext).Assembly);
    }
}