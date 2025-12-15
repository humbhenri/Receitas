
namespace MyRecipeBook.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    Task<bool> ExistActiveUserWithEmail(string email);
    Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifer);
    Task<Entities.User?> GetByEmailAndPassword(string email, string password);
    Task<Entities.User?> GetById(long id);
    List<string> GetAllUsers();
}