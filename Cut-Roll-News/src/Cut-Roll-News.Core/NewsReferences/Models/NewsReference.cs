using System.Text.Json.Serialization;
using Cut_Roll_News.Core.NewsArticles.Models;
using Cut_Roll_News.Core.NewsReferences.Enums;

namespace Cut_Roll_News.Core.NewsReferences.Models;

public class NewsReference
{
    public required Guid NewsArticleId { get; set; }
    public required Guid ReferencedId { get; set; }
    public string? ReferencedUrl { get; set; }
    public required ReferenceType ReferenceType { get; set; } 
    [JsonIgnore]
    public NewsArticle? NewsArticle { get; set; }
}
