
namespace MyRecipeBook.Domain.Repositories.User;

public interface IUserUpdateOnlyRepository
{
    void Update(Entities.User user);
}