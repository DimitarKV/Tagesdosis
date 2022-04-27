namespace Tagesdosis.Services.Posts.DTOs;

public class UpdatePostDTO
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public bool? IsVisible { get; set; }
}