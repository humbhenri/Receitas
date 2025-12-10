using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;

namespace MyRecipeBook.Infrastructure.Repositories;

public class UserRepository(MyRecipeBookDbContext dbContext) : IUserReadOnlyRepository, IUserWriteOnlyRepository,
    IUserUpdateOnlyRepository
{
    private readonly MyRecipeBookDbContext _dbContext = dbContext;

    public async Task Add(User user)
    {
        await _dbContext.AddAsync(user);
    }

    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) &&
        user.Active);
    }

    public async Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifer)
    {
        return await _dbContext.Users.AnyAsync(user => user.Active && user.UserIdentifier.Equals(userIdentifer));
    }

    public async Task<User?> GetByEmailAndPassword(string email, string password)
    {
        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email.Equals(email)
                && user.Password.Equals(password)
                && user.Active);
    }

    public async Task<User> GetById(long id)
    {
        return await _dbContext
            .Users
            .FirstAsync(user => user.Id == id);
    }

    public void Update(User user) => _dbContext.Users.Update(user);
}
