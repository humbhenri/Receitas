using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.Crypto;
using MyRecipeBook.Application.UseCases.User.Register;

namespace MyRecipeBook.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
        AddPasswordEncrypter(services);
    }

    public static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }

    private static void AddPasswordEncrypter(IServiceCollection services)
    {
        services.AddScoped(options => new PasswordEncrypter());
    }


}