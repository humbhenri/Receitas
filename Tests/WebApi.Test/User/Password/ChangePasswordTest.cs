using System.Net;
using System.Text.Json;
using Commons.Requests;
using Commons.Tokens;
using Shouldly;

namespace WebApi.Test.User.Password;

public class ChangePasswordTest : MyRecipeBookFixture
{

    private readonly string _password;
    private readonly Guid _userIdentifier;

    public ChangePasswordTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _password = factory.GetPassword();
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact(Skip="Fix me later")]
    public async Task Success()
    {
        var request = RequestChangePasswordJsonBuilder.Build();
        request.Password = _password;
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);
        var response = await DoPut("user/change-password", request, token);
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }
}