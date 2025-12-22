namespace MyRecipeBook.Communication.Responses;

public class ResponseRecipeJson
{
    public string Title { get; set; } = string.Empty;
    public Enums.CookingTime? CookingTime { get; set; } 
    public Enums.Difficulty? Difficulty { get; set; } 
    public IList<Ingredient> Ingredients { get; set; } = [];
    public IList<Instruction> Instructions { get; set; } = [];
    public IList<Enums.DishType> DishTypes { get; set; } = [];
}
