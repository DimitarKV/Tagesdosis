using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Tagesdosis.Domain.Types;
using Tagesdosis.Gateways.Portal.Services.User.Models;

namespace Tagesdosis.Gateways.Portal.Services.User;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    
    public UserService(IHttpClientFactory factory, IConfiguration configuration)
    {
        var clientName = configuration["Services:User:Client"];
        _httpClient = factory.CreateClient(clientName);
    }

    public async Task<ApiResponse?> CreateAsync(string userName, string email, string password)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, Endpoints.CreateUser);
        var credentials = new UserCredentials(userName, email, password);
        var json = JsonSerializer.Serialize(credentials);
        
        request.Content = new StringContent(json);
        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
        var response = await _httpClient.SendAsync(request);

        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
        
        return apiResponse;
    }

    public async Task<ApiResponse<string>?> CheckPasswordAsync(string userName, string password)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, Endpoints.CheckPassword);
        var credentials = new UserCredentials(userName, "", password);
        var json = JsonSerializer.Serialize(credentials);
        
        request.Content = new StringContent(json);
        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
        var response = await _httpClient.SendAsync(request);

        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
        
        return apiResponse;
    }
}