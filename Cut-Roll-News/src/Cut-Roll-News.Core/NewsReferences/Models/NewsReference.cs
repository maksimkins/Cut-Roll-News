using Cut_Roll_News.Core.NewsReferences.Enums;

namespace Cut_Roll_News.Core.NewsReferences.Models;

public class NewsReference
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string NewsArticleId { get; set; }
    public required string ReferenceId { get; set; }
    public required string ReferenceUrl { get; set; }
    public required ReferenceType ReferenceType { get; set; } 
}
