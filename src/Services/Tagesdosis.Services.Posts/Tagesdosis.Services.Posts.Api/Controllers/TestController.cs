using Microsoft.AspNetCore.Mvc;
using Tagesdosis.Services.Posts.Data.Entities;
using Tagesdosis.Services.Posts.Data.Repositories.Interfaces;

namespace Tagesdosis.Services.Posts.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TestController : ControllerBase
{
    private IPostRepository _repository;

    public TestController(IPostRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Test()
    {
        await _repository.CreatePostAsync(new Post { Content = "Post123", IsVisible = true, Title = "Post", UserId = "id" });
        return Ok("Good");
    }
}