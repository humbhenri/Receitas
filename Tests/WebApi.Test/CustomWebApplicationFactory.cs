using Commons.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyRecipeBook.Infrastructure.DataAccess;

namespace WebApi.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{

    private MyRecipeBook.Domain.Entities.User _user = default!;

    private string _password = string.Empty;

    public string GetEmail() => _user.Email;

    public string GetPassword() => _password;

    public string GetName() => _user.Name;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<MyRecipeBookDbContext>));
                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }
                services.AddDbContext<MyRecipeBookDbContext>(Options =>
                {
                    Options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                using var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyRecipeBookDbContext>();
                StartDatabase(dbContext);
            });
    }

    private void StartDatabase(MyRecipeBookDbContext dbContext)
    {
        (_user, _password) = UserBuilder.Build();
        dbContext.Database.EnsureDeleted();
        dbContext.Users.Add(_user);
        int count = dbContext.Users.ToList().Count;
        System.Console.WriteLine("Users count =====> " + count);
        dbContext.SaveChanges();    
    }
}