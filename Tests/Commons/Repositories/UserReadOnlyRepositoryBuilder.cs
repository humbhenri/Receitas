using Moq;
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

    public IUserReadOnlyRepository Build()
    {
        return _mock.Object;
    }
}