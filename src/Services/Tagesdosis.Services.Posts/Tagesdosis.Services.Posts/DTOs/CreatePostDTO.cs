namespace Tagesdosis.Services.Posts.DTOs;

public class CreatePostDTO
{
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsVisible { get; set; }
}