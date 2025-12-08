
using System.Net.Http.Json;
using Azure.Core;

namespace WebApi.Test;

public class MyRecipeBookFixture(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private const string LANG_HEADER = "Accept-Language";
    private readonly HttpClient _httpClient = factory.CreateClient();

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