namespace Cut_Roll_News.Core.NewsLikes.Models;

public class NewsLike
{
    public int Id { get; set; }
    public required string UserId { get; set; }
    public required int NewsArticleId { get; set; }
}
