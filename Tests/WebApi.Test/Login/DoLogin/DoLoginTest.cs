using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using MyRecipeBook.Communication.Requests;
using Shouldly;

namespace WebApi.Test.Login.DoLogin;

public class DoLoginTest : MyRecipeBookFixture
{
    private readonly string method = "login";

    private readonly string _email;

    private readonly string _password;

    private readonly string _name;

#pragma warning disable IDE0290 // Use primary constructor
    public DoLoginTest(CustomWebApplicationFactory factory) : base(factory)
#pragma warning restore IDE0290 // Use primary constructor
    {
        _email = factory.GetEmail();
        _password = factory.GetPassword();
        _name = factory.GetName();
    }

    [Fact(Skip = "Fix me later")]
    public async Task Success()
    {
        var request = new RequestLoginJson
        {
            Email = _email,
            Password = _password
        };

        var response = await DoPost(method, request);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);
        responseData.RootElement.GetProperty("name").GetString().ShouldBe(_name);
        responseData.RootElement.GetProperty("tokens").GetProperty("accessToken").GetString().ShouldNotBeNullOrEmpty();
    }
}