namespace MyRecipeBook.Domain.Repositories.User;

public interface IUserWriteOnlyRepository
{
    public Task Add(MyRecipeBook.Domain.Entities.User user);
}