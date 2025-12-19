using Mapster;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Entities;
using Sqids;

namespace MyRecipeBook.Application.Services.Mappings;

public static class MappingConfigurations
{
    public static void Configure(string alphabet)
    {
        var sqids = new SqidsEncoder<long>(new()
        {
            MinLength = 3,
            Alphabet = alphabet
        });

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
            .MapWith(str => new Ingredient { Item = str });

        TypeAdapterConfig<Domain.Enums.DishType, DishType>
            .NewConfig()
            .Map(dest => dest.Type, opt => opt);

        TypeAdapterConfig<Recipe, ResponseRegisteredRecipeJson>
            .NewConfig()
            .Map(dest => dest.Id, src => sqids.Encode(src.Id));

        TypeAdapterConfig<Recipe, ResponseShortRecipeJson>
            .NewConfig()
            .Map(dest => dest.Id, src => sqids.Encode(src.Id))
            .Map(dest => dest.AmountIngredients, src => src.Ingredients.Count);
    }
}