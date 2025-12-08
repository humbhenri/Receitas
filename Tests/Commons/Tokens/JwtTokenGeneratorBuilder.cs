using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Infrastructure.Security.Tokens.Generator;

namespace Commons.Tokens;

public static class JwtTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Build() => 
        new JwtTokenGenerator("tttttttttttttttttttttttttttttttt", 5);
}