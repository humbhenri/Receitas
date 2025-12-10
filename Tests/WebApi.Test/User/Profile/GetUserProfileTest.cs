using System.Net;
using System.Text.Json;
using Commons.Tokens;
using Shouldly;

namespace WebApi.Test.User.Profile;

public class GetUserProfileTest : MyRecipeBookFixture
{
    private readonly string METHOD = "user";
    private readonly string _name;
    private readonly string _email;
    private readonly Guid _userIdentifier;

    public GetUserProfileTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _name = factory.GetName();
        _email = factory.GetEmail();
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);
        var response = await DoGet(METHOD, token: token);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        await using var body = await response.Content.ReadAsStreamAsync();
        var data = await JsonDocument.ParseAsync(body);
        data.RootElement.GetProperty("name").GetString().ShouldBe(_name);
        data.RootElement.GetProperty("email").GetString().ShouldBe(_email);
    }

    [Fact]
    public async Task Error_Invalid_Token()
    {
        var response = await DoGet(METHOD, token: "invalid");
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}
