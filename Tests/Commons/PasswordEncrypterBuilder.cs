using MyRecipeBook.Application.Services.Crypto;

namespace Commons;

public static class PasswordEncrypterBuilder
{
    public static PasswordEncrypter Build()
    {
        var key = "test";
        return new PasswordEncrypter(key);
    }
}