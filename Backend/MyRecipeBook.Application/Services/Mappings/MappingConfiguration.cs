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

        TypeAdapterConfig<RequestRecipeJson, Recipe>
            .NewConfig()
            .Ignore(dest => dest.Instructions)
            .Map(dest => dest.Ingredients, src => src.Ingredients.Distinct())
            .Map(dest => dest.DishTypes, src => src.DishTypes.Distinct());

        TypeAdapterConfig<string, Ingredient>
            .NewConfig()
            .Map(dest => dest.Item, opt => opt);

        TypeAdapterConfig<Domain.Enums.DishType, DishType>
            .NewConfig()
            .Map(dest => dest.Type, opt => opt);

    }
}