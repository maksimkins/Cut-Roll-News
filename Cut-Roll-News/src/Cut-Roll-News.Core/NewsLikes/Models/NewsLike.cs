namespace Cut_Roll_News.Core.NewsLikes.Models;
public class NewsLike
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string UserId { get; set; }
    public required string NewsArticleId { get; set; }
}
