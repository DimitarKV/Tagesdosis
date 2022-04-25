namespace Tagesdosis.Services.Posts.Views;

public class PostView
{
    public string Owner { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}