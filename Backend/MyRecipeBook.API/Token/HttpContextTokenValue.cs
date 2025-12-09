using MyRecipeBook.Domain.Security.Tokens;

namespace MyRecipeBookAPI.Token;

public class HttpContextValue : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpContextValue(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string Value()
    {
        var authorization = _contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();
        return authorization["Bearer".Length..].Trim();
    }
}