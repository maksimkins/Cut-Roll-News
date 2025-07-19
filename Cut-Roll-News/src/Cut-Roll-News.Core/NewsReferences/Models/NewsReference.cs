using System.Text.Json.Serialization;
using Cut_Roll_News.Core.NewsArticles.Models;
using Cut_Roll_News.Core.NewsReferences.Enums;

namespace Cut_Roll_News.Core.NewsReferences.Models;

public class NewsReference
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string ReferenceId { get; set; }
    public string? ReferenceUrl { get; set; }
    public required ReferenceType ReferenceType { get; set; } 
    public required string NewsArticleId { get; set; }
    [JsonIgnore]
    public NewsArticle? NewsArticle { get; set; }
}
