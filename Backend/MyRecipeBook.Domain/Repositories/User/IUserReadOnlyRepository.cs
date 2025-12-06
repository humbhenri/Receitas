namespace MyRecipeBook.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    public Task<bool> ExistActiveUserWithEmail(string email);

    public Task<MyRecipeBook.Domain.Entities.User?> GetByEmailAndPassword(string email, string password);
}