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
}