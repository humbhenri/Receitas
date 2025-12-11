using Moq;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;

namespace Commons.Repositories;

public class UserUpdateOnlyRepositoryBuilder
{
    private readonly Mock<IUserUpdateOnlyRepository> _mock;

    public UserUpdateOnlyRepositoryBuilder() => _mock = new Mock<IUserUpdateOnlyRepository>();

    public IUserUpdateOnlyRepository Build()
    {
        return _mock.Object;
    }

}
