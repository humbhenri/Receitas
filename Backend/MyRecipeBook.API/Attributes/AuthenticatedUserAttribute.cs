using Microsoft.AspNetCore.Mvc;
using MyRecipeBookAPI.Filters;

namespace MyRecipeBookAPI.Attributes;

public class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
    {
    }
}