using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sqids;

namespace MyRecipeBookAPI.Binders;

public class MyRecipeBookIdBinder : IModelBinder
{
    private readonly SqidsEncoder<long> encoder;

    public MyRecipeBookIdBinder(SqidsEncoder<long> encoder) => this.encoder = encoder;

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var modelName = bindingContext.ModelName;
        var valueProvidedResult = bindingContext.ValueProvider.GetValue(modelName);
        if (valueProvidedResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }
        bindingContext.ModelState.SetModelValue(modelName, valueProvidedResult);
        var value = valueProvidedResult.FirstValue;
        if (string.IsNullOrEmpty(value))
        {
            return Task.CompletedTask;
        }
        var id = encoder.Decode(value).Single();
        bindingContext.Result = ModelBindingResult.Success(id);
        return Task.CompletedTask;
    }
}