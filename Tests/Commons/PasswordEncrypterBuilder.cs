using MyRecipeBook.Domain.Security.Crypto;
using MyRecipeBook.Infrastructure.Services.Crypto;

namespace Commons;

public static class PasswordEncrypterBuilder
{
    public static IPasswordEncrypter Build()
    {
        var key = "test";
        return new Sha512Encrypter(key);
    }
}