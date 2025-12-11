using Moq;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;

namespace Commons.Repositories;

public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _mock;

    public UserReadOnlyRepositoryBuilder() => _mock = new Mock<IUserReadOnlyRepository>();

    public void ExistActiveUserWithEmail(string email)
    {
        _mock.Setup(repository => repository.ExistActiveUserWithEmail(email))
            .ReturnsAsync(true);
    }

    public void GetByEmailAndPassword(User user)
    {
        _mock.Setup(repository => repository.GetByEmailAndPassword(user.Email, user.Password))
            .ReturnsAsync(user);
    }

    public void GetById(User user)
    {
        _mock.Setup(repository => repository.GetById(user.Id)).ReturnsAsync(user);
    }

    public IUserReadOnlyRepository Build()
    {
        return _mock.Object;
    }
}