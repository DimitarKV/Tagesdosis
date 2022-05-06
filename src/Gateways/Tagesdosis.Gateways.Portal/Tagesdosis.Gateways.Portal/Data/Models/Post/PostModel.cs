namespace Tagesdosis.Gateways.Portal.Data.Models.Post;

public class PostModel
{
    public PostModel(string title, string content)
    {
        Title = title;
        Content = content;
    }

    public string Title { get; set; }
    public string Content { get; set; }
}