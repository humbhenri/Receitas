using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Commons.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using MyRecipeBook.Exceptions;
using Shouldly;

namespace WebApi.Test.User.Register;

public class RegisterUserTest(CustomWebApplicationFactory factory) : MyRecipeBookFixture(factory)
{
    private readonly string method = "user";

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build(6);
        var response = await DoPost(method, request);
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);
        responseData.RootElement.GetProperty("name").GetString().ShouldBe(request.Name);
    }

    [Fact]
    public async Task Error_Empty_Name()
    {
        var request = RequestRegisterUserJsonBuilder.Build(6);
        request.Name = string.Empty;
        var response = await DoPost(method, request);
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);
        var errors = responseData.RootElement.GetProperty("errors")
            .EnumerateArray();
        var errorMsg = Messages.name_empty;
        errors.ShouldHaveSingleItem().GetString().ShouldBe(errorMsg);
    }
}