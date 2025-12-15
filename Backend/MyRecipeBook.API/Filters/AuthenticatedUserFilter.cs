using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBookAPI.Filters;

public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
{
    private readonly IAccessTokenValidator accessTokenValidator;
    private readonly IUserReadOnlyRepository repository;

    public AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator, IUserReadOnlyRepository repository)
    {
        this.accessTokenValidator = accessTokenValidator;
        this.repository = repository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try {
            var token = TokenOnRequest(context);
            var userIdentifer = accessTokenValidator.ValidateAndGetUserIdentifier(token);
            var exists = await repository.ExistActiveUserWithIdentifier(userIdentifer);
            if (!exists)
            {
                throw new MyRecipeBookException(Messages.forbidden);
            }
        } 
        catch (SecurityTokenExpiredException)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(string.Empty)
            {
                TokenIsExpired = true
            });
        }
        catch (MyRecipeBookException e)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(e.Message));
        }
        catch
        {

            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(Messages.forbidden));
        }
    }

    private string TokenOnRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString();
        if(string.IsNullOrWhiteSpace(authentication))
        {
            throw new MyRecipeBookException(Messages.no_token);
        }
        return removeBearer(authentication);
    }

    private string removeBearer(string authentication)
    {
        return authentication["Bearer ".Length..].Trim();
    }

}