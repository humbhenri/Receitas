
using MyRecipeBook.Domain.Dtos;

namespace MyRecipeBook.Domain.Services.OpenAI;

public interface IGenerateRecipeAI
{
    Task<GeneretedRecipeDto> Generate(IList<string> ingredients);
}