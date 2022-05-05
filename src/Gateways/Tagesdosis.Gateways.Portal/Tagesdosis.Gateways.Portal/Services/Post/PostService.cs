using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Tagesdosis.Domain.Types;
using Tagesdosis.Gateways.Portal.Data.Models.Post;
using Tagesdosis.Gateways.Portal.Providers;
using Tagesdosis.Gateways.Portal.Services.Post.Interfaces;
using Tagesdosis.Gateways.Portal.Static;

namespace Tagesdosis.Gateways.Portal.Services.Post;

public class PostService : IPostService
{
    private readonly HttpClient _httpClient;
    private readonly TokenAuthenticationStateProvider _stateProvider;
    
    public PostService(IHttpClientFactory factory, IConfiguration configuration, TokenAuthenticationStateProvider stateProvider)
    {
        _httpClient = factory.CreateClient(configuration["Services:Post:Client"]);
        _stateProvider = stateProvider;
    }

    public async Task<ApiResponse<int>?> CreatePostAsync(string title, string content)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, Endpoints.Post);
        var post = new PostModel(title, content);
        var json = JsonSerializer.Serialize(post);
        
        request.Headers.Add("Authorization", "Bearer " + await _stateProvider.GetTokenAsync());
        request.Content = new StringContent(json);
        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

        var response = await _httpClient.SendAsync(request);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<int>>();

        return apiResponse;
    }
}