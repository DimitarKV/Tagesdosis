using Tagesdosis.Domain.Entities;

namespace Tagesdosis.Services.Posts.Data.Entities;

public class Author : Entity<int>
{
    public string UserName { get; set; }
    public string ExternalId { get; set; }
    public IList<Post> Posts { get; set; }
}