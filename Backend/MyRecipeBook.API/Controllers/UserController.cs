using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBookAPI.Controllers;

[Route("[controller]")]
[ApiController]

public class UserController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello World");
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    public IActionResult Register(RegisterUserJson registerUserJson)
    {
        var useCase = new RegisterUserUseCase();
        var result = useCase.Execute(registerUserJson);
        return Created(string.Empty, result);
    }
}