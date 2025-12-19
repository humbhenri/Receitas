using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.UseCases.Recipe;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBookAPI.Attributes;

namespace MyRecipeBookAPI.Controllers;

[AuthenticatedUser]
public class RecipeController : MyRecipeBookBaseController
{

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredRecipeJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterRecipeUseCase useCase,
        [FromBody] RequestRecipeJson request
    )
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }

    [HttpPost("filter")]
    [ProducesResponseType(typeof(ResponseRecipesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Filter(
        [FromServices] IFilterRecipeUseCase useCase,
        [FromBody] RequestFilterRecipeJson request
    )
    {
        var response = await useCase.Execute(request);
        if (response.Recipes.Any())
        {
            return Ok(response);
        }
        return NoContent();
    }

    [HttpPost("id")]
    [ProducesResponseType(typeof(ResponseRecipeJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromServices] IGetRecipeByIdUseCase useCase,
        [FromRoute] string id
    )
    {
        var response = await useCase.Execute(long.Parse(id));
        return Ok(response);
    }
}