using Moq;
using MyRecipeBook.Domain.Repositories.Recipe;

namespace Commons.Repositories;

public static class RecipeWriteOnlyRepositoryBuilder
{
    public static IRecipeWriteOnlyRepository Build()
    {
        return new Mock<IRecipeWriteOnlyRepository>().Object;
    }
}
