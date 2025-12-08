using Microsoft.AspNetCore.Mvc.Filters;

namespace MyRecipeBookAPI.Attributes;

public class AuthenticatedUserAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        throw new NotImplementedException();
    }
}