using Tagesdosis.Domain.Entities;

namespace Tagesdosis.Services.Posts.Data.Entities;

public class Post : Entity<int>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string UserId { get; set; }
    public bool IsVisible { get; set; }
}