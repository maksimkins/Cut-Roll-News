namespace Cut_Roll_News.Core.NewsArticles.Models;

using System.ComponentModel;
using System.Text.Json.Serialization;
using Cut_Roll_News.Core.NewsLikes.Models;
using Cut_Roll_News.Core.NewsReferences.Models;

public class NewsArticle
{
    public int Id { get; set; }
    public required string AuthorId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    [DefaultValue(0)]
    public int Likes { get; set; }
    [JsonIgnore]
    public ICollection<NewsReference>? NewsReferences { get; set; }
    [JsonIgnore]
    public ICollection<NewsLike>? NewsLikes { get; set; }
}
