namespace MyRecipeBook.Domain.Security.Crypto;

public interface IPasswordEncrypter
{
    public string Encrypt(string password);
}