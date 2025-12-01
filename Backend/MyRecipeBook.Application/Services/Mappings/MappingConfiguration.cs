using Mapster;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Application.Services.Mappings;

public static class MappingConfigurations
{
    public static void Configure()
    {
        TypeAdapterConfig<RequestRegisterUserJson, User>
            .NewConfig()
            .Ignore(dest => dest.Password);
    }
}