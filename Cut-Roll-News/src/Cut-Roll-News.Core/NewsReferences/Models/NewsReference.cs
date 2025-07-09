using Cut_Roll_News.Core.NewsReferences.Enums;

namespace Cut_Roll_News.Core.NewsReferences.Models;

public class NewsReference
{
    public int Id { get; set; }
    public required int NewsArticleId { get; set; }
    public required string ReferenceId { get; set; }
    public required string ReferenceUrl { get; set; }
    public required ReferenceType ReferenceType { get; set; } 
}
