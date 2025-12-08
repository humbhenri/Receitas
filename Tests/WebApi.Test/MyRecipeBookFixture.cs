
using System.Net.Http.Json;

namespace WebApi.Test;

public class MyRecipeBookFixture : IClassFixture<CustomWebApplicationFactory>
{
    private const string LANG_HEADER = "Accept-Language";
    private readonly HttpClient _httpClient;

#pragma warning disable IDE0290 // Use primary constructor
    public MyRecipeBookFixture(CustomWebApplicationFactory factory)
#pragma warning restore IDE0290 // Use primary constructor
    {
        _httpClient = factory.CreateClient();
    }

    protected async Task<HttpResponseMessage> DoPost(string method, object request, string culture = "en")
    {
        ChangeRequestCulture(culture);
        return await _httpClient.PostAsJsonAsync(method, request);
    }

    private void ChangeRequestCulture(string culture)
    {
        if (_httpClient.DefaultRequestHeaders.Contains(LANG_HEADER))
        {
            _httpClient.DefaultRequestHeaders.Remove(LANG_HEADER);
        }
        _httpClient.DefaultRequestHeaders.Add(LANG_HEADER, culture);
    }
}