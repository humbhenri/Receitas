using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Domain.Services.OpenAI;

namespace MyRecipeBook.Infrastructure.Services.OpenAI;

public class GenerateRecipeAI : IGenerateRecipeAI
{
    public async Task<GeneretedRecipeDto> Generate(IList<string> ingredients)
    {
        throw new NotImplementedException();
    }
}