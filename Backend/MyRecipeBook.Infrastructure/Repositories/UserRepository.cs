using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;

namespace MyRecipeBook.Infrastructure.Repositories;

public class UserRepository(MyRecipeBookDbContext dbContext) : IUserReadOnlyRepository, IUserWriteOnlyRepository
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
}