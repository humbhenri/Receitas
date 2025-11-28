using Microsoft.AspNetCore.Mvc;

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
    public IActionResult Register()
    {
        return Created();
    }
}