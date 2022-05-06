namespace Tagesdosis.Gateways.Portal.Services.Post.Models;

public class CreatePost
{
    public CreatePost(string title, string content, string userName, bool isVisible)
    {
        Title = title;
        Content = content;
        UserName = userName;
        IsVisible = isVisible;
    }

    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsVisible { get; set; }
    public string UserName { get; set; }
}