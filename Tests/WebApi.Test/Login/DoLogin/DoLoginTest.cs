using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using MyRecipeBook.Communication.Requests;
using Shouldly;

namespace WebApi.Test.Login.DoLogin;

public class DoLoginTest(CustomWebApplicationFactory factory) : MyRecipeBookFixture(factory)
{
    private readonly string method = "login";

    private readonly HttpClient _httpClient = factory.CreateClient();

    private readonly string _email = factory.GetEmail();

    private readonly string _password = factory.GetPassword();

    private readonly string _name = factory.GetName();

    [Fact]
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
 
    }
}