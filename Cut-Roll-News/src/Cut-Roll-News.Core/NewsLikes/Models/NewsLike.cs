using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Cut_Roll_News.Core.NewsArticles.Models;

namespace Cut_Roll_News.Core.NewsLikes.Models;

public class NewsLike
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string UserId { get; set; }
    public required string NewsArticleId { get; set; }
    [JsonIgnore]
    public NewsArticle? NewsArticle { get; set; }
}
