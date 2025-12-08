using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using MyRecipeBook.Domain.Security.Tokens;

namespace MyRecipeBook.Infrastructure.Security.Tokens.Validator;

public class JwtTokenValidator : JwtTokenHandler, IAccessTokenValidator
{
    private readonly string _signingKey;

#pragma warning disable IDE0290 // Use primary constructor
    public JwtTokenValidator(string signingKey)
#pragma warning restore IDE0290 // Use primary constructor
    {
        _signingKey = signingKey;
    }

    public Guid ValidateAndGetUserIdentifier(string token)
    {
        var validationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = SecurityKey(_signingKey),
            ClockSkew = new TimeSpan(0)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);
        var userIdentifier = claimsPrincipal.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
        return Guid.Parse(userIdentifier);
    }
}