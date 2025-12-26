using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.UseCases.Login.DoLogin;
using MyRecipeBook.Application.UseCases.Recipe;
using MyRecipeBook.Application.UseCases.User.Password;
using MyRecipeBook.Application.UseCases.User.Profile;
using MyRecipeBook.Application.UseCases.User.Register;
using Sqids;

namespace MyRecipeBook.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddUseCases(services);
        AddIdEncoder(services, configuration);
    }

    public static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
        services.AddScoped<IRegisterRecipeUseCase, RegisterRecipeUseCase>();
        services.AddScoped<IFilterRecipeUseCase, FilterRecipeUseCase>();
        services.AddScoped<IGetRecipeByIdUseCase, GetRecipeByIdUseCase>();
        services.AddScoped<IDeleteRecipeUseCase, DeleteRecipeUseCase>();
        services.AddScoped<IUpdateRecipeUseCase, UpdateRecipeUseCase>();
    }

    private static void AddIdEncoder(IServiceCollection services, IConfiguration configuration)
    {
       var sqids = new SqidsEncoder<long>(new()
       {
           MinLength = 3,
           Alphabet = configuration.GetValue<string>("Settings:IdCryptographyAlphabet")!
       });
       services.AddSingleton(sqids);
    }
}